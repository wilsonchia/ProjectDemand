using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Models
{
    public class AccountDetailModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        SystemDataDetailModels addModels = new SystemDataDetailModels();

        public List<oAccountDetail> viewAccountDetail = new List<oAccountDetail>();
        public List<oAccountDetail> detailAccountDetail = new List<oAccountDetail>();
        public List<SelectListItem> selAccDeptNo = new List<SelectListItem>();
        public List<SelectListItem> selAccJobNo = new List<SelectListItem>();
        public List<SelectListItem> selAccClass = new List<SelectListItem>();
        public string valAccIndex { get; set; }
        public string valAccNo { get; set; }
        public string valAccName { get; set; }
        public string valAccClass { get; set; }
        public string valAccDeptNo { get; set; }
        public string valAccJobNo { get; set; }
        public string valAccMobile { get; set; }
        public string valAccPhone { get; set; }
        public string valAccEmail { get; set; }
        public string valAccPassword { get; set; }
        public string valAccNotation { get; set; }
        public string valAccImage { get; set; }
        public string valAccDateS { get; set; }
        public string valAccDateE { get; set; }
        public string valAccStatus { get; set; }
        public string valTitleAccClass { get; set; }
        public string valTitleAccDeptNo { get; set; }
        public string valTitleAccJobNo { get; set; }
        public string valTitleAccStatus { get; set; }
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
        public List<string> aryColumnName = new List<string>() { 
            "AccIndex", "AccNo", "AccName", "AccClass", "AccDeptNo"
            , "AccJobNo" , "AccMobile", "AccPhone", "AccEmail",  "AccPassword"
            , "AccNotation", "AccImage", "AccDateS", "AccDateE", "AccStatus", "AccNotationS" };
        public List<string> aryDeclareName = new List<string>() { 
            "@AccIndex", "@AccNo", "@AccName", "@AccClass", "@AccDeptNo"
            , "@AccJobNo" , "@AccMobile", "@AccPhone", "@AccEmail",  "@AccPassword"
            , "@AccNotation", "@AccImage", "@AccDateS", "@AccDateE", "@AccStatus","@AccNotationS" };

        public DataTable returnDataTable() {
            funDataTable.Clear(); funQuerySQL = ""; funDicParas = null;
            funQuerySQL = "select a.*, sa.SystemTitle as titleDeptNo, sb.SystemTitle as titleJobNo, sc.SystemTitle as titleClass"
                        + " from AccountDetail a inner join SystemDataDetail sa on sa.SystemClass='AccDeptNo' and sa.SystemValue=a.AccDeptNo "
                        + " inner join SystemDataDetail sb on sb.SystemClass='AccJobNo' and sb.SystemValue=a.AccJobNo "
                        + " inner join SystemDataDetail sc on sc.SystemClass='AccClass' and sc.SystemValue=a.AccClass Where 1=1 ";
            funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            return funDataTable;
        }

        public List<oAccountDetail> listObjAccountDetail() {            
            funDataTable = new DataTable(); funDataTable = returnDataTable();
            var rtnList = (from dt in funDataTable.AsEnumerable()
                       select new oAccountDetail {
                           oAccIndex = dt.Field<string>("AccIndex").ToString(),
                           oAccNo = dt.Field<string>("AccNo").ToString(),
                           oAccName = dt.Field<string>("AccName").ToString(),
                           oAccClass = dt.Field<string>("AccClass").ToString(),
                           oAccDeptNo = dt.Field<string>("AccDeptNo").ToString(),
                           oAccJobNo = dt.Field<string>("AccJobNo").ToString(),
                           oAccMobile = dt.Field<string>("AccMobile").ToString(),
                           oAccPhone = dt.Field<string>("AccPhone").ToString(),
                           oAccEmail = dt.Field<string>("AccEmail").ToString(),
                           oAccPassword = dt.Field<string>("AccPassword").ToString(),
                           oAccNotation = dt.Field<string>("AccNotation").ToString(),
                           oAccImage = dt.Field<string>("AccImage").ToString(),
                           oAccDateS = dt.Field<string>("AccDateS").ToString(),
                           oAccDateE = dt.Field<string>("AccDateE").ToString(),
                           oAccStatus = dt.Field<string>("AccStatus").ToString(),
                           oTitleAccDeptNo = dt.Field<string>("titleDeptNo").ToString(),
                           oTitleAccJobNo = dt.Field<string>("titleJobNo").ToString(),
                           oTitleAccClass = dt.Field<string>("titleClass").ToString(),
                           oTitleAccStatus = (dt.Field<string>("AccStatus").ToString() == "O") ? "啟用" : "停用",
                           oAccNotationS = dt.Field<string>("AccNotationS").ToString() 
                       }).ToList();            
            return rtnList;
        }

        public List<oAccountDetail> detailObjAccountDetail(string funAccIndex, string funAccNo)
        {
            List<oAccountDetail> rtnList = new List<oAccountDetail>();
            rtnList = listObjAccountDetail().Where(x => x.oAccIndex == funAccIndex && x.oAccNo == funAccNo).ToList();
            return rtnList;
        }

        public string getNewAccIndex()
        {
            funReturnValue = ""; funQuerySQL = ""; funDicParas = null; funDataTable = new DataTable(); 
            funQuerySQL = "select left(replicate('0', 5-len(cast(max(accindex) as int)+1)) "
                        + " + cast(cast(max(accindex) as int)+1 as varchar),5) as MaxIndex from AccountDetail";
            funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            if (funDataTable.Rows.Count > 0) { funReturnValue = funDataTable.Rows[0]["MaxIndex"].ToString(); }
            return funReturnValue;
        }
        
        public DataTable returnAccountDataTable()
        {
            funDataTable = null; funQuerySQL = ""; funDicParas = null;
            try
            {
                funQuerySQL = " Select a.AccIndex, a.AccNo, a.AccName, a.AccClass, a.AccDeptNo, a.AccJobNo, sa.SystemTitle as TitleAccClass"
                            + " ,sb.SystemTitle as TitleAccDeptNo, sc.SystemTitle as TitleccJobNo From AccountDetail a "
                            + " Inner Join SystemDataDetail sa on sa.SystemClass='AccClass' and sa.SystemValue=a.AccClass "
                            + " Inner Join SystemDataDetail sb on sb.SystemClass='AccDeptNo' and sb.SystemValue=a.AccDeptNo "
                            + " Inner Join SystemDataDetail sc on sc.SystemClass='AccJobNo' and sc.SystemValue=a.AccJobNo "
                            + " Where a.AccStatus='O' ";
                funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            }
            catch (Exception ex)
            {
                funDataTable.Clear();
            }
            return funDataTable;
        }

    }

    public class oAccountDetail
    {
        public string oAccIndex { get; set; }
        public string oAccNo { get; set; }
        public string oAccName { get; set; }
        public string oAccClass { get; set; }
        public string oAccDeptNo { get; set; }
        public string oAccJobNo { get; set; }
        public string oAccMobile { get; set; }
        public string oAccPhone { get; set; }
        public string oAccEmail { get; set; }
        public string oAccPassword { get; set; }
        public string oAccNotation { get; set; }
        public string oAccNotationS { get; set; }
        public string oAccImage { get; set; }
        public string oAccDateS { get; set; }
        public string oAccDateE { get; set; }
        public string oAccStatus { get; set; }
        public string oTitleAccClass { get; set; }
        public string oTitleAccDeptNo { get; set; }
        public string oTitleAccJobNo { get; set; }
        public string oTitleAccStatus { get; set; }
    }


}