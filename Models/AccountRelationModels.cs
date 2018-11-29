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

        public DataTable returnDataTable() {
            funDataTable.Clear(); funQuerySQL = ""; funDicParas = null;
            try { 
                funQuerySQL = "Select a.*, sa.SystemTitle as TitleAccDeptNo, sb.SytstemTitle as TitleRelationClass"
                            + ",aa.AccNo, aa.AccName, IsNull(ab.AccNo,'') as RelationAccNo, Isnull(ab.AccName,'') as RelationAccName "
                            + " From AccountRelation a Inner Join SystemDataDetail sa on sa.SystemClass='AccDeptNo' and sa.SystemValue=a.AccDeptNo "
                            + " Inner Join SystemDataDetail sb on sb.SystemClass='RelationClass' and sb.SystemValue=aRelationClass "
                            + " Inner Join AccountDetail aa on aa.AccIndex=a.AccIndex Left Join AccountDetail ab on ab.AccIndex=a.RelationIndex ";
                funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            } catch (Exception ex) {
                funDataTable.Clear();
            }
            return funDataTable;
        }

        public List<oAccountRelation> listAccountRelation()
        {
            List<oAccountRelation> list = new List<oAccountRelation>();
            try {
                DataTable dt = new DataTable(); dt = returnDataTable();
                list = (from a in dt.AsEnumerable() select new oAccountRelation {
                            oAccIndex = a.Field<string>("AccIndex"), oAccNo = a.Field<string>("AccNo"),
                            oAccName = a.Field<string>("AccName"), oAccDeptNo = a.Field<string>("AccDeptNo"),
                            oTitleAccDeptNo = a.Field<string>("TitleAccDeptNo"), oRelationClass = a.Field<string>("RelationClass"),
                            oTitleRelClass = a.Field<string>("TitleRelationClass"), oRelAccIndex = a.Field<string>("RelationAccIndex"),
                            oRelAccNo = a.Field<string>("RelationAccNo"), oRelAccName = a.Field<string>("RelationAccName")
                        }).ToList();
            } catch (Exception ex) {
                list.Clear();
            }
            return list;
        }
        

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