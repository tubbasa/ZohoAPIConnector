using Connector.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Connector.Core.ExtensionMethods
{
    public static class ZohoApi
    {
        public static string zohocrmurl = "https://crm.zoho.com/crm/private/xml/";
   
        public static String APIMethod(string modulename, string methodname, string recordId, List<ApiParameters> parameters)
        {
            string uri = zohocrmurl + modulename + "/" + methodname + "?";

            string postContent = "newFormat=1";
            postContent = postContent + "&authtoken={yourAuthToken}&scope=crmapi";//Give your authtoken
            if (methodname.Equals("insertRecords"))
            {
                postContent = postContent + "&xmlData=" + parameters.FirstOrDefault().data;
            }
            if (methodname.Equals("deleteRecords"))
            {
                postContent = postContent + "&id=" + recordId;
            }

            if (methodname.Equals("updateRecords"))
            {
                postContent = postContent + "&id=" + recordId + "&xmlData=" + parameters.FirstOrDefault().data;
            }
                if (methodname.Equals("getRecordById"))
            {
                postContent = postContent + "&scope=crmapi" + "&id=" + recordId+"&selectColumns=Leads"+parameters.FirstOrDefault().selected_attr;

            }
            if (methodname.Equals("getRecords"))
            {
                var parameter = parameters.FirstOrDefault();
                if (parameter.fromIndex!=null)
                {
                    postContent +=  "&fromIndex=" + parameter.fromIndex;
                }
                 if (parameter.toIndex!=null)
                {
                    postContent +=  "&toIndex=" + parameter.toIndex;
                }
                 if (parameter.sortColumnString !=null)
                {
                    postContent += "&sortColumnString=" + parameter.sortColumnString;
                }
                 if (parameter.sortOrderString!=null)
                {
                    postContent += "&sortOrderString=" + parameter.sortOrderString;
                }
                
            }
            string result = AccessCRM(uri, postContent);
            return result;
        }
        public static string AccessCRM(string url, string postcontent)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postcontent);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
           reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
