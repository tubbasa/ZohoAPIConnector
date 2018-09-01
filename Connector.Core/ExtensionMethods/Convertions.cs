using Connector.Data.Model;
using Connector.Data.Model.ApiResponse;
using Connector.Data.Model.ApiStructure;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Connector.Core.ExtensionMethods
{
    public static class Convertions
    {

        #region convert Record class to ApiParameters 
        public static List<ApiParameters> convertToApiParameters(List<Record> obj, string modelType)
        {
            List<ApiParameters> parameters = new List<ApiParameters>();

            #region convert list of record to xmldata and put apiparameters
            XmlDocument doc = new XmlDocument();
            XmlNode leads = doc.CreateElement("Leads");
            var result = "";
            int counter = 1;
            foreach (var item in obj)
            {
                /*row satırı oluşturuldu*/
                XmlNode rowNode = doc.CreateElement("Row");
                /*row satırının no attribute'ü oluşturuldu*/
                XmlAttribute no = doc.CreateAttribute("no");
                /*row satırının no attribute'üne değer atandı*/
                no.Value = counter.ToString();
                /*row satırına no attribute'ü eklendi*/
                rowNode.Attributes.Append(no);
                
                /*///////////*/
                
                XmlNode leadownernode = doc.CreateElement("FL");
                XmlAttribute leadval = doc.CreateAttribute("val");
                leadval.Value = "Lead Owner";
                leadownernode.InnerText = item.LeadOwner;
                leadownernode.Attributes.Append(leadval);
                rowNode.AppendChild(leadownernode);

                XmlNode ownernode = doc.CreateElement("FL");
                XmlAttribute ownerval = doc.CreateAttribute("val");
                ownerval.Value = "Owner";
                ownernode.InnerText = item.Owner;
                ownernode.Attributes.Append(ownerval);
                rowNode.AppendChild(ownernode);


                XmlNode companynode = doc.CreateElement("FL");
                XmlAttribute companyval = doc.CreateAttribute("val");
                companyval.Value = "Owner";
                companynode.InnerText = item.Company;
                companynode.Attributes.Append(companyval);
                rowNode.AppendChild(companynode);

                XmlNode fNamenode = doc.CreateElement("FL");
                XmlAttribute fNameval = doc.CreateAttribute("val");
                fNameval.Value = "First Name";
                fNamenode.InnerText = item.FirstName;
                fNamenode.Attributes.Append(fNameval);
                rowNode.AppendChild(fNamenode);

                XmlNode lNamenode = doc.CreateElement("FL");
                XmlAttribute lNameval = doc.CreateAttribute("val");
                lNameval.Value = "Last Name";
                lNamenode.InnerText = item.LastName;
                lNamenode.Attributes.Append(lNameval);
                rowNode.AppendChild(lNamenode);


                XmlNode destnode = doc.CreateElement("FL");
                XmlAttribute destval = doc.CreateAttribute("val");
                destval.Value = "Destination";
                destnode.InnerText = item.Destination;
                destnode.Attributes.Append(destval);
                rowNode.AppendChild(destnode);



                XmlNode mailnode = doc.CreateElement("FL");
                XmlAttribute mailval = doc.CreateAttribute("val");
                mailval.Value = "Email";
                mailnode.InnerText = item.Email;
                mailnode.Attributes.Append(mailval);
                rowNode.AppendChild(mailnode);

                XmlNode phonenode = doc.CreateElement("FL");
                XmlAttribute phoneval = doc.CreateAttribute("val");
                phoneval.Value = "Phone";
                phonenode.InnerText = item.Phone;
                phonenode.Attributes.Append(phoneval);
                rowNode.AppendChild(phonenode);


                XmlNode Faxnode = doc.CreateElement("FL");
                XmlAttribute Faxval = doc.CreateAttribute("val");
                Faxval.Value = "Fax";
                Faxnode.InnerText = item.Fax;
                Faxnode.Attributes.Append(Faxval);
                rowNode.AppendChild(Faxnode);
            
                XmlNode Mobilenode = doc.CreateElement("FL");
                XmlAttribute Mobileval = doc.CreateAttribute("val");
                Mobileval.Value = "Mobile";
                Mobilenode.InnerText = item.Mobile;
                Mobilenode.Attributes.Append(Mobileval);
                rowNode.AppendChild(Mobilenode);


                XmlNode WebSitenode = doc.CreateElement("FL");
                XmlAttribute WebSiteval = doc.CreateAttribute("val");
                WebSiteval.Value = "WebSite";
                WebSitenode.InnerText = item.WebSite;
                WebSitenode.Attributes.Append(WebSiteval);
                rowNode.AppendChild(WebSitenode);
                

                XmlNode LeadSourcenode = doc.CreateElement("FL");
                XmlAttribute LeadSourceval = doc.CreateAttribute("val");
                LeadSourceval.Value = "Lead Source";
                LeadSourcenode.InnerText = item.LeadSource;
                LeadSourcenode.Attributes.Append(LeadSourceval);
                rowNode.AppendChild(LeadSourcenode);



                XmlNode LeadStatusnode = doc.CreateElement("FL");
                XmlAttribute LeadStatusval = doc.CreateAttribute("val");
                LeadStatusval.Value = "Lead Status";
                LeadStatusnode.InnerText = item.LeadStatus;
                LeadStatusnode.Attributes.Append(LeadStatusval);
                rowNode.AppendChild(LeadStatusnode);





                //val.Value = "CreatedBy";
                //FL.InnerText = item.CreatedBy;
                //rowNode.AppendChild(FL);

                //val.Value = "CreatedTime";
                //FL.InnerText = item.CreatedTime.ToString();
                //rowNode.AppendChild(FL);

                //val.Value = "ModifiedTime";
                //FL.InnerText = item.ModifiedTime.ToString() ;
                //rowNode.AppendChild(FL);
               
                leads.AppendChild(rowNode);


                counter++;
                result = leads.OuterXml;

                parameters.Add(new ApiParameters { Id = item.Id.ToString(), data = "" });

            }
            foreach (var item in parameters)
            {
                item.data = result;
            }

            doc.AppendChild(leads);
            //string xmlstring = doc.InnerXml;


            #endregion
        
            return parameters;
        }
        public static ApiParameters convertToApiParameters(Record obj)
        {
            ApiParameters param = new ApiParameters();
            param.Id = obj.Id.ToString();
                /*record to apiparameter id sini alacaksın id ye yazacaksın*/
            return param;
        }

        #endregion

        #region convert result to Record
        public static List<Record> convertToRecord(string obj, string modelType)
        {
            List<Record> resultList = new List<Record>();
            if (modelType=="getRecords"||modelType=="getRecordById")
            {
                resultList= ResponseProcessing.getResponse(obj);
            }
            else if (modelType == "updateRecords" || modelType == "insertRecords")
            {
                resultList=ResponseProcessing.otherResponse(obj);
            }


            return resultList;
        }



        #endregion

        #region convert result to Record
        public static List<ApiResponse> convertToApiResponse(string obj, string modelType)
        {
            return new List<ApiResponse> { };
        } /*-*/



        #endregion

        #region convert string to ApiParameters
        public static ApiParameters convertToApiParameters(string obj)
        {
            var parameters = obj.Split("&");
            Dictionary<string, string> dicti = new Dictionary<string, string>();
            foreach (var item in parameters)
            {
                var param = item.Split("=");
                dicti.Add(param[0], param[1]);
            }
            ApiParameters api = new ApiParameters();
            api.fromIndex = dicti["fromIndex"];
            api.toIndex = dicti["toIndex"];
            //api.sortColumnString = dicti["sortColumnString"];
            api.sortOrderString = dicti["sortOrderString"];
            //api.lastModifiedTime =Convert.ToDateTime(dicti["lastModifiedTime"]);

            return api;
        }
        #endregion
    }
}
