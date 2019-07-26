<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Dashboard_CustomSchemaProvider/Form1.cs) (VB: [Form1.vb](./VB/Dashboard_CustomSchemaProvider/Form1.vb))
* [CustomDBSchemaProvider.cs](./CS/Dashboard_CustomSchemaProvider/CustomDBSchemaProvider.cs) (VB: [FormCustomDBSchemaProvider1.vb](./VB/Dashboard_CustomSchemaProvider/CustomDBSchemaProvider.vb))
<!-- default file list end -->
# How to: Customize the Data Store Schema to Restrict Fields in Query Builder 

This example demonstrates how to customize a data store schema for a dashboard data source that uses a connection to the _nwind.mdb_ database.

A **CustomDBSchemaProvider** class implements the  [IDBSchemaProviderEx](https://docs.devexpress.com/CoreLibraries/DevExpress.DataAccess.Sql.IDBSchemaProviderEx) interface. It defines a custom data store schema that contains three related tables and eight fields.

Click the _Use Custom DBSchema Provider_ button to assign a CustomDBSchemaProvider instance to the  [DashboardDesigner.CustomDBSchemaProviderEx](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardDesigner.CustomDBSchemaProviderEx) property. 

To see the result, add a new query or edit the existing query. The [Query Builder](https://docs.devexpress.com/Dashboard/117275) window contains only fields and tables that the **CustomDBSchemaProvider** supplies.


![](/images/screenshot.png)
