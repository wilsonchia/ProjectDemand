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

        //
        // GET: /DemandDetail/

        public ActionResult Index()
        {
            return View();
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


            return Redirect("");
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
