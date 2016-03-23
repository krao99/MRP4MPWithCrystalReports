using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP4ME.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;

namespace MRP4ME.ViewModel
{
    public class LocateBOMViewModel : WorkspaceViewModel
    {
        private DataView bomitems;

        public LocateBOMViewModel() { FillItemCodes(); }

        private ObservableCollection<bom_table> _bomitems;
        RelayCommand _locateBOMCommand;
        public string _itemcode;
        private List<string> _itemcodes;

        public List<string> Itemcodes
        {
            get
            {
                return _itemcodes;
            }
            set
            {
                _itemcodes = value;
                NotifyPropertyChanged("Itemcodes");
            }
        }

        public string ItemCode
        {
            get { return _itemcode;}
            set
            {  
                _itemcode = value;
                NotifyPropertyChanged("ItemCode");
            }
        }

        public ObservableCollection<bom_table> BOMItems1
        {
            get
            {
                return _bomitems;
            }
            set
            {
                _bomitems = value;
                NotifyPropertyChanged("BOMItems");
            }
        }

        private void FillItemCodes()
        {
            using (var dbContext = new MRP4MEEntities())
            {
                var q = (from a in dbContext.bom_table
                         select a.item_code).Distinct().ToList();
                this.Itemcodes = q;
            }

            NotifyPropertyChanged("Itemcodes");
        }

        /// <summary>
        /// Returns a command to locate bom.
        /// </summary>
        public ICommand LocateBOMCommand
        {
            get
            {
                if (_locateBOMCommand == null)
                {
                    _locateBOMCommand = new RelayCommand(
                            param => this.GenerateBOMView()
                         );
                }
                return _locateBOMCommand;
            }
        }

        /// <summary>
        /// populate bom from the database.  This method is invoked by the LocateBOMCommand.
        /// </summary>
        public void GenerateBOMGrid()
        {
            using (var dbContext = new MRP4MEEntities())
            {
                var bomitems = new ObservableCollection<bom_table>(dbContext.bom_table.Where(x => x.item_code.Equals(this.ItemCode)));
                this.BOMItems1 = bomitems;
            }

            NotifyPropertyChanged("BOMItems1");

        }



        public DataView BOMItems
        {
            get
            {
                
                return bomitems;
            }
            set
            {
                bomitems = value;
                NotifyPropertyChanged("BOMItems");
                
            }
        }

        public void GenerateBOMView()
        {

            if (String.IsNullOrEmpty(this.ItemCode))
            {
                MessageBox.Show("Please enter Item Code.");
                return;
            }

            DataView vw = new DataView();
            DataSet bomDataSet = new DataSet();
            using (var dbContext = new MRP4MEEntities())
            {
                using (MySqlConnection connection = new MySqlConnection(dbContext.Database.Connection.ConnectionString))
                {
                    try
                    {
                        String sql = "SELECT type, component, component_description as comp_desc, quantity_per, unit_of_measure as uom, effective_from as eff_from, " +
                       " effective_through AS eff_through, if(engineering_change_order=0,'No','Yes') as ECO, cost_of_unit_of_measure cost_uom," +
                       " bom_cost, scrap_percent, scrap_cost, extended_cost, item_code, item_number " +
                       " FROM bom_table";

                        MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                        adapter.Fill(bomDataSet);
                        bomDataSet.Relations.Add("rsParentChild",
                        bomDataSet.Tables[0].Columns["item_number"],
                        bomDataSet.Tables[0].Columns["item_code"], false);

                        vw = bomDataSet.Tables[0].DefaultView;
                        vw.RowFilter = "item_code = '" + this.ItemCode + "'";
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to generate BOM for this Item Code." + ex.Message, "MRP4ME");
                    }
                    finally
                    {
                        if(connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                    

                }
            }
            this.BOMItems = vw;
            NotifyPropertyChanged("BOMItems");
        }
    }
}


    

