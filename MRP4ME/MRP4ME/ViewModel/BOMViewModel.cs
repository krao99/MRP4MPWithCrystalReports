using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP4ME.Data;
using MRP4ME.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using System.Windows;
//using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using ValidationToolkit;
using System.Diagnostics;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Threading;

namespace MRP4ME.ViewModel
{
    /// <summary>
    /// A UI-friendly wrapper for a Customer object.
    /// </summary>
    public class BOMViewModel : WorkspaceViewModel, INotifyDataErrorInfo
    {
        // Constraints
        public const string Constraint_Mandatory = "IsMandatory";
        public const string Constraint_MustBeNonNegative = "NonNegative";
        public const string Constraint_ValidatePositiveInteger = "ValidatePositiveInteger";
        public const string Constraint_ValidateDecimal = "ValidateDecimal";
        public const string Constraint_ValidatePercent = "ValidatePercent";
        public const string Constraint_InvalidDate = "InvalidDate";

        #region Fields

        RelayCommand _saveCommand;

        BOMModel bomForDB = new BOMModel();
        bool isNewBOM = true;


        #endregion // Fields


        private ObservableCollection<ItemType> _itemTypes = new ObservableCollection<ItemType>
        {
            new ItemType { Name = "Inventory Part"},
            new ItemType { Name = "Set Up"},
            new ItemType { Name = "Inventory Assembly"},
            new ItemType { Name = "Non-Inventory"},
        };

        public IEnumerable<ItemType> ItemTypes
        {
            get
            {
                return _itemTypes;
            }
        }

        public IEnumerable<string> ECOTypes
        {
            get
            {
                return new List<string> {"Yes", "No" };
            }
        }
        #region Constructor

        public BOMViewModel(int BOMId)
        {
            base.ErrorsChanged += OnErrorsChanged;
            this.ThisIsEnabled = true;

            //this.ItemTypes = new List<string>() { "London", "Birmingham", "Glasgow" };

            if (!String.IsNullOrEmpty(ParentBOMId))
            {
                isNewBOM = false;
                PopulateBOM(BOMId);
            }
            else
            {
                this.EffectiveFrom = DateTime.Today;
                this.EffectiveThrough = DateTime.Today;
            }
        }

        #endregion // Constructor

        #region BOM Properties

        [Required(AllowEmptyStrings = false)]
        public string ItemNumber
        {
            get { return bomForDB.item_number; }
            set
            {
                if (value == bomForDB.item_number)
                    return;

                bomForDB.item_number = value;
                ValidateProperty("ItemNumber");
                NotifyPropertyChanged("ItemNumber");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string ItemCode
        {
            get { return bomForDB.item_code; }
            set
            {
                if (value == bomForDB.item_code)
                    return;

                bomForDB.item_code = value;
                ValidateProperty("ItemCode");
                NotifyPropertyChanged("ItemCode");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string Version
        {
            get { return bomForDB.version; }
            set
            {
                if (value == bomForDB.version)
                    return;

                bomForDB.version = value;
                ValidateProperty("Version");
                NotifyPropertyChanged("Version");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string Component
        {
            get { return bomForDB.component; }
            set
            {
                if (value == bomForDB.component)
                    return;

                bomForDB.component = value;
                ValidateProperty("Component");
                NotifyPropertyChanged("Component");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string ComponentDescription
        {
            get { return bomForDB.component_description; }
            set
            {
                if (value == bomForDB.component_description)
                    return;

                bomForDB.component_description = value;
                ValidateProperty("ComponentDescription");
                NotifyPropertyChanged("ComponentDescription");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string SelectedItemType
        {
            get { return bomForDB.type; }
            set
            {
                if (value == bomForDB.type)
                    return;

                bomForDB.type = value;
                ValidateProperty("SelectedItemType");
                NotifyPropertyChanged("SelectedItemType");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string QuantityPer
        {
            get { return bomForDB.quantity_per; }
            set
            {
                if (value == bomForDB.quantity_per)
                    return;

                bomForDB.quantity_per = value;
                ValidateProperty("QuantityPer");
                NotifyPropertyChanged("QuantityPer");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string UnitOfMeasure
        {
            get { return bomForDB.unit_of_measure; }
            set
            {
                if (value == bomForDB.unit_of_measure)
                    return;

                bomForDB.unit_of_measure = value;
                ValidateProperty("UnitOfMeasure");
                NotifyPropertyChanged("UnitOfMeasure");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public DateTime EffectiveFrom
        {
            get
            {
                return bomForDB.effective_from;
            }
            set
            {
                if (value == bomForDB.effective_from)
                    return;

                bomForDB.effective_from = value;
                ValidateProperty("EffectiveFrom");
                NotifyPropertyChanged("EffectiveFrom");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public DateTime EffectiveThrough
        {
            get
            {
                return bomForDB.effective__through;
            }
            set
            {
                if (value == bomForDB.effective__through)
                    return;

                bomForDB.effective__through = value;

                ValidateProperty("EffectiveThrough");
                NotifyPropertyChanged("EffectiveThrough");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string EngineeringChangeOrder
        {
            get { return bomForDB.engineering_change_order; }
            set
            {
                if (bomForDB.engineering_change_order == value)
                    return;

                bomForDB.engineering_change_order = value;
                ValidateProperty("EngineeringChangeOrder");
                NotifyPropertyChanged("EngineeringChangeOrder");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string CostOfUnitOfMeasure
        {
            get { return bomForDB.cost_of_unit_of_measure; }
            set
            {
                if (value == bomForDB.cost_of_unit_of_measure)
                    return;

                bomForDB.cost_of_unit_of_measure = value;
                ValidateProperty("CostOfUnitOfMeasure");
                NotifyPropertyChanged("CostOfUnitOfMeasure");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string BOMCost
        {
            get { return bomForDB.bom_cost; }
            set
            {
                if (value == bomForDB.bom_cost)
                    return;

                bomForDB.bom_cost = value;
                ValidateProperty("BOMCost");
                NotifyPropertyChanged("BOMCost");
            }
        }

        [Required(AllowEmptyStrings = false)]
        public string ScrapPercent
        {
            get
            {
                return bomForDB.scrap_percent;
            }
            set
            {
                if (value == bomForDB.scrap_percent)
                    return;

                bomForDB.scrap_percent = value;
                ValidateProperty("ScrapPercent");
                NotifyPropertyChanged("ScrapPercent");
            }
        }

        
        public string ScrapCost
        {
            get { return bomForDB.scrap_cost; }
            set
            {
                if (value == bomForDB.scrap_cost)
                    return;

                bomForDB.scrap_cost = value;
                NotifyPropertyChanged("ScrapCost");
            }
        }

        public string ExtendedCost
        {
            get { return bomForDB.extended_cost; }
            set
            {
                if (bomForDB.extended_cost == value)
                    return;

                bomForDB.extended_cost = value;
                NotifyPropertyChanged("ExtendedCost");
            }
        }

        public string ParentBOMId
        {
            get { return bomForDB.parent_bom_id; }
            set
            {
                if (value == bomForDB.parent_bom_id)
                    return;

                bomForDB.component = value;
                ValidateProperty("ParentBOMId");
                NotifyPropertyChanged("ParentBOMId");
            }
        }
        #endregion // Sales Order Properties

        #region Presentation Properties
        bool _thisIsEnabled;
        public bool ThisIsEnabled
        {
            get { return _thisIsEnabled; }

            set
            {
                if (_thisIsEnabled == value)
                {
                    return;
                }

                _thisIsEnabled = value;
                NotifyPropertyChanged("ThisIsEnabled");
            }
        }

        /// <summary>
        /// Returns a command that saves the sales order.
        /// </summary>
        public ICommand SaveBOMCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand((object z) =>
                    {
                        try
                        {
                            Save();
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    },
                    CanExecute);
                }
                return _saveCommand;
            }
        }

        #endregion Presentation Properties

        public BOMModel BOM
        {
            get { return new BOMModel(); }
            set
            {
                if (value == bomForDB)
                    return;

                bomForDB = value;
            }
        }

        #region Public Methods

        private void PopulateBOM(int BOMId)
        {
            bom_table _bom = new bom_table();

            using (var dbContext = new MRP4MEEntities())
            {
                _bom = dbContext.bom_table.Find(BOMId);

                if (!String.IsNullOrEmpty(_bom.item_number))
                {
                    this.ThisIsEnabled = false;
                    this.ItemNumber = _bom.item_number;
                    this.ItemCode = _bom.item_code;
                    this.Version = _bom.version.ToString();
                    this.Component = _bom.component;
                    this.ComponentDescription = _bom.component_description;
                    this.SelectedItemType = _bom.type;
                    this.QuantityPer = _bom.quantity_per.ToString();
                    this.UnitOfMeasure = _bom.unit_of_measure;
                    this.EffectiveFrom = _bom.effective_from;
                    this.EffectiveThrough = _bom.effective_through;
                    this.EngineeringChangeOrder = _bom.engineering_change_order.ToString();
                    this.CostOfUnitOfMeasure = _bom.cost_of_unit_of_measure.ToString();
                    this.BOMCost = _bom.bom_cost.ToString();
                    this.ScrapPercent = _bom.scrap_percent.ToString();
                    this.ScrapCost = _bom.scrap_cost.ToString();
                    this.ExtendedCost = _bom.extended_cost.ToString();
                }
            }
        }

        /// <summary>
        /// Saves the BOM to the DB.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            try
            {
                bom_table _bom;

                using (var dbContext = new MRP4MEEntities())
                {
                    _bom = dbContext.bom_table.Find(bomForDB.bom_id);

                    if (_bom != null)
                    { //check for duplicate Item Number when new BOM
                        if (isNewBOM)
                            throw new DbUpdateException();
                    }
                    else
                    { //if BOM is null then it is new BOM
                        _bom = new bom_table();
                    }

                    DateTime dateValueEffFrom = DateTime.Parse(this.EffectiveFrom.ToString());
                    DateTime dateValueEffTo = DateTime.Parse(this.EffectiveThrough.ToString());
                    _bom.item_number = bomForDB.item_number;
                    _bom.item_code = bomForDB.item_code;
                    _bom.version = Int32.Parse(bomForDB.version);
                    _bom.component = bomForDB.component;
                    _bom.component_description = bomForDB.component_description;
                    _bom.type = bomForDB.type;
                    _bom.quantity_per = Convert.ToSingle(bomForDB.quantity_per);
                    _bom.unit_of_measure = bomForDB.unit_of_measure;
                    _bom.effective_from = DateTime.Parse(dateValueEffFrom.ToString("yyyy/MM/dd"));
                    _bom.effective_through = DateTime.Parse(dateValueEffTo.ToString("yyyy/MM/dd"));
                    _bom.engineering_change_order = this.EngineeringChangeOrder == "Yes"? true : false;
                    _bom.cost_of_unit_of_measure = Convert.ToDecimal(bomForDB.cost_of_unit_of_measure);
                    _bom.scrap_percent = Convert.ToDecimal(bomForDB.scrap_percent);
                    _bom.scrap_cost = Convert.ToDecimal(bomForDB.scrap_cost);
                    _bom.extended_cost = Convert.ToDecimal(bomForDB.extended_cost);

                    if (isNewBOM)
                    {
                        dbContext.bom_table.Add(_bom);
                        dbContext.SaveChanges();
                        dbContext.Entry(_bom).State = EntityState.Detached;
                    }
                    else
                    {
                        dbContext.SaveChanges();
                        dbContext.Entry(_bom).State = EntityState.Modified;
                        dbContext.Entry(_bom).State = EntityState.Detached;
                    }
                }
                ClearForm();

                MessageBox.Show("BOM successfully saved.", "MRP4ME");
                //NotifyPropertyChanged("SO");
            }
            catch (DbUpdateException ex)
            {
                //MessageBox.Show("SO Number already exist!" , "MRP4ME");
                AddError(new ValidationError("BOM", Constraint_Mandatory, " Item Number already exist!"));
                NotifyPropertyChanged("ItemNumber");
                Tracer.LogUserDefinedValidation("DbUpdateException. " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("BOM not saved, please verify data and save again." + ex.Message, "MRP4ME");
            }
        }

        private void ClearForm()
        {
            this.ItemNumber = "";
            this.ItemCode = "";
            this.Version = "";
            this.Component = "";
            this.ComponentDescription = "";
            this.SelectedItemType = "";
            this.QuantityPer = "";
            this.UnitOfMeasure = "";
            this.EngineeringChangeOrder = "";
            this.CostOfUnitOfMeasure = "";
            this.BOMCost = "";
            this.ScrapPercent = "";
            this.ScrapCost = "";
            this.ExtendedCost = "";
            base.errors.Clear();
            base.ErrorsChanged += OnErrorsChanged;
            NotifyPropertyChanged("ItemNumber");
            NotifyPropertyChanged("ItemCode");
            NotifyPropertyChanged("Version");
            NotifyPropertyChanged("Component");
            NotifyPropertyChanged("ComponentDescription");
            NotifyPropertyChanged("ItemType");
            NotifyPropertyChanged("QuantityPer");
            NotifyPropertyChanged("UnitOfMeasure");
            NotifyPropertyChanged("EngineeringChangeOrder");
            NotifyPropertyChanged("CostOfUnitOfMeasure");
            NotifyPropertyChanged("BOMCost");
            NotifyPropertyChanged("ScrapPercent");
            NotifyPropertyChanged("ScrapCost");
            NotifyPropertyChanged("ExtendedCost");

        }

        

       

        #endregion // Public Methods

        #region Private Helpers

        public bool CanExecute(object z)
        {
            //int num1;
            return isBOMValid()
                   && ErrorCount == 0;
        }

        private void CalculateFields()
        {
            decimal scrapPct = 0;
            decimal bomCost = 0;
            decimal extCost = 0;
            decimal scrapCost = 0;

            if (GetPropertyErrors("ScrapPercent") == null )
	            scrapPct = Convert.ToDecimal(this.ScrapPercent);

            if (GetPropertyErrors("BOMCost") == null )
	            bomCost = Convert.ToDecimal(this.BOMCost);

            if(bomCost > 0) 
            {
	            if(scrapPct > 0) 
		            scrapCost = bomCost * (scrapPct/100);
                else
                    this.ScrapCost = "";
	
	            if(scrapCost > 0) 
		            this.ScrapCost = scrapCost.ToString();
	            else 
		            this.ScrapCost = "";

                extCost = bomCost + scrapCost;
                if(extCost > 0) 
		            this.ExtendedCost = extCost.ToString();
	            else 
		            this.ExtendedCost = "";

            } 
            else 
            {
	            this.ScrapCost = "";
	            this.ExtendedCost = "";
            }
            

        }

        #endregion // Private Helpers

        #region validation

        public bool isBOMValid()
        {
            bool validBOM = true;

            if (String.IsNullOrEmpty(this.ItemNumber))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.ItemCode))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.Version))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.Component))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.ComponentDescription))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.SelectedItemType))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.QuantityPer))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.UnitOfMeasure))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.EffectiveFrom.ToString()))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.EffectiveThrough.ToString()))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.EngineeringChangeOrder))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.CostOfUnitOfMeasure))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.BOMCost))
            {
                validBOM = false;
            }
            else if (String.IsNullOrEmpty(this.ScrapPercent))
            {
                validBOM = false;
            }
            

            return validBOM;
        }

        private void ValidatePositiveInteger(string x, string fieldName)
        {
            long num1;
            RemoveError(fieldName, Constraint_ValidatePositiveInteger);

            if (String.IsNullOrEmpty(x))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePositiveInteger, " is required."));
            }
            else if (!long.TryParse(x, out num1))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePositiveInteger, " must be number."));
            }
            else if (Convert.ToInt64(x) < 0)
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePositiveInteger, " must be non-negative."));
            }
        }

        private void ValidateDecimal(string x, string fieldName)
        {
            decimal num1;
            RemoveError(fieldName, Constraint_ValidateDecimal);
            if (String.IsNullOrEmpty(x))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidateDecimal, " is required."));
            }
            else if (!decimal.TryParse(x, out num1))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidateDecimal, " must be number."));
            }
            else if (Convert.ToDecimal(x) < 0)
            {
                AddError(new ValidationError(fieldName, Constraint_ValidateDecimal, " must be non-negative."));
            }
        }

        private bool ValidatePercent(string x, string fieldName)
        {
            decimal num1;
           
            RemoveError(fieldName, Constraint_ValidatePercent);
            bool isValid = true;
            if (String.IsNullOrEmpty(x))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePercent, " is required."));
                isValid = false;
            }
            else if (!decimal.TryParse(x, out num1))
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePercent, " must be number."));
                isValid = false;
            }
            else if (Convert.ToDecimal(x) < 0)
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePercent, " must be non-negative."));
                isValid = false;
            }
            else if (Convert.ToDecimal(x) > 100)
            {
                AddError(new ValidationError(fieldName, Constraint_ValidatePercent, " must be less than 100."));
                isValid = false;
            }
            
            return isValid;
        }

        private void ValidateDate(DateTime dt, string fieldName)
        {
            DateTime temp;
            RemoveError(fieldName, Constraint_InvalidDate);

            if (String.IsNullOrEmpty(dt.ToString()))
            {
                AddError(new ValidationError(fieldName, Constraint_InvalidDate, " is required."));
            }
            else if (!DateTime.TryParse(dt.ToString(), out temp))
            {
                AddError(new ValidationError(fieldName, Constraint_InvalidDate, " must be a valid date."));
            }
        }

        private void ValidateEmptyString(string x, string fieldName)
        {
            RemoveError(fieldName, Constraint_Mandatory);

            if (String.IsNullOrEmpty(x))
            {
                AddError(new ValidationError(fieldName, Constraint_Mandatory, " is required."));
            }
        }

        public void ValidateProperty(string propertyName)
        {
            Tracer.LogValidation("INotifyDataErrorInfo.ValidateProperty called. Validating " + propertyName);

            switch (propertyName)
            {
                case "ItemNumber":
                    {
                        ValidateEmptyString(this.ItemNumber, propertyName);
                    }
                    break;
                case "ItemCode":
                    {
                        ValidateEmptyString(this.ItemCode, propertyName);
                    }
                    break;
                case "Version":
                    {
                        ValidatePositiveInteger(this.Version, propertyName);
                    }
                    break;
                case "Component":
                    {
                        ValidateEmptyString(this.Component, propertyName);
                    }
                    break;
                case "ComponentDescription":
                    {
                        ValidateEmptyString(this.ComponentDescription, propertyName);
                    }
                    break;
                case "ItemType":
                    {
                        ValidateEmptyString(this.SelectedItemType, propertyName);
                    }
                    break;
                case "QuantityPer":
                    {
                        ValidateDecimal(this.QuantityPer, propertyName);
                    }
                    break;
                case "UnitOfMeasure":
                    {
                        ValidateEmptyString(this.UnitOfMeasure, propertyName);
                    }
                    break;
                case "EffectiveFrom":
                    {
                        ValidateDate(this.EffectiveFrom, propertyName);
                    }
                    break;
                case "EffectiveThrough":
                    {
                        ValidateDate(this.EffectiveThrough, propertyName);
                    }
                    break;
                case "EngineeringChangeOrder":
                    {
                        ValidateEmptyString(this.EngineeringChangeOrder, propertyName);
                    }
                    break;
                case "CostOfUnitOfMeasure":
                    {
                        ValidateDecimal(this.CostOfUnitOfMeasure, propertyName);
                    }
                    break;
                case "BOMCost":
                    {
                        ValidateDecimal(this.BOMCost, propertyName);
                        CalculateFields();
                    }
                    break;
                    
                case "ScrapPercent":
                    {
                        ValidatePercent(this.ScrapPercent, propertyName);
                        CalculateFields();

                    }
                    break;
                case "ScrapCost":
                    {
                        ValidateDecimal(this.ScrapCost, propertyName);
                    }
                    break;
                case "ExtendedCost":
                    {
                        ValidateDecimal(this.ExtendedCost, propertyName);
                    }
                    break;
            }
            if (String.IsNullOrEmpty(propertyName))
            {
                Tracer.LogValidation("No cross-property validation errors.");
            }
        }

        #endregion validation

        #region INotifyErrorDataInfo

        // INotifyErrorDataInfo.
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return base.GetPropertyErrors(propertyName);
        }

        // INotifyErrorDataInfo.
        public bool HasErrors
        {
            get { return ErrorCount != 0; }
        }

        // Helper
        private void RaiseErrorsChanged(string propertyName)
        {
            NotifyErrorsChanged(propertyName);
        }
        #endregion

        #region CurrentValidationError

        // The "CurrentValidationError" property is kept up-to-date with the "latest" error.
        public void OnErrorsChanged(object sender, DataErrorsChangedEventArgs args)
        {
            string propertyName = args.PropertyName;
            Tracer.LogUserDefinedValidation("OnErrorsChanged called. " + GetValidationErrorMessagesAsString());
            NotifyPropertyChanged("CurrentValidationError");
        }

        // Bind target for error bar.
        public virtual ValidationError CurrentValidationError
        {
            get
            {
                if (ErrorCount == 0)
                    return null;

                // Get the error list associated with the last property to be validated.
                Debug.Assert(!String.IsNullOrEmpty(lastPropertyValidated));
                List<ValidationError>.Enumerator p = errors[lastPropertyValidated].GetEnumerator();

                // Decide which error needs to be returned.
                ValidationError error = null;
                while (p.MoveNext())
                {
                    error = p.Current;
                    if (error.ID == "System.Windows.Controls.ExceptionValidationRule")
                        break;
                }
                return error;
            }
        }
        #endregion INotifyErrorDataInfo
    }
    [Serializable]
    public class ItemType : INotifyPropertyChanged
    {
        private string _Name;
        
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
