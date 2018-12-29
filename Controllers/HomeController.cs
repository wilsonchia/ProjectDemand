using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemand.Models;

namespace MvcDemand.Controllers
{
    public class HomeController : Controller
    {

        ClassDataBase dbClass = new ClassDataBase();
        AccountDetailModels adModel = new AccountDetailModels();


        public ActionResult Index()
        {
            if (Session["AccIndex"] == null)
            {
                return Redirect("~/Home/Login");
            } else {
                return View();
            }
            
        }

        public ActionResult Login()
        {
            return View();
        }

        public RedirectResult LogOut()
        {
            Session.Abandon();
            return Redirect("~/Home/Login");
        }

        [HttpPost]
        public RedirectResult LoginSessionValue(FormCollection post)
        {
            Session["AccIndex"] = post["hideAccIndex"].ToString();
            Session["AccNo"] = post["hideAccNo"].ToString();
            Session.Timeout = 240;            
            return Redirect("~/Home/Index");
        }

        public string returnCheckLoginData(string fLoginNo, string fLoginPass)
        {
            string funReturnValue = ""; string fPassValue = "";
            List<oAccountDetail> oAccountDetail = new List<oAccountDetail>();
            oAccountDetail = adModel.listObjAccountDetail();
            if (oAccountDetail.Where(x => x.oAccNo == fLoginNo).ToList() == null) {
                funReturnValue = "X_帳號不正確";                
            } else {
                oAccountDetail = oAccountDetail.Where(x => x.oAccNo == fLoginNo).ToList();
                if (oAccountDetail.Count() > 0) {
                    fPassValue = oAccountDetail[0].oAccPassword.ToString();
                    funReturnValue = fPassValue == fLoginPass ? string.Format(@"O_{0}", oAccountDetail[0].oAccIndex.ToString()) : "X_密碼不正確";                    
                } else {
                    funReturnValue = "X_帳號不正確";                
                }                              
            }            
            return funReturnValue;
        }

        public string funExecutePassword(string execClass, string fAccIndex, string fPassword , string nPassword)
        {
            string funReturnValue = ""; string cPassValue = "";
            
            
            List<oAccountDetail> oAccountDetail = new List<oAccountDetail>();
            oAccountDetail = adModel.listObjAccountDetail();
            if (oAccountDetail.Where(x => x.oAccIndex == fAccIndex).ToList() == null)
            {
                funReturnValue = "X_帳號不存在";
            }
            else
            {
                oAccountDetail = oAccountDetail.Where(x => x.oAccIndex == fAccIndex).ToList();
                if (oAccountDetail.Count() > 0)
                {
                    cPassValue = oAccountDetail[0].oAccPassword.ToString();
                    if (execClass == "C")
                    {
                        funReturnValue = cPassValue == fPassword ? string.Format(@"O_{0}", oAccountDetail[0].oAccIndex.ToString()) : "X_密碼不正確";
                    }
                    else
                    {
                        List<string> PassModDeclare = new List<string>(); List<object> PassModValue = new List<object>();
                        PassModDeclare = new List<string>() { "@AccIndex", "@AccPassword" };
                        PassModValue = new List<object>() { fAccIndex, nPassword };
                        funReturnValue = dbClass.msExecuteDataBase("U", "AccountDetail", 1, PassModDeclare, PassModValue);
                    }
                }
                else
                {
                    funReturnValue = "X_帳號不存在";
                }
            }
             
            return funReturnValue;
        }

    }
}
