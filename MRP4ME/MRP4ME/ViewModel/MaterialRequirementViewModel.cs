using MRP4ME.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP4ME.Data;

namespace MRP4ME.ViewModel
{
    public class MaterialRequirementViewModel : WorkspaceViewModel
    {
        private DataView salesorders;
        public MaterialRequirementViewModel()
        {
            salesorders = getMaterialRequirementsData();
        }


        public DataView Salesorders
        {
            get
            {
                using (var dbContext = new MRP4MEEntities())
                {
                    salesorders = getMaterialRequirementsData();
                }
                return salesorders;
            }
            set
            {
                NotifyPropertyChanged("Salesorders");
                return;
            }
        }

        public DataView getMaterialRequirementsData()
        {
            DataView vw = new DataView();
            DataSet myGroovySet = new DataSet();
            using (var dbContext = new MRP4MEEntities())
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.Database.Connection.ConnectionString))
                {
                    String sql = "SELECT customer_name, required_date, so_number, item_code, item_id, " +
                        " parent_item_id, name, unit_cost, description, quantity, back_ordered, quantity_received" +
                        " FROM Test_Sales_Orders";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                    adapter.Fill(myGroovySet);
                    myGroovySet.Relations.Add("rsParentChild",
                    myGroovySet.Tables[0].Columns["item_id"],
                    myGroovySet.Tables[0].Columns["parent_item_id"], false);

                    //get the default view and then filter based on the parent element being null
                    vw = myGroovySet.Tables[0].DefaultView;
                    vw.RowFilter = "parent_item_id is null"; //see what happens when i am commented out
                    connection.Close();

                }
            }
            return vw;
        }
    }
}
