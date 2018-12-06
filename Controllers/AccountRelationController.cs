using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;
using System.Data;

namespace MvcDemand.Controllers
{
    public class AccountRelationController : Controller
    {
        ClassDataBase dbClass = new ClassDataBase();
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        AccountDetailModels adModel = new AccountDetailModels();
        AccountRelationModels arModel = new AccountRelationModels();
        public List<string> aryDeclareName = new List<string>() { "@AccIndex", "@AccDeptNo", "@RelationClass", "@RelationAccIndex" };

        
        public ActionResult Index(AccountRelationModels viewModel)
        {
            viewModel.viewAccountRelation = arModel.listAccountRelation();            
            viewModel.listAccountRelationA = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "A").ToList();
            viewModel.listAccountRelationB = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "B").ToList();
            viewModel.listAccountRelationC = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "C").ToList();
            viewModel.listAccountRelationD = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "D").ToList();
            viewModel.listAccountRelationE = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "E").ToList();
            viewModel.listAccountRelationF = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "F").ToList();
            viewModel.listAccountRelationG = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "G").ToList();
            viewModel.listAccountRelationH = viewModel.viewAccountRelation.Where(x => x.oRelationClass == "H").ToList();
            viewModel.listAccountDetail = adModel.listObjAccountDetail();
            string valAccountData = ""; string valAccountDeptData = "";
            foreach (oAccountDetail item in viewModel.listAccountDetail) { 
                valAccountData += item.oAccIndex.ToString() + "_";
                valAccountDeptData += item.oAccIndex.ToString() + "$" + item.oAccDeptNo.ToString() + "_";
            }
            valAccountData = (valAccountData != "") ? valAccountData.Substring(0, valAccountData.Length - 1) : valAccountData;
            viewModel.aryAccountDetail = valAccountData;
            valAccountDeptData = (valAccountDeptData != "") ? valAccountDeptData.Substring(0, valAccountDeptData.Length - 1) : valAccountDeptData;
            viewModel.aryAccountDeptData = valAccountDeptData;
            viewModel.selAccDeptNo = sddModel.selObjSystemDataDetail("AccDeptNo", "請選擇單位", "");
            return View(viewModel);
        }

        public string AccountRelationDataCreate(string fRelatClass, string fAccIndex, string fAccDeptNo)
        {
            string fReturnValue = ""; string fExecuteValue = "";
            List<object> delDeclareValue = new List<object>() { fAccIndex, fAccDeptNo, fRelatClass, "" };
            List<string> delDeclareName = new List<string>() { "@AccIndex", "@AccDeptNo", "@RelationClass", "@RelationAccIndex" };
            fExecuteValue = dbClass.msExecuteDataBase("D", "AccountRelation", 0, delDeclareName, delDeclareValue);
            List<object> insDeclareValue = new List<object>() { fAccIndex, fAccDeptNo, fRelatClass, "" };
            fExecuteValue = dbClass.msExecuteDataBase("N", "AccountRelation", 0, aryDeclareName, insDeclareValue);
            fReturnValue = arModel.returnAccountRelationClassData(fRelatClass, fAccDeptNo);
            return fReturnValue;
        }

        public string AccountRelationDataDelete(string fRelatClass, string fAccIndex, string fAccDeptNo)
        {
            string fReturnValue = ""; string fExecuteValue = "";
            List<string> delDeclareName = new List<string>() { "@AccIndex", "@AccDeptNo", "@RelationClass", "@RelationAccIndex" };
            List<object> delDeclareValue = new List<object>() { fAccIndex, fAccDeptNo, fRelatClass, "" };
            fExecuteValue = dbClass.msExecuteDataBase("D", "AccountRelation", 0, delDeclareName, delDeclareValue);            
            fReturnValue = arModel.returnAccountRelationClassData(fRelatClass, fAccDeptNo);
            return fReturnValue;
        }
        


    }
}
