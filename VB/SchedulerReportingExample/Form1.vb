#Region "#usings"
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Reporting
#End Region ' #usings
Imports System
Imports System.Windows.Forms


Namespace SchedulerReportingExample
    Partial Public Class Form1
        Inherits Form

        #Region "#lastfetchedinterval"
        Private lastFetchedInterval As New TimeInterval()
        #End Region ' #lastfetchedinterval

        Public Sub New()
            InitializeComponent()

            AddHandler schedulerStorage1.AppointmentsChanged, AddressOf OnApptChangedInsertedDeleted
            AddHandler schedulerStorage1.AppointmentsInserted, AddressOf OnApptChangedInsertedDeleted
            AddHandler schedulerStorage1.AppointmentsDeleted, AddressOf OnApptChangedInsertedDeleted

            schedulerControl1.GroupType = SchedulerGroupType.Resource
            schedulerControl1.Start = Date.Today
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' TODO: This line of code loads data into the 'scheduleTestDataSet.Resources' table. You can move, or remove it, as needed.
            Me.resourcesTableAdapter.Fill(Me.scheduleTestDataSet.Resources)
            ' TODO: This line of code loads data into the 'scheduleTestDataSet.Appointments' table. You can move, or remove it, as needed.
            Me.appointmentsTableAdapter.Fill(Me.scheduleTestDataSet.Appointments)
        End Sub

        Private Sub OnApptChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            Me.appointmentsTableAdapter.Update(scheduleTestDataSet)
            Me.scheduleTestDataSet.AcceptChanges()
        End Sub

        Private Sub toolStripButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripButton1.Click
'            #Region "#showreport"
            Dim xr As New XtraSchedulerReport1()
            Dim scPrintAdapter As New SchedulerControlPrintAdapter(Me.schedulerControl1)
            xr.SchedulerAdapter = scPrintAdapter

            xr.CreateDocument(True)

            Using printTool As New ReportPrintTool(xr)
                printTool.ShowRibbonPreviewDialog()
            End Using
'            #End Region ' #showreport
        End Sub
    End Class
End Namespace
