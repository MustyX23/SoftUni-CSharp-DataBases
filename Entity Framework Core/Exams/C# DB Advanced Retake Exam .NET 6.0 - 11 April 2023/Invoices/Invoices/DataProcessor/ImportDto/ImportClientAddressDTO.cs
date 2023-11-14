using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportClientAddressDTO
    {
        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}
