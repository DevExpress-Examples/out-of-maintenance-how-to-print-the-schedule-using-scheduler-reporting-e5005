#region #usings
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Reporting;
#endregion #usings
using System;
using System.Windows.Forms;


namespace SchedulerReportingExample
{
    public partial class Form1 : Form
    {
        #region #lastfetchedinterval
        TimeInterval lastFetchedInterval = new TimeInterval();
        #endregion #lastfetchedinterval

        public Form1()
        {
            InitializeComponent();

            schedulerStorage1.AppointmentsChanged += OnApptChangedInsertedDeleted;
            schedulerStorage1.AppointmentsInserted += OnApptChangedInsertedDeleted;
            schedulerStorage1.AppointmentsDeleted += OnApptChangedInsertedDeleted;

            schedulerControl1.GroupType = SchedulerGroupType.Resource;
            schedulerControl1.Start = DateTime.Today;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'scheduleTestDataSet.Resources' table. You can move, or remove it, as needed.
            this.resourcesTableAdapter.Fill(this.scheduleTestDataSet.Resources);
            // TODO: This line of code loads data into the 'scheduleTestDataSet.Appointments' table. You can move, or remove it, as needed.
            this.appointmentsTableAdapter.Fill(this.scheduleTestDataSet.Appointments);
        }

        private void OnApptChangedInsertedDeleted(object sender, PersistentObjectsEventArgs e)
        {
            this.appointmentsTableAdapter.Update(scheduleTestDataSet);
            this.scheduleTestDataSet.AcceptChanges();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            #region #showreport
            XtraSchedulerReport1 xr = new XtraSchedulerReport1();
            SchedulerControlPrintAdapter scPrintAdapter =
                new SchedulerControlPrintAdapter(this.schedulerControl1);
            xr.SchedulerAdapter = scPrintAdapter;
            
            xr.CreateDocument(true);

            using (ReportPrintTool printTool = new ReportPrintTool(xr))
            {
                printTool.ShowRibbonPreviewDialog();
            }
            #endregion #showreport
        }
    }
}
