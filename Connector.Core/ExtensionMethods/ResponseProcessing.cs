using Connector.Data.Model.ApiStructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using System.Xml;


namespace Connector.Core.ExtensionMethods
{
    public static class ResponseProcessing
    {
        public static List<Record> getResponse(string obj)
        {
            /*
             <Leads>
            <row no="1">
            <FL val="LEADID">2000000022020</FL>
            <FL val="SMOWNERID">2000000018005</FL>
            <FL val="Lead Owner">John</FL>
            <FL val="Company">Zillium</FL>
            <FL val="First Name">Scott</FL>
            <FL val="Last Name">James</FL>
            <FL val="No of Employees">10</FL>
            <FL val="Annual Revenue">1000.0</FL>
            <FL val="SMCREATORID">2000000016908</FL>
            <FL val="Created By">John</FL>
            <FL val="Created Time">2010-03-16 10:04:52</FL>
            <FL val="Modified Time">2010-03-16 10:04:52</FL>
            </row>
            </Leads>
             */
            List<Record> sonuc;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(obj);
            if (doc.InnerText != "Leads")
            {
                /**result node'u*/
                XmlNode row = doc.FirstChild;
                var rowlist = doc.GetElementsByTagName("row");
                var list = doc.GetElementsByTagName("FL");
                List<Record> recordList = new List<Record>();

                for (int i = 0; i < rowlist.Count; i++)
                {
                    Record rec = new Record();
                    foreach (XmlNode item in rowlist[i].ChildNodes)
                    {


                        var test = item.ChildNodes;


                        var atrText = item.Attributes["val"].Value;
                        switch (atrText)
                        {
                            case "Lead Owner":
                                rec.LeadOwner = item.InnerText;
                                break;
                            case "Owner":
                                rec.Owner = item.InnerText;
                                break;
                            case "First Name":
                                rec.FirstName = item.InnerText;
                                break;
                            case "Last Name":
                                rec.LastName = item.InnerText;
                                break;
                            case "Destination":
                                rec.Destination = item.InnerText;
                                break;
                            case "Email":
                                rec.Email = item.InnerText;
                                break;
                            case "Phone":
                                rec.Phone = item.InnerText;
                                break;
                            case "Mobile":
                                rec.Mobile = item.InnerText;
                                break;
                            case "WebSite":
                                rec.WebSite = item.InnerText;
                                break;
                            case "Lead Source":
                                rec.LeadSource = item.InnerText;
                                break;
                            case "Lead Status":
                                rec.LeadStatus = item.InnerText;
                                break;
                            case "Created By":
                                rec.CreatedBy = item.InnerText;
                                break;
                            case "Created Time":
                                rec.CreatedTime = Convert.ToDateTime(item.InnerText);
                                break;
                            case "Modified Time":
                                rec.ModifiedTime = Convert.ToDateTime(item.InnerText);
                                break;
                            case "LEADID":
                                rec.Id = Convert.ToInt64(item.InnerText);
                                break;
                            default:
                                break;


                        }




                    }
                    recordList.Add(rec);
                }
                sonuc = recordList;
            }
            else
            {
                sonuc = null;
                /*hata döndür*/
            }

            return sonuc;

        }


        public static List<Record> otherResponse(string obj)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(obj);
            XmlNode firstrow = doc.FirstChild;
            XmlNodeList rowList = doc.GetElementsByTagName("row");
            List<Record> recordList = new List<Record>();

            if (firstrow.ToString() == "error")
            {
                recordList.Add(new Record { Id = 0 });


            }
            else
            {
                var rowlist = doc.GetElementsByTagName("row");
                //Dönecek olan xml zoho.com da farklı verildiği için ilk başta bu şekilde bir geri dönüş tiri olarak Record belirlenmişti.Request yapıldığında alınan yanıt : <response uri="/crm/private/xml/Leads/insertRecords">< result > < message > Record(s) added successfully</ message > </ result > </ response>  şeklinde olduğundan; yukarıda yaptığım  error düğümü gelmiyorsa Session içerisinde result olarak başarılı dönüş yapacak şekilde ayarlandı. 

                //for (int i = 0; i < rowList.Count; i++)
                //{


                //    foreach (XmlNode item in rowlist[i])
                //    {

                //        if (item.Name == "success")
                //        {
                //            Record rec = new Record();

                //            var flId = item.ChildNodes[1].ChildNodes[0].InnerText;


                //            //rec.Id = Convert.ToInt64(flId);
                //            //recordList.Add(rec);
                //        }
                //        else
                //        {
                //            Record rec = new Record();
                //            rec.Id = 0;
                //            recordList.Add(rec);
                //        }


                //    }




                //}



                //Session.SetString("responseProcessResult", "Başarılı şekilde eklenmiştir");
                recordList.Add(new Record { Id = 1 });

            }
            return recordList;
        }


    }
}
