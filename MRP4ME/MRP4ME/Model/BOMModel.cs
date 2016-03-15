using MRP4ME.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP4ME.Model
{
    public class BOMModel
    {
        public string type { get; set; }
        public string component { get; set; }
        public string component_description { get; set; }
        public string quantity_per { get; set; }
        public string unit_of_measure { get; set; }
        public System.DateTime effective_from { get; set; }
        public System.DateTime effective__through { get; set; }
        public string cost_of_unit_of_measure { get; set; }
        public string bom_cost { get; set; }
        public string scrap_percent { get; set; }
        public string scrap_cost { get; set; }
        public string extended_cost { get; set; }
        public string version { get; set; }
        public string item_code { get; set; }
        public string item_number { get; set; }
        public string engineering_change_order { get; set; }
        public string bom_id { get; set; }
        public string parent_bom_id { get; set; }

    }

}
