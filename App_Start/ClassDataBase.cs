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
        
    }
}