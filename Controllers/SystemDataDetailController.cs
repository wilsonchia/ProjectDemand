using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Controllers
{
    public class SystemDataDetailController : Controller
    {
        SystemDataDetailModels sddModel = new SystemDataDetailModels();


        public ActionResult Index(SystemDataDetailModels viewModel)
        {
            GetBindData("", "1", viewModel);
            return View(viewModel);
        }

        public PartialViewResult Detail(string fSystemClass, SystemDataDetailModels viewModel)
        {
            List<oSystemDataDetail> deList = new List<oSystemDataDetail>(); deList = null;
            deList = sddModel.listObjSystemDataDetail().Where(x => x.oSystemClass == fSystemClass && x.oSystemStatus != "D").ToList();
            viewModel.detailSystemDataDetail = deList;            
            return PartialView("Detail", viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, SystemDataDetailModels viewModel)
        {
            GetBindData(form["searchValue"].ToString(), form["hidePageIndex"].ToString(), viewModel);
            return View(viewModel);
        }

        public PartialViewResult GetBindData(string fSearchValue, string fPageIndex , SystemDataDetailModels viewModel) {
            List<oSystemDataDetail> dataList = new List<oSystemDataDetail>();
            dataList = sddModel.listObjSystemDataDetail().Where(x=>x.oSystemStatus == "D").ToList();            
            if (!(string.IsNullOrWhiteSpace(fSearchValue))) {
                dataList = dataList.Where(x => x.oSystemClass.Contains(fSearchValue) || x.oSystemValue.Contains(fSearchValue)
                    || x.oSystemTitle.Contains(fSearchValue) || x.oSystemNotation.Contains(fSearchValue) || x.oSystemRemark.Contains(fSearchValue)
                    ).ToList();
            }
            viewModel.viewSystemDataDetail = dataList;            
            viewModel.valCountSum = dataList.Count.ToString();
            viewModel.valCountPage = "30";
            viewModel.valPageCountSum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(viewModel.valCountSum) / Convert.ToDecimal(viewModel.valCountPage))).ToString();
            viewModel.valPageIndex = fPageIndex;
            ViewBag.valCountSum = viewModel.valCountSum;
            ViewBag.valCountPage = viewModel.valCountPage;
            ViewBag.valPageCountSum = viewModel.valPageCountSum;
            ViewBag.valPageIndex = viewModel.valPageIndex;             
            return PartialView("List", viewModel);
        }
      
        [HttpPost]
        public RedirectResult Create(FormCollection form)
        {
            List<object> aryDataCreate = new List<object>() { 
                form["hideSystemClass"].ToString(), form["hideSystemValue"].ToString(),
                form["hideSystemTitle"].ToString(), form["hideSystemNotation"].ToString(),
                form["hideSystemRemark"].ToString(), form["hideSystemStatus"].ToString()
            };
            String funExecuteValue = sddModel.DataCreate(aryDataCreate);
            return Redirect("~/SystemDataDetail/Index");
        }

    }
}
