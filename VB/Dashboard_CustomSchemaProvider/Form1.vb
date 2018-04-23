Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess
Imports DevExpress.Xpo.DB
Imports DevExpress.DataAccess.Sql

Namespace Dashboard_CustomSchemaProvider
	Partial Public Class Form1
		Inherits RibbonForm
		Public Sub New()
			InitializeComponent()
			dashboardDesigner1.CreateRibbon()

			' Specifies a custom schema provider.
			dashboardDesigner1.CustomDBSchemaProvider = New CustomDBSchemaProvider()

			' Loads a dashboard from an XML file.
			dashboardDesigner1.LoadDashboard("..\..\Data\dashboard.xml")
		End Sub

		' Creates a new class that defines a custom data store schema by implementing the 
		' IDBSchemaProvider interface.
		Private Class CustomDBSchemaProvider
            Implements IDBSchemaProvider
            Public Function GetSchema(ByVal dataConnection As SqlDataConnection, _
                                      ByVal tableList() As String) As DBSchema _
                                  Implements IDBSchemaProvider.GetSchema
                Dim cp = TryCast(dataConnection.ConnectionParameters, Access97ConnectionParameters)
                If cp IsNot Nothing AndAlso cp.FileName = "..\..\Data\nwind.mdb" Then
                    Return GetSchema(dataConnection)
                Else
                    Return dataConnection.GetDBSchema(tableList)
                End If
            End Function
            Public Function GetSchema(ByVal dataConnection As SqlDataConnection) As DBSchema _
                                  Implements IDBSchemaProvider.GetSchema
                Dim cp = TryCast(dataConnection.ConnectionParameters, Access97ConnectionParameters)
                If cp IsNot Nothing AndAlso cp.FileName = "..\..\Data\nwind.mdb" Then

                    ' Adds two tables with required columns to a data store schema.
                    Dim tables(1) As DBTable
                    tables(0) = New DBTable("Categories")
                    tables(0).AddColumn(New DBColumnWithAlias("CategoryName", "Category"))
                    tables(0).AddColumn(New DBColumnWithAlias("CategoryID", "Category ID"))

                    tables(1) = New DBTableWithAlias("Products", "Product list")
                    tables(1).AddColumn(New DBColumnWithAlias("ProductName", "Product"))
                    tables(1).AddColumn(New DBColumnWithAlias("CategoryID", "Category ID"))

                    Dim views(-1) As DBTable
                    Return New DBSchema(tables, views)
                Else
                    Return dataConnection.GetDBSchema()
                End If
            End Function
		End Class
	End Class
End Namespace
