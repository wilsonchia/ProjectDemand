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

        public DataTable returnDataTable() {
            funDataTable.Clear(); funQuerySQL = ""; funDicParas = null;
            funQuerySQL = "select a.*, sa.SystemTitle as titleDeptNo, sb.SystemTitle as titleJobNo, sb.SystemTitle as titleClass" 
                        + " from AccountDetail a inner join SystemDataDetail sa on sa.systemclass='AccDeptNo' and sa.SystemValue=a.AccDeptNo "
                        + " inner join SystemDataDetail sb on sb.SystemClass='AccJobNo' and sb.SystemValue=a.AccJobNo "
                        + " inner join SystemDataDetail sc on sc.SystemClass='AccClass' and sc.SystemValue = a.AccClass Where 1=1 ";
            funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            return funDataTable;
        }

        public List<oAccountDetail> listObjAccountDetail() {
            List<oAccountDetail> rtnList = new List<oAccountDetail>();
            funDataTable = null; funDataTable = returnDataTable();
            foreach (DataRow dr in funDataTable.Rows)
            {
                oAccountDetail item = new oAccountDetail();
                item.oAccIndex = dr["AccIndex"].ToString();
                item.oAccNo = dr["AccNo"].ToString();
                item.oAccName = dr["AccName"].ToString();
                item.oAccClass = dr["AccClass"].ToString();
                item.oAccDeptNo = dr["AccDeptNo"].ToString();
                item.oAccJobNo = dr["AccJobNo"].ToString();
                item.oAccMobile = dr["AccMobile"].ToString();
                item.oAccPhone = dr["AccPhone"].ToString();
                item.oAccEmail = dr["AccEmail"].ToString();
                item.oAccPassword = dr["AccPassword"].ToString();
                item.oAccNotation = dr["AccNotation"].ToString();
                item.oAccImage = dr["AccImage"].ToString();
                item.oAccDateS = dr["AccDateS"].ToString();
                item.oAccDateE = dr["AccDateE"].ToString();
                item.oAccStatus = dr["AccStatus"].ToString();
                item.oTitleAccDeptNo = dr["titleDeptNo"].ToString();
                item.oTitleAccJobNo = dr["titleJobNo"].ToString();
                item.oTitleAccClass = dr["titleClass"].ToString();
                item.oTitleAccStatus = (dr["AccStatus"].ToString() == "O") ? "啟用" : "停用";
            }
            return rtnList;
        }

        public List<oAccountDetail> detailObjAccountDetail(string funAccIndex, string funAccNo)
        {
            List<oAccountDetail> rtnList = new List<oAccountDetail>();
            rtnList = listObjAccountDetail().Where(x => x.oAccIndex == funAccIndex && x.oAccNo == funAccNo).ToList();
            return rtnList;
        }

        public string DataCreate(List<object> listDataCreate)
        {
            funExecuteValue = ""; funExecuteSQL = ""; funDicParas = null;
            try {
                funDicParas = dbClass.GetListToNewDictionary(aryDeclareName, listDataCreate);
                funExecuteSQL = "Insert Into AccountDetail Values ( ";
                for (int i = 0; i < aryDeclareName.Count; i++) { funExecuteSQL += string.Format(@"{0}{1}", aryDeclareName[i], (i < aryDeclareName.Count - 1) ? "," : ""); }
                funExecuteSQL += " ) ";
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
        }

        public string DataUpdate(List<object> listDataUpdate)
        {
            funExecuteSQL = ""; funExecuteValue = ""; funDicParas = null;
            try {
                funDicParas = dbClass.GetListToNewDictionary(aryDeclareName, listDataUpdate);
                funExecuteSQL = "Update AccountDetail Set ";
                for (int i = 2; i < aryColumnName.Count; i++) {
                    funExecuteSQL += string.Format(@" {0}={1}{2} ", aryColumnName[i], aryDeclareName[i], (i < aryColumnName.Count - 1) ? "," : "");
                }
                funExecuteSQL += " Where ";
                for (int i = 0; i < 2; i++) {
                    funExecuteSQL += string.Format(@" {0}={1}{2}", aryColumnName[i], aryDeclareName[i], (i < 1) ? " and " : "");
                }
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
        }

        public string DataDelete(List<object> listDataDelete) {
            funExecuteValue = ""; funExecuteSQL = ""; funDicParas = null; List<string> aryDeclareValue = new List<string>();
            try {
                aryDeclareValue = new List<string>() { "@AccIndex", "@AccNo" };
                funDicParas = dbClass.GetListToNewDictionary(aryDeclareValue, listDataDelete);
                funExecuteSQL = @"Delete from AccountDetail Where AccIndex=@AccIndex and AccNo=@AccNo ";
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
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