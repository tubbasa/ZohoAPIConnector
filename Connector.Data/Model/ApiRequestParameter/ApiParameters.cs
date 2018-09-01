using Connector.Data.Model.ApiStructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Connector.Data.Model
{
    public class ApiParameters
    {
        public string newFormat { get; set; }
        public string authtoken { get; set; }
        public string scope { get; set; }
        public string fromIndex { get; set; }
        public string toIndex { get; set; }
        public string sortColumnString { get; set; }
        public string sortOrderString { get; set; }
        public string Id { get; set; }
        public string selectColumns { get; set; }
        public List<int> IdList { get; set; }
        public string data { get; set; }
        public int version { get; set; }
        public bool wfTrigger { get; set; }
        public string criteria { get; set; }
        public string selected_attr {get;set; }
        public DateTime lastModifiedTime { get; set; }

    }
}
