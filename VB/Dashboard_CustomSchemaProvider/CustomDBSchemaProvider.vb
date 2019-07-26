Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.Xpo.DB
Imports System.Collections.Specialized

Namespace Dashboard_CustomSchemaProvider
	' Creates a new class that defines a custom data store schema by implementing the 
	' IDBSchemaProvider interface.
	Friend Class CustomDBSchemaProvider
		Implements IDBSchemaProviderEx

		Private tables() As DBTable
		Public Sub LoadColumns(ByVal connection As SqlDataConnection, ParamArray ByVal tables() As DBTable) Implements IDBSchemaProviderEx.LoadColumns
			For Each table As DBTable In tables
				If table.Name = "Categories" AndAlso table.Columns.Count = 0 Then
					Dim categoryIdColumn As DBColumn = New DBColumn With {.Name = "CategoryID"}
					table.AddColumn(categoryIdColumn)
					Dim categoryNameColumn As DBColumn = New DBColumn With {.Name = "CategoryName"}
					table.AddColumn(categoryNameColumn)
				End If
				If table.Name = "Products" AndAlso table.Columns.Count = 0 Then
					Dim categoryIdColumn As DBColumn = New DBColumn With {.Name = "CategoryID"}
					table.AddColumn(categoryIdColumn)
					Dim supplierIdColumn As DBColumn = New DBColumn With {.Name = "SupplierID"}
					table.AddColumn(supplierIdColumn)
					Dim productNameColumn As DBColumn = New DBColumn With {.Name = "ProductName"}
					table.AddColumn(productNameColumn)

					Dim foreignKey1 As New DBForeignKey( { categoryIdColumn }, "Categories", CustomDBSchemaProvider.CreatePrimaryKeys("CategoryID"))
					table.ForeignKeys.Add(foreignKey1)
					Dim foreignKey2 As New DBForeignKey( { supplierIdColumn }, "Suppliers", CustomDBSchemaProvider.CreatePrimaryKeys("SupplierID"))
					table.ForeignKeys.Add(foreignKey2)
				End If
				If table.Name = "Suppliers" AndAlso table.Columns.Count = 0 Then
					Dim supplierIdColumn As DBColumn = New DBColumn With {.Name = "SupplierID"}
					table.AddColumn(supplierIdColumn)
					Dim companyNameColumn As DBColumn = New DBColumn With {.Name = "CompanyName"}
					table.AddColumn(companyNameColumn)
				End If
			Next table
		End Sub

		Public Shared Function CreatePrimaryKeys(ParamArray ByVal names() As String) As StringCollection
			Dim collection As New StringCollection()
			collection.AddRange(names)
			Return collection
		End Function

		Public Function GetTables(ByVal connection As SqlDataConnection, ParamArray ByVal tableList() As String) As DBTable() Implements IDBSchemaProviderEx.GetTables
			Dim cp = TryCast(connection.ConnectionParameters, Access97ConnectionParameters)
			If cp IsNot Nothing AndAlso cp.FileName.Contains("nwind.mdb") Then
				If tables IsNot Nothing Then
					Return tables
				End If

				tables = New DBTable(2){}
				tables(0) = New DBTable("Categories")
				tables(1) = New DBTable("Products")
				tables(2) = New DBTable("Suppliers")
			Else
				tables = New DBTable(){}
			End If
			Return tables
		End Function

		Public Function GetViews(ByVal connection As SqlDataConnection, ParamArray ByVal viewList() As String) As DBTable() Implements IDBSchemaProviderEx.GetViews
			Dim views(-1) As DBTable
			Return views
		End Function

		Public Function GetProcedures(ByVal connection As SqlDataConnection, ParamArray ByVal procedureList() As String) As DBStoredProcedure() Implements IDBSchemaProviderEx.GetProcedures
			Dim storedProcedures(-1) As DBStoredProcedure
			Return storedProcedures
		End Function
	End Class
End Namespace
