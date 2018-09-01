using Connector.Data.Model.ApiStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZohoConnectorApplicationUI.Models.UIModels
{
    public class SearchModel
    {
      public Record searchedValues { get; set; }

        public List<Record> RecordList { get; set; }

        public string SelectionValue { get; set; }
    }
}
