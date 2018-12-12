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

        AccountRelationModels arModel = new AccountRelationModels();

        public List<oDemandDetail> objDemandDetail = new List<oDemandDetail>();
        public List<SelectListItem> selAccountDetail = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailAgent = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailTop = new List<SelectListItem>();
        public List<SelectListItem> selAccountDetailMan = new List<SelectListItem>();
        public List<SelectListItem> selDemandClass = new List<SelectListItem>();
        
        public string voDemandIndex { get; set; }
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
}