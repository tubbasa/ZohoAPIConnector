using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZohoConnectorApplicationUI.Models;
using Connector.Data.Model;
using Connector.Data.Model.ApiStructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZohoConnectorApplicationUI.Models.UIModels;
using Connector.Core.Infastructure;

namespace ZohoConnectorApplicationUI.Controllers
{
    public class HomeController : Controller
    {
        IRecordRepository record;
        public HomeController(IRecordRepository _record)
        {
            record = _record;

        }
        public IActionResult Index()
        {

            return View();
        }

        /*
                     //List<Record> list = new List<Record>();
            //list.Add(new Record {Id=1,CreatedBy="burcu",Email="borcuko@hotmail.com" ,FirstName="Burcu",LastName="Acar",LeadOwner="burco",Owner="burcu"});
            //list.Add(new Record { Id = 1, CreatedBy = "tuba", Email = "tuba@hotmail.com", FirstName = "tuba", LastName = "saday", LeadOwner = "tuba", Owner = "tuba" });
            //record.insertRecords(list);
            //record.searchRecords("(((Last Name:Steve)AND(Company:Zillum))OR(Lead Status:Contacted))");
            //record.getRecords("newFormat=1&authtoken=sbdjEDBDJ445791323&scope=crmapi&fromIndex=20&toIndex=200&sortColumnString=Account Name&sortOrderString=desc");
            // Record rd = new Record();
            // rd.Id = 1;rd.LastName = "Acikel";rd.LeadSource = "Exriz";
            // List<Record> list = new List<Record>();
            //var resp= record.updateRecords(rd);

            Record rec = new Record();
            //https://crm.zoho.com/crm/private/xml/Leads/getRecordById?authtoken=AuthToken&scope=crmapi&id=2000000022020&selectColumns=Leads(Lead Owner,First Name,Last Name,Email,Company,No of Employees,Annual Revenue,Created By,Created Time) sadece Id gelecek on taraftan ve biz bunu record nesnesiyle tanımladık 
            rec.Id = 2000000022020;
            var resp=record.getRecordById(rec, "Lead Owner,First Name,Last Name,Email,Company,No of Employees,Annual Revenue,Created By,Created Time");
             */

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult RecordList()
        {
            /*getrecords*/
            List<SelectListItem> SortingFilter = new List<SelectListItem>();
            SortingFilter.Add(new SelectListItem { Text = "Sıralama Ölçüsü Seçiniz", Value = "-1", Selected = true });
            SortingFilter.Add(new SelectListItem { Text = "Artan", Value = "0", Selected = false });
            SortingFilter.Add(new SelectListItem { Text = "Azalan", Value = "", Selected = false });
     
            TempData["SortingFilter"] = SortingFilter;
        
            return View();
        }

        [HttpPost]
        public IActionResult RecordList(ApiParameters param)
        {
            
            if(param.sortOrderString=="0")
            {
                param.sortOrderString = "asc";                
            }
            else
            {
                param.sortOrderString = "desc";
            }
    
            var model = record.getRecords("fromIndex="+param.fromIndex+ "&toIndex="+param.toIndex+ "&sortOrderString="+ param.sortOrderString);

            TempData["recordList"] = model;

            return View();
        }
      
        public IActionResult RecordById()
        {
              var specific = new List<SelectListItem>();
            string [] speclist = {"Lead Owner", "First Name", "Last Name", "Email", "Created By", "Created Time"};
            foreach (var item in speclist)
            {
                specific.Add(new SelectListItem
                {
                    Text = item,
                    Value =item
                });
            }
          
            ViewBag.SpecificList = specific;

            return View();
        }

        [HttpPost]       
        public IActionResult RecordById(RecordByIdModel obj)
        {
            string selection= "";
            for (int i = 0; i < obj.Value.Count; i++)
            {
                selection += obj.Value[i];
                if (i!=obj.Value.Count-1)
                {
                    selection += ",";
                }
            }
            Record newrecord = new Record();
            newrecord.Id = Convert.ToInt64(obj.Id);
            var model = record.getRecordById(newrecord, selection);
            obj.Record = model;
            obj.SelectionValue = selection;
            return View(obj);
        }



        public IActionResult InsertRecord()
        {
           ViewBag.succed = 0;
            return View();
        }

        [HttpPost]
        public IActionResult InsertRecord(Record obj)
        {
            List<Record> recordList = new List<Record>();
            recordList.Add(obj);
            var model = record.insertRecords(recordList);
            int succeed = 0;
            foreach (var item in model)
            {
                if (item.Id!=0)
                {
                    succeed++;
                }
            }

         ViewBag.succed = succeed;
            return View();
        }



        public IActionResult UpdateRecord(string id)
        {
            string selection = "Lead Owner,First Name,Last Name,Email,Created By,Created Time";
            Record newrecord = new Record();
            newrecord.Id = Convert.ToInt64(id) ;
            ViewBag.succed = 0;
            var selectedRecord = record.getRecordById(newrecord, selection);
            return View(selectedRecord);
        }

        [HttpPost]
        public IActionResult UpdateRecord(Record obj)
        {
            if (ModelState.IsValid)
            {
                var result = record.updateRecords(obj);
                int succed = 1;
                foreach (var item in result)
                {
                    if (item.Id != 0)
                    {
                        succed++;
                    }
                }
                ViewBag.succed = succed;
                return View();
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult deleteRecord(long id)
        {
            var Id = id.ToString();
            record.deleteRecords(Id);
            TempData["result"] = 1;
            return RedirectToAction("RecordList", "Home");

        }


      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
