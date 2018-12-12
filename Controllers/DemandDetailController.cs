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

    }
}
