using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcDemand.Models;

namespace MvcDemand.Models
{
    public class DemandDetailModels
    {
        ClassDataBase dbClass = new ClassDataBase();
        AccountRelationModels arModel = new AccountRelationModels();

        public List<oDemandDetail> objDemandDetail = new List<oDemandDetail>();
        public List<SelectListItem> selAccountDetail = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailAgent = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailTop = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailMan = new List<SelectListItem>();
        public List<SelectListItem> selDemandClass = new List<SelectListItem>();
        
        public string vDemandIndex { get; set; }
        public string vDemandDate { get; set; }
        public string vDemandTitle { get; set; }
        public string vDemandClass { get; set; }
        public string vDemandTest { get; set; }
        public string vDemandUpload { get; set; }
        public string vDemandStep { get; set; }
        public string vDemandFrom { get; set; }
        public string vDemandProject { get; set; }
        public string vDemandDateS { get; set; }
        public string vDemandDateE { get; set; }
        public string vDemandDateH { get; set; }
        public string vDemandNotation { get; set; }
        public string vDemandRemark { get; set; }
        public string vDemandStatus { get; set; }
        public string vDemandAccIndex { get; set; }
        public string vDemandAgentIndex { get; set; }
        public string vDemandTopIndex { get; set; }
        public string vDemandManIndex { get; set; }
        public string vUpdate_DateTime { get; set; }
        public string vCreate_DateTime { get; set; }
        public string vDemandClassTitle { get; set; }
        public string vDemandStepTitle { get; set; }
        public string vDemandStatusTitle { get; set; }
        public string vDemandAccNumber { get; set; }
        public string vDemandAccNumberName { get; set; }
        public string vDemandAgentNumber { get; set; }
        public string vDemandAgentName { get; set; }
        public string vDemandTopNumber { get; set; }
        public string vDemandTopName { get; set; }
        public string vDemandManNumber { get; set; }
        public string vDemandManName { get; set; }
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
        public string aryAccountDetail { get; set; }
        public string aryAccountDeptData { get; set; }

        public List<SelectListItem> returnSelectAccountData(string fAccDeptNo, string fRelClass)
        {
            List<oAccountRelation> listAcc = new List<oAccountRelation>();
            listAcc = arModel.listAccountRelation();            
            List<SelectListItem> list = new List<SelectListItem>();
            if (fAccDeptNo != "") { listAcc = listAcc.Where(x => x.oAccDeptNo == fAccDeptNo ).ToList(); }
            if (fRelClass != "") { listAcc = listAcc.Where(x => x.oRelationClass == fRelClass).ToList(); }
            if (listAcc.Count > 0)
            {
                list.Add(new SelectListItem() { Value = "", Text= "請選擇" });
                foreach (oAccountRelation item in listAcc) {
                    list.Add(new SelectListItem() { Value = item.oAccIndex.ToString(), Text = string.Format(@"({2})({0}){1}", item.oAccNo.ToString(), item.oAccName.ToString(), item.oAccIndex.ToString()) });
                }
            }
            return list;
        }

        public DataTable returnDataTableDemandDetail()
        {
            Dictionary<string, object> funDicParas = new Dictionary<string,object>(); 
            DataTable rtnDT = new DataTable(); funQuerySQL = ""; funDicParas = null;
            try {
                funQuerySQL = string.Format(@"Select dd.*, sda.systemtitle as DemandClassTitle 
                                , sdb.SystemTitle as DemandStepTitle, sdc.systemtitle as DemandStatusTitle
                                , isnull(ada.AccNo,'') as AccNumber, isnull(ada.AccName,'') as AccName
                                , isnull(adb.AccNo,'') as AgentNumber, isnull(adb.accname,'') as AgentName
                                , isnull(adc.AccNo,'') as TopNumber, isnull(adc.AccName,'') as TopName
                                , isnull(ade.AccNo,'') as ManNumber, isnull(ade.AccName,'') as ManName
                                from DemandDetail dd 
	                            inner join SystemDataDetail sda on sda.SystemClass='DemandClass' and sda.SystemValue = dd.DemandClass
	                            inner join SystemDataDetail sdb on sdb.SystemClass='DemandStep' and sdb.SystemValue= dd.DemandStep
	                            inner join SystemDataDetail sdc on sdc.SystemClass='DemandStatus' and sdc.SystemValue=dd.DemandStatus
	                            left join AccountDetail ada on ada.AccIndex=dd.DemandAccIndex 
	                            left join AccountDetail adb on adb.AccIndex = dd.DemandAgentIndex
	                            left join AccountDetail adc on adc.AccIndex = dd.DemandTopIndex
	                            left join AccountDetail ade on ade.AccIndex = dd.DemandManIndex");
                rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, funDicParas);
            } catch (Exception ex) {
                rtnDT.Clear();
            }
            return rtnDT;
        }

        public List<oDemandDetail> objDemandDetailData()
        {
            List<oDemandDetail> list = new List<oDemandDetail>();
            try {
                list = (from dt in returnDataTableDemandDetail().AsEnumerable()
                        select new oDemandDetail
                        {
                            oDemandIndex = dt.Field<string>("DemandIndex"), oDemandDate = dt.Field<string>("DemandDate"),
                            oDemandTitle = dt.Field<string>("DemandTitle"), oDemandClass = dt.Field<string>("DemandClass"),
                            oDemandTest = dt.Field<string>("DemandTest"),  oDemandUpload = dt.Field<string>("DemandUpload"),
                            oDemandStep = dt.Field<string>("DemandStep"),  oDemandFrom = dt.Field<string>("DemandFrom"),
                            oDemandProject = dt.Field<string>("DemandProject"), oDemandDateS = dt.Field<string>("DemandDateS"),
                            oDemandDateE = dt.Field<string>("DemandDateE"), oDemandDateH = dt.Field<string>("DemandDateH"),
                            oDemandNotation = dt.Field<string>("DemandNotation"), oDemandRemark = dt.Field<string>("DemandRemark"),
                            oDemandStatus = dt.Field<string>("DemandStatus"), oDemandAccIndex = dt.Field<string>("DemandAccIndex"),
                            oDemandAgentIndex = dt.Field<string>("DemandAgentIndex"), oDemandTopIndex = dt.Field<string>("DemandTopIndex"),
                            oDemandManIndex = dt.Field<string>("DemandManIndex"), oUpdate_DateTime = dt.Field<string>("Update_DateTime"),
                            oCreate_DateTime = dt.Field<string>("Create_DateTime"),
                            oDemandClassTitle = dt.Field<string>("DemandClassTitle"),
                            oDemandStepTitle = dt.Field<string>("DemandStepTitle"),
                            oDemandStatusTitle = dt.Field<string>("DemandStatusTitle"),
                            oDemandAccNumber = dt.Field<string>("AccNumber"),
                            oDemandAccNumberName = dt.Field<string>("AccName"),
                            oDemandAgentNumber = dt.Field<string>("AgentNumber"),
                            oDemandAgentName = dt.Field<string>("AgentName"),
                            oDemandTopNumber = dt.Field<string>("TopNumber"),
                            oDemandTopName = dt.Field<string>("TopName"),
                            oDemandManNumber = dt.Field<string>("ManNumber"),
                            oDemandManName = dt.Field<string>("ManName")
                        }).ToList();
            } catch (Exception ex) {
                list = null;
            }
            return list;
        }


        /*
        public List<oDemandDetail> objDemandDetailData()
        {


        }
        */


        public string returnDemandMaxIndex()
        {
            funReturnValue = ""; funQuerySQL = ""; DataTable rtnDT = new DataTable(); string funMaxIndex = "";
            List<string> fDeclare = new List<string>(); List<object> fValue = new List<object>();
            Dictionary<string,object> dicPara = new Dictionary<string,object>(); dicPara = null;
            try {
                string nowMonth = dbClass.ReturnDetailToNowDateTime("VM");
                funQuerySQL = string.Format(@"select isnull(max(DemandIndex),'') as MaxIndex from DemandDetail 
                             where substring(DemandDate,1,6)='{0}' ", nowMonth);
                rtnDT = dbClass.msDataTableToDataBase(funQuerySQL, dicPara);
                if (rtnDT.Rows.Count > 0)
                {
                    funMaxIndex = rtnDT.Rows[0]["MaxIndex"].ToString();
                    funMaxIndex = nowMonth + Convert.ToInt32(Convert.ToInt32(funMaxIndex.Substring(6, 4)) + 1).ToString().PadLeft(4, '0').ToString();
                } else {
                    funMaxIndex = nowMonth+ "0001";
                }
                funReturnValue = funMaxIndex;
            }
            catch (Exception ex)
            {
                funReturnValue = ex.Message;
            }
            return funReturnValue;
        }

    }


    public class oDemandDetail
    {
        public string oDemandIndex { get; set; }
        public string oDemandDate { get; set; }
        public string oDemandTitle { get; set; }
        public string oDemandClass { get; set; }
        public string oDemandTest { get; set; }
        public string oDemandUpload { get; set; }
        public string oDemandStep { get; set; }
        public string oDemandFrom { get; set; }
        public string oDemandProject { get; set; }
        public string oDemandDateS { get; set; }
        public string oDemandDateE { get; set; }
        public string oDemandDateH { get; set; }
        public string oDemandNotation { get; set; }
        public string oDemandRemark { get; set; }
        public string oDemandStatus { get; set; }
        public string oDemandAccIndex { get; set; }
        public string oDemandAgentIndex { get; set; }
        public string oDemandTopIndex { get; set; }
        public string oDemandManIndex { get; set; }
        public string oUpdate_DateTime { get; set; }
        public string oCreate_DateTime { get; set; }
        public string oDemandClassTitle { get; set; }
        public string oDemandStepTitle { get; set; }
        public string oDemandStatusTitle { get; set; }
        public string oDemandAccNumber { get; set; }
        public string oDemandAccNumberName { get; set; }
        public string oDemandAgentNumber { get; set; }
        public string oDemandAgentName { get; set; }
        public string oDemandTopNumber { get; set; }
        public string oDemandTopName { get; set; }
        public string oDemandManNumber { get; set; }
        public string oDemandManName { get; set; }
    }

    public class oDemandSchedule
    {
        public string oDemandIndex { get; set; }
        public string oDemandStep { get; set; }
        public string oSchAccIndex { get; set; }
        public string oSchNotation { get; set; }
        public string oSchDateTime { get; set; }
        public string oSchStatus { get; set; }
    }

}