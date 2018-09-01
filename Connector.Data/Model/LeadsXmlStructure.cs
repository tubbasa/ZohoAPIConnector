using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Connector.Data.Model
{
    public class LeadsXmlStructure
    {
        [XmlRoot(ElementName = "FL")]
        public class FL
        {
            [XmlAttribute(AttributeName = "val")]
            public string Val { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "row")]
        public class Row
        {
            [XmlElement(ElementName = "FL")]
            public List<FL> FL { get; set; }
            [XmlAttribute(AttributeName = "no")]
            public string No { get; set; }
        }

        [XmlRoot(ElementName = "Leads")]
        public class Leads
        {
            [XmlElement(ElementName = "row")]
            public List<Row> Row { get; set; }
        }
    }
}
