using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Models
{
    public class AccountRelationModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        SystemDataDetailModels sddModel = new SystemDataDetailModels();
        AccountDetailModels adModel = new AccountDetailModels();
        List<oAccountRelation> viewAccountRelation = new List<oAccountRelation>();
        List<oAccountRelation> detailAccountRelation = new List<oAccountRelation>();
        List<SelectListItem> selAccIndex = new List<SelectListItem>();
        List<SelectListItem> selAccDeptNo = new List<SelectListItem>();        
        List<SelectListItem> selRelationAccIndex = new List<SelectListItem>();
        public string valAccIndex { get; set; }
        public string valAccNo { get; set; }
        public string valAccName { get; set; }
        public string valAccDeptNo { get; set; }
        public string valTitleAccDeptNo { get; set; }
        public string valRelationClass { get; set; }
        public string valTitleRelClass { get; set; }
        public string valRelAccIndex { get; set; }
        public string valRelAccNo { get; set; }
        public string valRelAccName { get; set; }
        public string valPageIndex { get; set; }
        public string valPageCountSum { get; set; }
        public string valCountSum { get; set; }
        public string valCountPage { get; set; }
        public string funQuerySQL { get; set; }
        public string funExecuteSQL { get; set; }
        public string funReturnValue { get; set; }
        public string funExecuteValue { get; set; }
        public string valShowDetail { get; set; }
        public string valDetailCount { get; set; }
        public DataTable funDataTable { get; set; }
        public Dictionary<string, object> funDicParas = new Dictionary<string, object>();

        




    }

    public class oAccountRelation {
        public string oAccIndex { get; set; }
        public string oAccNo { get; set; }
        public string oAccName { get; set; }
        public string oAccDeptNo { get; set; }
        public string oTitleAccDeptNo { get; set; }
        public string oRelationClass { get; set; }
        public string oTitleRelClass { get; set; }
        public string oRelAccIndex { get; set; }
        public string oRelAccNo { get; set; }
        public string oRelAccName { get; set; }
    }

}