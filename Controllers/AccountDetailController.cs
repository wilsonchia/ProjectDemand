using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;


namespace MvcDemand.Controllers
{
    public class AccountDetailController : Controller
    {
        AccountDetailModels adModel = new AccountDetailModels();
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        ClassDataBase dbClass = new ClassDataBase();
        
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
                HttpUtility.HtmlEncode(form["textAccNotation"].ToString()),
                "", form["textAccDateS"].ToString(),"","O" };
            string fExecuteValue = adModel.DataCreate(listDataCreate);
            return Redirect("~/AccountDetail/Index");
        }

    }
}
