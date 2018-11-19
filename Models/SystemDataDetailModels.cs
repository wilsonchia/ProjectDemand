using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace MvcDemand.Models
{
    public class SystemDataDetailModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        public List<oSystemDataDetail> viewSystemDataDetail = new List<oSystemDataDetail>();
        public List<oSystemDataDetail> detailSystemDataDetail = new List<oSystemDataDetail>();
        public string valSystemClass { get; set; }
        public string valSystemValue { get; set; }
        public string valSystemTitle { get; set; }
        public string valSystemNotation { get; set; }
        public string valSystemRemark { get; set; }
        public string valSystemStatus { get; set; }
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
        public List<string> aryColumnName = new List<string>() { "SystemClass", "SystemValue", "SystemTitle", "SystemNotation", "SystemRemark", "SystemStatus" };
        public List<string> aryDeclareName = new List<string>() { "@SystemClass", "@SystemValue", "@SystemTitle", "@SystemNotation", "@SystemRemark", "@SystemStatus" };

        public DataTable returnDataTable() {
            try {
                funDataTable= null; funQuerySQL = ""; funDicParas = null;
                funQuerySQL = "select * from SystemDataDetail where 1=1 ";
                funDataTable = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);                
            } catch (Exception ex) {
                Console.Write(ex.Message);
            }
            return funDataTable;
        }

        public List<oSystemDataDetail> listObjSystemDataDetail() {
            List<oSystemDataDetail> rtnList = new List<oSystemDataDetail>();
            try {                
                funDataTable = null; funDataTable = returnDataTable();
                if (funDataTable.Rows.Count > 0) {
                    foreach (DataRow dr in funDataTable.Rows) {
                        oSystemDataDetail item = new oSystemDataDetail();
                        item.oSystemClass = dr["SystemClass"].ToString();
                        item.oSystemValue = dr["SystemValue"].ToString();
                        item.oSystemTitle = dr["SystemTitle"].ToString();
                        item.oSystemNotation = dr["SystemNotation"].ToString();
                        item.oSystemRemark = dr["SystemRemark"].ToString();
                        item.oSystemStatus = dr["SystemStatus"].ToString();
                        rtnList.Add(item);
                    }
                }
            } catch (Exception ex) {
                Console.Write(ex.Message);
            }
            return rtnList;
        }

        public List<oSystemDataDetail> detailObjSystemDataDetail(string fSystemClass, string fSystemValue, string fSearchValue) {
            List<oSystemDataDetail> detailList = new List<oSystemDataDetail>();
            try {
                detailList = listObjSystemDataDetail().Where(x => x.oSystemClass == fSystemClass && x.oSystemValue == fSystemValue).ToList();                
            } catch (Exception ex) {
                detailList.Clear();
            }
            return detailList;
        }

        public List<SelectListItem> selObjSystemDataDetail(string fSystemClass, string fNullTitle) {
            List<SelectListItem> rtnSelList = new List<SelectListItem>();
            List<oSystemDataDetail> SysList = new List<oSystemDataDetail>();
            try {
                SysList = listObjSystemDataDetail().Where(x => x.oSystemClass == fSystemClass && x.oSystemStatus != "D").ToList();
                rtnSelList.Add(new SelectListItem() { Value = "", Text = fNullTitle });
                if (SysList.Count > 0) {
                    foreach (oSystemDataDetail item in SysList) {
                        rtnSelList.Add(new SelectListItem() { Value = item.oSystemValue.ToString(), Text=item.oSystemTitle.ToString() });
                    }
                }
            } catch (Exception ex) {
                rtnSelList = null;
            }
            return rtnSelList;
        }

        public string DataCreate(List<object> listDataCreate) {
            funExecuteValue = ""; funExecuteSQL = ""; funDicParas = null;
            try {
                funExecuteSQL = "Insert into SystemDataDetail ( ";
                for (int i = 0; i < aryColumnName.Count; i++) {
                    funExecuteSQL += string.Format(@"{0}{1}", aryColumnName[i].ToString(), (i < aryColumnName.Count - 1) ? "," : "");
                }
                funExecuteSQL += " ) Values ( ";
                for (int j = 0; j < aryDeclareName.Count; j++) {
                    funExecuteSQL += string.Format(@"{0}{1}", aryDeclareName[j].ToString(), (j < aryDeclareName.Count - 1) ? "," : "");
                }
                funExecuteSQL += " ) ";
                funDicParas = null; funDicParas = dbClass.GetListToNewDictionary(aryDeclareName, listDataCreate);
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
        }

        public string DataUpdate(List<object> listDataUpdate) {
            funExecuteSQL = ""; funExecuteValue = ""; funDicParas = null;
            try {
                funExecuteSQL = "Update SystemDataDetail Set ";
                for (int i = 2; i < aryColumnName.Count; i++) {
                    funExecuteSQL += string.Format(@" {0}={1}{2}", aryColumnName[i], aryDeclareName[i], (i < aryColumnName.Count - 1) ? "," : "");
                }
                funExecuteSQL += " Where ";
                for (int j = 0; j < 2; j++) {
                    funExecuteSQL += string.Format(@" {0}={1}{2}", aryColumnName[j], aryDeclareName[j], (j < 1) ? " and " : "");
			    }
                funDicParas = dbClass.GetListToNewDictionary(aryDeclareName, listDataUpdate);
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);                
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
        }

        public string DataDelete(string fSystemClass, string fSystemValue) {
            funExecuteValue = ""; funExecuteSQL = ""; funDicParas = null;
            try {
                funExecuteSQL = string.Format(@" Delete from SystemDataDetail Where SystemClass='{0}' and SystemValue='{1}' ", fSystemClass, fSystemValue);
                funExecuteValue = dbClass.msExecuteValueToDataBase(funExecuteSQL, funDicParas);
            } catch (Exception ex) {
                funExecuteValue = ex.Message;
            }
            return funExecuteValue;
        }

    }

    public class oSystemDataDetail {
        public string oSystemClass { get; set; }
        public string oSystemValue { get; set; }
        public string oSystemTitle { get; set; }
        public string oSystemNotation { get; set; }
        public string oSystemRemark { get; set; }
        public string oSystemStatus { get; set; }
    }


}