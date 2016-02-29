using CrystalDecisions.CrystalReports.Engine;
using MRP4ME.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MRP4ME.Reports
{
    public class ReportUtility
    {
        public static void Display_report(ReportClass rc, object objDataSource, Window parentWindow)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewerUI Viewer = new ReportViewerUI();
                
                Viewer.setReportSource(rc);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
