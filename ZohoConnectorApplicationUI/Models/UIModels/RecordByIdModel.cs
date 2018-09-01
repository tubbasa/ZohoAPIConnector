using Connector.Data.Model.ApiStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZohoConnectorApplicationUI.Models.UIModels
{
    public class RecordByIdModel
    {
        public long Id { get; set; }
        public List<string> Value { get; set; }

        public Record Record{ get; set; }

        public string SelectionValue { get; set; }
    }
}
