using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;

namespace MvcDemand.Controllers
{
    public class DemandDetailController : Controller
    {

        SystemDataDetailModels sdModel = new SystemDataDetailModels();
        DemandDetailModels ddModel = new DemandDetailModels();
        AccountDetailModels adModel = new AccountDetailModels();
        AccountRelationModels arModel = new AccountRelationModels();
        ClassDataBase dbClass = new ClassDataBase();

        //
        // GET: /DemandDetail/

        public ActionResult Index(DemandDetailModels viewModel)
        {
            viewModel.objDemandDetail = ddModel.objDemandDetailData();
            ViewBag.objDemandDetail = ddModel.objDemandDetailData();
            ViewBag.valCountSum = viewModel.objDemandDetailData().Count().ToString();

            return View(viewModel);
        }

        public ActionResult Create(DemandDetailModels viewModel)
        {
            viewModel.selDemandClass = sdModel.selObjSystemDataDetail("DemandClass", "請選擇", "");
            ViewBag.selDemandClass = viewModel.selDemandClass;
            viewModel.selAccountDetail = ddModel.returnSelectAccountData("", "A");
            ViewBag.selAccountDetail = viewModel.selAccountDetail;
            viewModel.selAccountDetailAgent = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailAgent = viewModel.selAccountDetailAgent;
            viewModel.selAccountDetailTop = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailTop = viewModel.selAccountDetailTop;
            viewModel.selAccountDetailMan = sdModel.selObjSystemDataDetail("", "請選擇", "");
            ViewBag.selAccountDetailMan = viewModel.selAccountDetailMan;
            return View(viewModel);
        }

        
        [HttpPost]
        [ValidateInput(false)]
        public RedirectResult Create(FormCollection form)
        {
            string funMaxDemandIndex = ddModel.returnDemandMaxIndex(); 
            List<string> listSchDeclare = new List<string>() { "@DemandIndex", "@DemandStep", "@SchAccIndex", "@SchNotation", "@SchDateTime", "@SchStatus" };
            List<string> aryDeclare = new List<string>() { "@DemandIndex","@DemandDate","@DemandTitle","@DemandClass","@DemandTest"
                        ,"@DemandUpload","@DemandStep","@DemandFrom","@DemandProject","@DemandDateS"
                        ,"@DemandDateE","@DemandDateH","@DemandNotation","@DemandRemark","@DemandStatus"
                        ,"@DemandAccIndex","@DemandAgentIndex","@DemandTopIndex","@DemandManIndex", "@Update_DateTime"
                        ,"@Create_DateTime" };
            List<object> aryValue = new List<object>(){ funMaxDemandIndex, dbClass.ReturnDetailToNowDateTime("VF"), form["textDemandTitle"].ToString(), form["hideDemandClass"], form["hideDemandTest"]
                , form["hideDemandUpload"], "A", form["textDemandFrom"], form["textDemandProject"], form["textDemandDateS"]
                , "", form["textDemandDateH"], HttpUtility.HtmlEncode(form["textDemandNotation"]), HttpUtility.HtmlEncode(form["textDemandRemark"]), "X"
                , form["hideDemandAccIndex"].Replace(',',' ').Trim(), form["hideDemandAgentIndex"], form["hideDemandTopIndex"], form["hideDemandManIndex"], dbClass.ReturnDetailToNowDateTime("VF"), ""};            
            
            string fExecuteValue = dbClass.msExecuteDataBase("N", "DemandDetail", 0, aryDeclare, aryValue);
            string DemandStepST = (form["hideDemandUpload"] == "O") ? "X" : "O";
            List<object> listSchValue = new List<object>(); string fExecSchValue = "";
            if (form["hideDemandAccIndex"] != "") {
                listSchValue = new List<object>() { funMaxDemandIndex, "A", form["hideDemandAccIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), DemandStepST };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }            
            if (form["hideDemandAgentIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "A", form["hideDemandAgentIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), DemandStepST };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandTopIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "C", form["hideDemandTopIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), "X" };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandManIndex"] != "")
            {
                listSchValue = new List<object>() { funMaxDemandIndex, "D", form["hideDemandManIndex"].Replace(',', ' ').Trim(), "", dbClass.ReturnDetailToNowDateTime("VF"), "X" };
                fExecSchValue = dbClass.msExecuteDataBase("N", "DemandSchedule", 0, listSchDeclare, listSchValue);
            }
            if (form["hideDemandUpload"] == "O")
            {
                return Redirect("~/DemandDetail/Index");            
            } else {
                return Redirect("~/DemandDetail/Upload?fDemandIndex=" + funMaxDemandIndex);            
            }
            
        }

        public ActionResult Upload(DemandDetailModels viewModel, string fDemandIndex)
        {
            viewModel.vDemandIndex = fDemandIndex;
            ViewBag.vDemandIndex = fDemandIndex;
            return View(viewModel);
        }
        
        public string returnValueToAccIndex(string fAccIndex)
        {
            string returnValue = ""; string oAccDeptNo = "";
            List<oAccountDetail> delList = adModel.listObjAccountDetail().Where(x => x.oAccIndex == fAccIndex).ToList();
            oAccDeptNo = (delList.Count > 0) ? delList[0].oAccDeptNo.ToString() : "";
            List<oAccountRelation> ardListB = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "B").ToList();
            List<oAccountRelation> ardListC = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "C").ToList();
            List<oAccountRelation> ardListD = arModel.listAccountRelation().Where(x => x.oAccDeptNo == oAccDeptNo && x.oRelationClass == "E").ToList();            
            returnValue = oAccDeptNo;
            if (ardListB.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemA in ardListB) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemA.oAccIndex, itemA.oAccNo, itemA.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            if (ardListC.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemB in ardListC) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemB.oAccIndex, itemB.oAccNo, itemB.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            if (ardListD.Count > 0) {
                returnValue += "^";
                foreach (oAccountRelation itemC in ardListD) {
                    returnValue += string.Format(@"{0}_{1}_{2}{3}", itemC.oAccIndex, itemC.oAccNo, itemC.oAccName, "|");
                }
            } else { returnValue += "^Zero"; }
            return returnValue;
        }

    }
}
