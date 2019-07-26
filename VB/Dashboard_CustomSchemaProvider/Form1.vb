Imports DevExpress.DashboardWin

Namespace Dashboard_CustomSchemaProvider
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Public Sub New()
			InitializeComponent()
			dashboardDesigner1.CreateRibbon()
			AddHandler dashboardDesigner1.CustomizeDashboardTitle, AddressOf DashboardDesigner1_CustomizeDashboardTitle

			dashboardDesigner1.LoadDashboard("Data\dashboard-with-products.xml")
		End Sub

		Private Sub DashboardDesigner1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As DevExpress.DashboardWin.CustomizeDashboardTitleEventArgs)
			Dim itemSave As New DashboardToolbarItem(Sub(args) UseCustomDBSchemaProvider()) With {.Caption = "Use Custom DBSchema Provider"}
			e.Items.Clear()
			e.Items.Add(itemSave)
		End Sub

		Private Sub UseCustomDBSchemaProvider()
			dashboardDesigner1.CustomDBSchemaProviderEx = New CustomDBSchemaProvider()
		End Sub
	End Class
End Namespace
