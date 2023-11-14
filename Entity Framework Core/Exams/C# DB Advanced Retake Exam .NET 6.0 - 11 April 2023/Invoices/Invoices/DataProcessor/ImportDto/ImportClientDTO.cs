using Invoices.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Client")]
    public class ImportClientDTO
    {
        public string Name { get; set; }

        public string NumberVat { get; set; }

        public ImportClientAddressDTO[] Addresses { get; set; }

    }
}
