using System;
using System.Collections.Generic;
using System.Text;

namespace Connector.Data.Model.ApiStructure
{
    public class Record
    {
        public long Id { get; set; }
        public string LeadOwner{ get; set; }
        public string Owner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Destination { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string WebSite { get; set; }
        public string LeadSource { get; set; }
        public string LeadStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }

    }
}
