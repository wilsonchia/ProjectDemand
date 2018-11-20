using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Models
{
    public class AccountDetailModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        SystemDataDetailModels addModels = new SystemDataDetailModels();

        List<oAccountDetail> viewAccountDetail = new List<oAccountDetail>();
        List<oAccountDetail> detailAccountDetail = new List<oAccountDetail>();
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
            , "AccNotation", "AccImage", "AccDateS", "AccDateE", "AccStatus" };
        public List<string> aryDeclareName = new List<string>() { 
            "@AccIndex", "@AccNo", "@AccName", "@AccClass", "@AccDeptNo"
            , "@AccJobNo" , "@AccMobile", "@AccPhone", "@AccEmail",  "@AccPassword"
            , "@AccNotation", "@AccImage", "@AccDateS", "@AccDateE", "@AccStatus" };


        public DataTable returnDataTable()
        {
            funDataTable.Clear(); funQuerySQL = ""; funDicParas = null;
            funQuerySQL = "select a.*, sa.SystemTitle as nameDeptNo, sb.SystemTitle as nameJobNo, sb.SystemTitle as nameClass" 
                        + " from AccountDetail a inner join SystemDataDetail sa on sa.systemclass='AccDeptNo' and sa.SystemValue=a.AccDeptNo "
                        + " inner join SystemDataDetail sb on sb.SystemClass='AccJobNo' and sb.SystemValue=a.AccJobNo "
                        + " inner join SystemDataDetail sc on sc.SystemClass='AccClass' and sc.SystemValue = a.AccClass";
            funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
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