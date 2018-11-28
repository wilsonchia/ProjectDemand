using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MvcDemand
{
    public class ClassDataBase
    {

        static string msConnValue = "Data Source=localhost; User ID=admSQL; password=t/6ru8jo3; Initial Catalog=dbDemand";
        static System.Data.SqlClient.SqlConnection msConn = new System.Data.SqlClient.SqlConnection();
        static System.Data.SqlClient.SqlCommand msComm = new System.Data.SqlClient.SqlCommand();
        static System.Data.SqlClient.SqlDataAdapter msDa = new System.Data.SqlClient.SqlDataAdapter();
        static System.Data.DataSet msDs = new System.Data.DataSet();
        static System.Data.DataTable msDT = new System.Data.DataTable();
        static string msRtnValue { get; set; }
        static string msExecuteValue { get; set; }
        static string msQuerySQL { get; set; }
        static string msExecuteSQL { get; set; }
        static Dictionary<string, object> msDicPara { get; set; }        

        public DataTable msDataTableToDataBase(string rtnQuerySQL, Dictionary<string, object> valDicPara)
        {
            msDs.Clear(); msDT.Clear();
            try {
                msConn = new System.Data.SqlClient.SqlConnection(msConnValue); msConn.Open();
                msComm = new System.Data.SqlClient.SqlCommand(rtnQuerySQL, msConn);
                if (valDicPara != null) {
                    foreach (KeyValuePair<string, object> item in valDicPara) {
                        msComm.Parameters.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }
                msDa = new System.Data.SqlClient.SqlDataAdapter(msComm);
                msDa.Fill(msDs, "table"); msDT = msDs.Tables["table"];
            } catch (Exception ex) {
                msDT.Clear();
            } finally {
                msConn.Dispose(); msComm.Dispose();
            }
            return msDT;
        }

        public string msCheckValueToDataBase(string rtnQuerySQL, Dictionary<string, object> valDicPara) {
            msRtnValue = ""; msDT.Clear();
            try {
                msDT = msDataTableToDataBase(rtnQuerySQL, valDicPara);
                msRtnValue = (msDT.Rows.Count > 0) ? "O" : "X";
            }
            catch (Exception ex) {
                msRtnValue = ex.Message;
            }
            return msRtnValue;
        }

        public string msExecuteValueToDataBase(string rtnExecuteSQL, Dictionary<string, object> valDicPara)
        {
            msExecuteValue = "";
            try {
                msConn = new System.Data.SqlClient.SqlConnection(msConnValue); msConn.Open();
                msComm = new System.Data.SqlClient.SqlCommand(rtnExecuteSQL, msConn);
                if (valDicPara != null)
                {
                    foreach (KeyValuePair<string, object> item in valDicPara)
                    {
                        msComm.Parameters.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }
                msExecuteValue =  (msComm.ExecuteNonQuery() == 1) ? "O" : "X";
                msConn.Dispose(); msComm.Dispose();
            } catch (Exception ex) {
                msExecuteValue = ex.Message;
            }
            return msExecuteValue;
        }

        public byte[] rtnByteReadFromFile(string sendHtmlValue) {
            System.IO.FileStream fs = System.IO.File.OpenRead(sendHtmlValue);
            byte[] rtnData = new byte[fs.Length];
            int br = fs.Read(rtnData, 0, rtnData.Length);
            if (br != fs.Length) throw new System.IO.IOException(sendHtmlValue);
            return rtnData;
        }

        public Dictionary<string, object> GetListToNewDictionary(List<string> listDicKey, List<object> listDicValue) {
            msDicPara = new Dictionary<string, object>();
            try {
                if (listDicValue != null) {
                    for (int i = 0; i < listDicKey.Count; i++) { msDicPara.Add(listDicKey[i].ToString(), listDicValue[i]); }
                }
            } catch (Exception ex) {
                msDicPara.Clear();
            }
            return msDicPara;
        }

        public string ReturnDetailToNowDateTime(string ReturnShowClass)
        {
            string ReturnYear, ReturnMonth, ReturnDay, ReturnHour;
            string ReturnMinute, ReturnSecond, ReturnWeekEnd, ReturnShowDate;
            string ReturnWeekEndTitle, ReturnShowDetailValue, ReturnYearC;
            ReturnShowDetailValue = ""; ReturnShowDate = ""; ReturnWeekEndTitle = "";
            ReturnYear = DateTime.Now.Year.ToString();
            ReturnMonth = DateTime.Now.Month.ToString().PadLeft(2, '0');
            ReturnDay = DateTime.Now.Day.ToString().PadLeft(2, '0');
            ReturnHour = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            ReturnMinute = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            ReturnSecond = DateTime.Now.Second.ToString().PadLeft(2, '0');
            ReturnWeekEnd = DateTime.Now.DayOfWeek.GetHashCode().ToString();
            ReturnYearC = Convert.ToString(Convert.ToInt32(ReturnYear) - Convert.ToInt32(1911));

            switch (ReturnWeekEnd)
            {
                case "0": ReturnWeekEndTitle = "（週日）"; break;
                case "1": ReturnWeekEndTitle = "（週一）"; break;
                case "2": ReturnWeekEndTitle = "（週二）"; break;
                case "3": ReturnWeekEndTitle = "（週三）"; break;
                case "4": ReturnWeekEndTitle = "（週四）"; break;
                case "5": ReturnWeekEndTitle = "（週五）"; break;
                case "6": ReturnWeekEndTitle = "（週六）"; break;
            }
            switch (ReturnShowClass)
            {
                case "VF": ReturnShowDate = ReturnYear + ReturnMonth + ReturnDay + ReturnHour + ReturnMinute + ReturnSecond; break;
                case "VD": ReturnShowDate = ReturnYear + ReturnMonth + ReturnDay; break;
                case "VT": ReturnShowDate = ReturnHour + ReturnMinute + ReturnDay; break;
                case "SF": ReturnShowDate = ReturnYear + "/" + ReturnMonth + "/" + ReturnDay + "  " + ReturnHour + ":" + ReturnMinute + ":" + ReturnSecond; break;
                case "SD": ReturnShowDate = ReturnYear + "/" + ReturnMonth + "/" + ReturnDay; break;
                case "ST": ReturnShowDate = ReturnHour + ":" + ReturnMinute + ":" + ReturnSecond; break;
                case "SC": ReturnShowDate = "中華民國" + ReturnYearC + "年" + ReturnMonth + "月" + ReturnDay + "日"; break;
                case "SW": ReturnShowDate = "中華民國" + ReturnYearC + "年" + ReturnMonth + "月" + ReturnDay + "日" + ReturnWeekEndTitle; break;
            }
            ReturnShowDetailValue = ReturnShowDate;
            return ReturnShowDetailValue;
        }

        public string ReturnToRandomValue(string funStrValue, int RanLen)
        {
            string rtnValue = ""; string ranValue = "";
            Random ran = new Random();
            for (int i = 0; i < RanLen; i++) {
                ranValue += ran.Next(0, 9).ToString(); 
            }
            rtnValue = string.Format(@"{0}{1}", funStrValue, ranValue);
            return rtnValue;
        }

        public string msExecuteDataBase(string execClass, string execTableName, int chkLen, List<string> aryDeclareName, List<object> aryDeclareValue)
        {
            string rtnExecValue = ""; string funExecuteSQL = "";
            Dictionary<string, object> funDicPara = new Dictionary<string, object>();
            funDicPara = GetListToNewDictionary(aryDeclareName, aryDeclareValue);
            switch (execClass)
            {
                case "N":
                        funExecuteSQL = string.Format(@" Insert Into {0} Values ( ", execTableName);
                        for (int i = 0; i < aryDeclareName.Count; i++) { funExecuteSQL += string.Format(@" {0}{1}", aryDeclareName[i], (i < aryDeclareName.Count() - 1) ? "," : ""); }
                        funExecuteSQL += string.Format(@" ) ");
                    break;
                case "U":
                        funExecuteSQL = string.Format(@" Update {0} Set ", execTableName);
                        for (int i = chkLen; i < aryDeclareName.Count; i++) {
                            funExecuteSQL += string.Format(@" {0}={1} {2}", aryDeclareName[i].Replace('@', ' '), aryDeclareName[i], (i < aryDeclareName.Count() - 1) ? "," : "");
                        }
                        funExecuteSQL += string.Format(@" where ");
                        for (int i = 0; i < chkLen; i++) {
                            funExecuteSQL += string.Format(@" {0}={1} {2}", aryDeclareName[i].Replace('@', ' '), aryDeclareName[i], (i < chkLen - 1) ? " and " : "");
                        }
                    break;
                case "D":
                        funExecuteSQL = string.Format(@" Delete from {0} where ", execTableName);
                        for (int i = 0; i < aryDeclareName.Count; i++) {
                            funExecuteSQL += string.Format(@" {0}={1} {2}", aryDeclareName[i].Replace('@', ' '), aryDeclareName[i], (i < aryDeclareName.Count() - 1) ? " and " : "");
                        }
                    break;
            }
            SqlConnection conn = new SqlConnection(msConnValue); conn.Open();
            SqlCommand comm = new SqlCommand(funExecuteSQL, conn);
            if (funDicPara != null) {
                foreach (KeyValuePair<string, object> item in funDicPara) {
                    comm.Parameters.AddWithValue(item.Key.ToString(), item.Value.ToString());
                }
            }
            rtnExecValue = (comm.ExecuteNonQuery() == 1) ? "O" : "X";
            return rtnExecValue;
        }

        
    }
}