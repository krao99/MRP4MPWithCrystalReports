using MRP4ME.Data;
using MRP4ME.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP4ME.Reports
{
    public class ReportsModel
    {
        internal List<SalesOrder> GetAllSalesOrders()
        {
            List<SalesOrder> salesOrdersList = new List<SalesOrder>();

            List<sales_order> _salesorders = new List<sales_order>();
            using (var dbContext = new MRP4MEEntities())
            {
                _salesorders = new List<sales_order>(dbContext.sales_order);
            }
            foreach (var val in _salesorders)
            {
                SalesOrder salesOrderObj = new SalesOrder();
                
                salesOrderObj.required_date = val.required_date;
                salesOrderObj.so_number = val.so_number;
                salesOrderObj.customer_name = val.customer_name;
                salesOrderObj.item_code = val.item_code;
                salesOrderObj.name = val.name;
                if(val.unit_cost > 0)
                    salesOrderObj.unit_cost = string.Format("{0:#0.00######}", val.unit_cost);

                salesOrderObj.description = val.description;
                salesOrderObj.quantity = val.quantity.ToString();
                if(val.quantity_received > 0)
                    salesOrderObj.quantity_received = val.quantity_received.ToString();

                if(val.back_ordered > 0)
                    salesOrderObj.back_ordered = val.back_ordered.ToString();
        
                salesOrdersList.Add(salesOrderObj);

            }



            return salesOrdersList;
        }
    }
}
