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

        SystemDataDetailModels addModel = new SystemDataDetailModels();
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
            string valAccountData = "";
            foreach (oAccountDetail item in viewModel.listAccountDetail) { valAccountData += item.oAccIndex.ToString() + "_";  }
            valAccountData = (valAccountData != "") ? valAccountData.Substring(0, valAccountData.Length - 1) : valAccountData;
            viewModel.aryAccountDetail = valAccountData;
            return View(viewModel);
        }


        public PartialViewResult GetBindData(string fSearchValue, AccountRelationModels viewModel) {
            
            /*
            viewModel.listAccountRelationA = sdList.Where(x => x.oRelationClass.Equals("A")).ToList();
            ViewBag.listAccountRelationA = viewModel.listAccountRelationA;
            viewModel.listAccountRelationB = sdList.Where(x => x.oRelationClass == "B").ToList();
            viewModel.listAccountRelationC = sdList.Where(x => x.oRelationClass == "C").ToList();
            viewModel.listAccountRelationD = sdList.Where(x => x.oRelationClass == "D").ToList();
            viewModel.listAccountRelationE = sdList.Where(x => x.oRelationClass == "E").ToList();
            viewModel.listAccountRelationF = sdList.Where(x => x.oRelationClass == "F").ToList();
            viewModel.listAccountRelationG = sdList.Where(x => x.oRelationClass == "G").ToList();
            viewModel.listAccountRelationH = sdList.Where(x => x.oRelationClass == "H").ToList();
             */ 
            return PartialView("list", viewModel);
        }


    }
}
