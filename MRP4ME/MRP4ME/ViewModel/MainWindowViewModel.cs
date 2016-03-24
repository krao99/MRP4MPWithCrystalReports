﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using MRP4ME.Model;
using MRP4ME.Properties;
using MRP4ME.Data;
using System.Windows.Input;
using MRP4ME.Reports;

namespace MRP4ME.ViewModel
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields
        public ICommand HomeCommand { get; set; }
        public ICommand SalesOrderCommand { get; set; }
        public ICommand EditSOCommand { get; set; }
        public ICommand BOMCommand { get; set; }
        public ICommand NewBOMCommand { get; set; }
        public ICommand LocateBOMCommand { get; set; }
        public ICommand PrintBOMCommand { get; set; }

        ReadOnlyCollection<CommandViewModel> _commands;

        ObservableCollection<WorkspaceViewModel> _workspaces;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel()
        {
            this.HomeCommand = new RelayCommand(param => this.ShowHomeView());

            #region  Sales order commands

            this.SalesOrderCommand = new RelayCommand(param => this.CreateNewSalesOrder());
            this.EditSOCommand = new RelayCommand(param => this.EditSalesOrder((string)param));

            #endregion Sales order commands

            #region  BOM commands

            this.BOMCommand = new RelayCommand(param => this.ShowBOMDashBoard());
            this.NewBOMCommand = new RelayCommand(param => this.CreateNewBOM());
            this.LocateBOMCommand = new RelayCommand(param => this.ShowLocateBOM());

            this.PrintBOMCommand = new RelayCommand(param => this.PrintBOM());

            #endregion BOM commands

            this.ShowHomeView();
        }

        #endregion // Constructor

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {

                new CommandViewModel(
                    "Home",
                    new RelayCommand(param => this.ShowHomeView())),
            };
        }

        #endregion // Commands

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        void ShowHomeView()
        {
            bool HomeWorkspaceExist = false;
            //base.DisplayName = "Home";
            HomeViewModel workspace =
                this.Workspaces.FirstOrDefault(vm => vm is HomeViewModel)
                as HomeViewModel;

            if (workspace == null)
            {
                workspace = new HomeViewModel();

                this.Workspaces.Add(workspace);
            }
            else
            {
                if (this.Workspaces.Count > 0)
                {
                    HomeWorkspaceExist = workSpaceExist(Workspaces, "HomeViewModel");
                }

                //if this work space is not exist
                if (!HomeWorkspaceExist)
                {
                    this.Workspaces.Add(workspace);
                    this.SetActiveWorkspace(workspace);
                }
            }
        }

        void CreateNewSalesOrder()
        {
            bool SOWorkspaceExist = false;
            //SalesOrderViewModel Constructor takes SOnumber as param
            //empty string for new sales order

            SalesOrderViewModel workspace = new SalesOrderViewModel("");
            if (this.Workspaces.Count > 0)
            {
                SOWorkspaceExist = workSpaceExist(Workspaces, "SalesOrderViewModel");
            }

            //if this work space is not exist
            if (!SOWorkspaceExist)
            {
                this.Workspaces.Add(workspace);
                this.SetActiveWorkspace(workspace);
            }
        }

        void EditSalesOrder(string SONumber)
        {
            bool SOWorkspaceExist = false;
            //SalesOrderViewModel Constructor takes SOnumber as param
            //SONumber for edit sales order
            SalesOrderViewModel workspace = new SalesOrderViewModel(SONumber);
            if (this.Workspaces.Count > 0)
            {
                SOWorkspaceExist = workSpaceExist(Workspaces, "SalesOrderViewModel");
            }

            //if this work space is not exist
            if (!SOWorkspaceExist)
            {
                this.Workspaces.Add(workspace);
                this.SetActiveWorkspace(workspace);
            }
        }

        void CreateNewBOM()
        {
            bool NewBOMWorkspaceExist = false;
            //SalesOrderViewModel Constructor takes SOnumber as param
            //empty string for new sales order

            BOMViewModel workspace = new BOMViewModel(0);
            if (this.Workspaces.Count > 0)
            {
                NewBOMWorkspaceExist = workSpaceExist(Workspaces, "BOMViewModel");
            }

            //if this work space is not exist
            if (!NewBOMWorkspaceExist)
            {
                this.Workspaces.Add(workspace);
                this.SetActiveWorkspace(workspace);
            }
        }

        void ShowBOMDashBoard()
        {
            bool BOMWorkspaceExist = false;
            //sales_order newSalesOrder = SalesOrder.CreateSalesOrder();

            BOMDashBoardViewModel workspace = new BOMDashBoardViewModel();
            if (this.Workspaces.Count > 0)
            {
                BOMWorkspaceExist = workSpaceExist(Workspaces, "BOMDashBoardViewModel");
            }

            //if this work space is not exist
            if (!BOMWorkspaceExist)
            {
                this.Workspaces.Add(workspace);
                this.SetActiveWorkspace(workspace);
            }
        }

        void PrintBOM()
        {
            // bool BOMWorkspaceExist = false;

            SalesOrder salesorder = new SalesOrder();

            List<SalesOrder> salesOrdersList = salesorder.GetAllSalesOrders();

            if (salesOrdersList.Count > 0)
            {
                ReportViewerUI repWindow = new ReportViewerUI();
                BOMReport soReport = new BOMReport();
                ReportUtility.Display_report(soReport, salesOrdersList, repWindow);
            }

        }

        void ShowLocateBOM()
        {
            bool LocateBOMWorkspaceExist = false;

            LocateBOMViewModel workspace = new LocateBOMViewModel();
            if (this.Workspaces.Count > 0)
            {
                LocateBOMWorkspaceExist = workSpaceExist(Workspaces, "LocateBOMViewModel");
            }

            //if this work space is not exist
            if (!LocateBOMWorkspaceExist)
            {
                this.Workspaces.Add(workspace);
                this.SetActiveWorkspace(workspace);
            }
        }





        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers

        private bool workSpaceExist(ObservableCollection<WorkspaceViewModel> Workspaces, string sWorkSpaceName)
        {
            bool isValid = false;
            for (int i = 0; i < Workspaces.Count; i++)
            {

                if (Workspaces[i].GetType().Name.Equals(sWorkSpaceName))
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

    }
}