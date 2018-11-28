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


        //
        // GET: /AccountRelation/

        public ActionResult Index()
        {
            return View();
        }

    }
}
