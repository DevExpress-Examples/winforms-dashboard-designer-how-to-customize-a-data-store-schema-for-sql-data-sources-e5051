Imports System.Collections.Specialized
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.Xpo.DB
Imports DevExpress.XtraBars.Ribbon

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

            Public Function GetSchema(ByVal connection As SqlDataConnection, _
                                      ByVal schemaLoadingMode As SchemaLoadingMode) As DBSchema _
                                  Implements IDBSchemaProvider.GetSchema
                Dim cp = TryCast(connection.ConnectionParameters, Access97ConnectionParameters)
                If cp IsNot Nothing AndAlso cp.FileName = "..\..\Data\nwind.mdb" Then

                    Dim tables() As DBTable
                    If schemaLoadingMode.HasFlag(schemaLoadingMode.TablesAndViews) Then
                        ' Creates two tables with required columns to be added to a data store schema.
                        tables = New DBTable(1) {}
                        Dim categoriesTable As New DBTable("Categories")
                        Dim categoryNameColumn1 As DBColumn = New DBColumn With {.Name = "CategoryName"}
                        categoriesTable.AddColumn(categoryNameColumn1)
                        Dim categoryIdColumn1 As DBColumn = New DBColumn With {.Name = "CategoryID"}
                        categoriesTable.AddColumn(categoryIdColumn1)
                        tables(0) = categoriesTable

                        Dim productsTable As New DBTable("Products")
                        Dim productNameColumn2 As DBColumn = New DBColumn With {.Name = "ProductName"}
                        productsTable.AddColumn(productNameColumn2)
                        Dim categoryIdColumn2 As DBColumn = New DBColumn With {.Name = "CategoryID"}
                        productsTable.AddColumn(categoryIdColumn2)
                        tables(1) = productsTable

                        ' Creates a foreign key for the 'Products' table that points to the 'CategoryID' 
                        ' column in the 'Categories' table.
                        Dim foreignKey As New DBForeignKey({categoryIdColumn2}, categoriesTable.Name,
                                                           CustomDBSchemaProvider.CreatePrimaryKeys(categoryIdColumn1.Name))
                        productsTable.ForeignKeys.Add(foreignKey)
                    Else
                        tables = New DBTable() {}
                    End If

                    Dim views(-1) As DBTable
                    Return New DBSchema(tables, views)
                Else
                    Return connection.GetDBSchema()
                End If
            End Function

            Public Sub LoadColumns(ByVal connection As SqlDataConnection,
                                   ByVal ParamArray tables() As DBTable) _
                               Implements IDBSchemaProvider.LoadColumns
                connection.LoadDBColumns(tables)
            End Sub

            Public Shared Function CreatePrimaryKeys(ByVal ParamArray names() As String) As StringCollection 
                Dim collection As New StringCollection()
                collection.AddRange(names)
                Return collection
            End Function
        End Class
    End Class
End Namespace
