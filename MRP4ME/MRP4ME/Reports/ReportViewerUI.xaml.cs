using SAPBusinessObjects.WPF.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MRP4ME.Reports
{
    /// <summary>
    /// Interaction logic for ReportViewerUI.xaml
    /// </summary>
    public partial class ReportViewerUI : Window
    {
        public ReportViewerUI()
        {
            InitializeComponent();
            crystalReportsViewer.ToggleSidePanel = Constants.SidePanelKind.None;
            var sidepanel = crystalReportsViewer.FindName("btnToggleSidePanel") as ToggleButton;
            if (sidepanel != null)
            {
                crystalReportsViewer.ViewChange += (x, y) => sidepanel.IsChecked = false;
            }
        }

        public void setReportSource(CrystalDecisions.CrystalReports.Engine.ReportDocument aReport)
        {
            this.crystalReportsViewer.ViewerCore.ReportSource = aReport;
        }
    }
}
