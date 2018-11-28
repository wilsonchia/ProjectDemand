using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;


namespace MvcDemand.Controllers
{
    public class AccountDetailController : Controller
    {
        AccountDetailModels adModel = new AccountDetailModels();
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        ClassDataBase dbClass = new ClassDataBase();
        public List<string> aryDeclareName = new List<string>() { 
            "@AccIndex", "@AccNo", "@AccName", "@AccClass", "@AccDeptNo"
            , "@AccJobNo" , "@AccMobile", "@AccPhone", "@AccEmail",  "@AccPassword"
            , "@AccNotation", "@AccImage", "@AccDateS", "@AccDateE", "@AccStatus","@AccNotationS" };
        
        public ActionResult Index(AccountDetailModels viewModel)
        {
            getBindData("", "1", viewModel);
            return View(viewModel);
        }

        public PartialViewResult getBindData(string fSearchValue, string fPageIndex , AccountDetailModels viewModel) {
            List<oAccountDetail> accList = new List<oAccountDetail>();
            accList = adModel.listObjAccountDetail();
            
            if (!(string.IsNullOrWhiteSpace(fSearchValue))) {
                accList = accList.Where(x => x.oAccIndex.Contains(fSearchValue) || x.oAccNo.Contains(fSearchValue) || x.oAccName.Contains(fSearchValue)
                    || x.oAccMobile.Contains(fSearchValue) || x.oAccPhone.Contains(fSearchValue) || x.oAccEmail.Contains(fSearchValue) || x.oAccNotation.Contains(fSearchValue)
                    || x.oAccDeptNo.Contains(fSearchValue) || x.oAccJobNo.Contains(fSearchValue)).ToList();
            }
            
            viewModel.viewAccountDetail = accList;
            viewModel.valCountSum = accList.Count.ToString();
            viewModel.valCountPage = "30";
            viewModel.valPageCountSum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(viewModel.valCountSum) / Convert.ToDecimal(viewModel.valCountPage))).ToString();
            viewModel.valPageIndex = fPageIndex;
            ViewBag.valCountSum = viewModel.valCountSum;
            ViewBag.valCountPage = viewModel.valCountPage;
            ViewBag.valPageCountSum = viewModel.valPageCountSum;
            ViewBag.valPageIndex = viewModel.valPageIndex;
            
            List<SelectListItem> listselAccClass = new List<SelectListItem>();
            listselAccClass = sddModel.selObjSystemDataDetail("AccClass", "請選擇帳號類別", "");
            ViewBag.selAccClass = listselAccClass;
            List<SelectListItem> listselDeptNo = new List<SelectListItem>();
            listselDeptNo = sddModel.selObjSystemDataDetail("AccDeptNo", "請選擇所屬單位", "S"); 
            ViewBag.selAccDeptNo = listselDeptNo;
            List<SelectListItem> listselJobNo = new List<SelectListItem>();
            listselJobNo = sddModel.selObjSystemDataDetail("AccJobNo", "請選擇帳號職稱", "D");
            ViewBag.selAccJobNo = listselJobNo;
             
            return PartialView("List", viewModel);
        }

        public string checkAccNo(string funAccNo)
        {
            string rtnValue = ""; int chkCount = 0;
            chkCount = adModel.listObjAccountDetail().Where(x => x.oAccNo == funAccNo).Count();
            rtnValue = (chkCount > 0) ? "O" : "X";                
            return rtnValue;
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectResult Create(FormCollection form)
        {
            List<object> listDataCreate = new List<object>()
            {
                adModel.getNewAccIndex().ToString(), form["textAccNo"].ToString(),
                form["textAccName"].ToString(), form["hideAccClass"].ToString(),
                form["hideAccDeptNo"].ToString(), form["hideAccJobNo"].ToString(),
                form["textAccMobile"].ToString(), form["textAccPhone"].ToString(),
                form["textAccEmail"].ToString(), form["textAccMobile"].ToString(),
                HttpUtility.HtmlEncode(form["textAccNotation"].ToString()), "",
                form["textAccDateS"].ToString(), "", "O", form["hideAccNotationS"].ToString() };
            string fExecuteValue = dbClass.msExecuteDataBase("N", "AccountDetail", 0, aryDeclareName, listDataCreate);            
            return Redirect("~/AccountDetail/Index");
        }

        public  JsonResult FormUpdate(string fAccIndex, string fAccNo) {
            List<oAccountDetail> deList = new List<oAccountDetail>();
            deList = adModel.detailObjAccountDetail(fAccIndex, fAccNo);
            var rtnData = new { 
                deList[0].oAccIndex, deList[0].oAccNo, deList[0].oAccName, deList[0].oAccClass,
                deList[0].oAccDeptNo, deList[0].oAccJobNo, deList[0].oAccMobile, deList[0].oAccPhone,
                deList[0].oAccEmail, deList[0].oAccNotationS, deList[0].oAccDateS, deList[0].oAccDateE,
                deList[0].oAccStatus, deList[0].oAccPassword };
            return Json(rtnData);
        }

        [HttpPost]
        [ValidateInput(false)]
        public RedirectResult Update(FormCollection form)
        {
            List<object> listDataUpdate = new List<object>();
            listDataUpdate.Add(form["hideAccIndex"].ToString());
            listDataUpdate.Add(form["hideAccNo"].ToString());
            listDataUpdate.Add(form["textAccNameU"].ToString());
            listDataUpdate.Add(form["hideAccClass"].ToString());
            listDataUpdate.Add(form["hideAccDeptNo"].ToString());
            listDataUpdate.Add(form["hideAccJobNo"].ToString());
            listDataUpdate.Add(form["textAccMobileU"].ToString());
            listDataUpdate.Add(form["textAccPhoneU"].ToString());
            listDataUpdate.Add(form["textAccEmailU"].ToString());
            listDataUpdate.Add(form["hideAccPassword"].ToString());
            listDataUpdate.Add(HttpUtility.HtmlEncode(form["textAccNotationU"].ToString()));
            listDataUpdate.Add("");
            listDataUpdate.Add(form["hideAccDateS"].ToString());
            listDataUpdate.Add(form["textAccDateE"].ToString());
            listDataUpdate.Add(form["hideAccStatus"].ToString());
            listDataUpdate.Add(form["hideAccNotationS"].ToString());
            string fExecuteValue = dbClass.msExecuteDataBase("U", "AccountDetail", 2, aryDeclareName, listDataUpdate);            
            return Redirect("~/AccountDetail/Index");    
        }

        [HttpPost]
        public RedirectResult Upload(HttpPostedFileBase fileUpload)
        {
            string showDetailData = ""; DataTable dtSystemDetail = new DataTable();
            string AccIndex = ""; string AccNo = "";
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string FilePath = System.Web.HttpContext.Current.Server.MapPath("../excel");
                string FileName = fileUpload.FileName; string fExecuteValue = "";
                fileUpload.SaveAs(string.Format(@"{0}\{1}", FilePath, FileName));
                dynamic hssfweb; DataTable tempDataTable = new DataTable(); tempDataTable.Clear();
                using (MemoryStream ms = new MemoryStream(dbClass.rtnByteReadFromFile(string.Format(@"{0}\{1}", FilePath, FileName))))
                {
                    hssfweb = WorkbookFactory.Create(ms);
                }
                ISheet sheet = (ISheet)hssfweb.GetSheetAt(0);
                if (sheet.LastRowNum > 0)
                {
                    IRow rowT = (IRow)sheet.GetRow(0);
                    for (int i = 0; i < 9; i++)
                    {
                        DataColumn col = new DataColumn(rowT.GetCell(i).ToString(), typeof(string));
                        tempDataTable.Columns.Add(col);
                    }
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow rowD = (IRow)sheet.GetRow(i);
                        if (rowD != null)
                        {
                            DataRow tempRow = tempDataTable.NewRow();
                            for (int a = 0; a < 9; a++)
                            {
                                tempRow[a] = (rowD.GetCell(a).ToString() != null) ? rowD.GetCell(a).ToString() : "";
                            }
                        }
                    }                    
                    if (tempDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow drtemp in tempDataTable.Rows)
                        {
                            List<object> DataList = new List<object>();
                            AccNo = drtemp["AccNo"].ToString();
                            if (adModel.listObjAccountDetail().Where(x => x.oAccIndex == AccNo).Count() > 0)
                            {
                                List<oAccountDetail> deList = new List<oAccountDetail>();
                                deList = adModel.listObjAccountDetail().Where(x => x.oAccIndex == AccNo).ToList();
                                DataList.Add(deList[0].oAccIndex); DataList.Add(drtemp["AccNo"]);
                                DataList.Add(drtemp["AccName"]); DataList.Add(drtemp["AccClass"]);
                                DataList.Add(drtemp["AccDeptNo"]); DataList.Add(drtemp["AccJobNo"]);
                                DataList.Add(drtemp["AccMobile"]); DataList.Add(drtemp["AccPhone"]);
                                DataList.Add(drtemp["AccEmail"]); DataList.Add(drtemp["AccMobile"]);
                                DataList.Add(deList[0].oAccNotation); DataList.Add("");
                                DataList.Add(deList[0].oAccDateS); DataList.Add(deList[0].oAccDateE);
                                DataList.Add(deList[0].oAccStatus); DataList.Add(deList[0].oAccNotationS);
                                fExecuteValue = dbClass.msExecuteDataBase("U", "AccountDetail", 2, aryDeclareName, DataList);
                            } else { 
                                DataList.Add(adModel.getNewAccIndex().ToString());
                                DataList.Add(drtemp["AccNo"]); DataList.Add(drtemp["AccName"]);
                                DataList.Add(drtemp["AccClass"]); DataList.Add(drtemp["AccDeptNo"]);
                                DataList.Add(drtemp["AccJobNo"]); DataList.Add(drtemp["AccMobile"]);
                                DataList.Add(drtemp["AccPhone"]); DataList.Add(drtemp["AccEmail"]);
                                DataList.Add(drtemp["AccMobile"]); DataList.Add("");
                                DataList.Add(""); DataList.Add(dbClass.ReturnDetailToNowDateTime("SD"));
                                DataList.Add(""); DataList.Add("O"); DataList.Add("");
                                fExecuteValue = dbClass.msExecuteDataBase("N", "AccountDetail", 0, aryDeclareName, DataList);
                            }
                        }
                    }                    
                }                
            }
            return Redirect("~/AccountDetail/Index");
        }

        public void OutToExcel() {
            List<string> listExcelTitle = new List<string>() { "AccNo", "AccName", "AccClass", "AccDeptNo", "AccJobNo", "AccMobile", "AccPhone", "AccEmail" };
            List<oAccountDetail> acList = new List<oAccountDetail>();
            acList = adModel.listObjAccountDetail();
            string FileName = "AccountData.xlsx"; string SheetName = "Data"; Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            XSSFWorkbook NpoiWB = new XSSFWorkbook();

            XSSFCellStyle xCellStyle = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFDataFormat NpoiFormat = (XSSFDataFormat)NpoiWB.CreateDataFormat();
            xCellStyle.SetDataFormat(NpoiFormat.GetFormat("[DbNum2][$-804]0"));
            XSSFCellStyle cellStyleFontColor = (XSSFCellStyle)NpoiWB.CreateCellStyle();
            XSSFFont font1 = (XSSFFont)NpoiWB.CreateFont(); font1.Color = (short)10; font1.IsBold = true; cellStyleFontColor.SetFont(font1);

            ISheet xSheet = NpoiWB.CreateSheet(SheetName);

            IRow xRowT = xSheet.CreateRow(0); xRowT.HeightInPoints = 40;
            for (int i = 0; i < listExcelTitle.Count; i++)
            {
                ICell xCellT = xRowT.CreateCell(i); xCellT.SetCellValue(listExcelTitle[i]);
            }

            int len = 0;
            foreach (oAccountDetail item in acList)
            {
                List<string> listExcelData = new List<string>() { 
                    item.oAccNo.ToString(), item.oAccName.ToString(), item.oAccClass.ToString(), 
                    item.oAccDeptNo.ToString(), item.oAccJobNo.ToString(), item.oAccMobile.ToString(),
                    item.oAccPhone.ToString(), item.oAccEmail.ToString() };
                IRow xRowD = xSheet.CreateRow(len + 1); xRowD.HeightInPoints = 40;
                for (int b = 0; b < listExcelData.Count; b++)
                {
                    ICell xCellData = xRowD.CreateCell(b); xCellData.SetCellValue(listExcelData[b]);
                }
                len++;
            }

            MemoryStream MS = new MemoryStream(); NpoiWB.Write(MS);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
            Response.BinaryWrite(MS.ToArray());
            NpoiWB = null; MS.Close(); MS.Dispose(); Response.Flush(); Response.End();

        }


    }
}
