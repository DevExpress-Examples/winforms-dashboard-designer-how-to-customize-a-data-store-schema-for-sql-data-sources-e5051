using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using System.Collections.Specialized;

namespace Dashboard_CustomSchemaProvider
{
    // Creates a new class that defines a custom data store schema by implementing the 
    // IDBSchemaProvider interface.
    class CustomDBSchemaProvider : IDBSchemaProviderEx
    {
        DBTable[] tables;
        public void LoadColumns(SqlDataConnection connection, params DBTable[] tables)
        {
            foreach (DBTable table in tables)
            {
                if (table.Name == "Categories" && table.Columns.Count == 0)
                {
                    DBColumn categoryIdColumn = new DBColumn { Name = "CategoryID" };
                    table.AddColumn(categoryIdColumn);
                    DBColumn categoryNameColumn = new DBColumn { Name = "CategoryName" };
                    table.AddColumn(categoryNameColumn);
                }
                if (table.Name == "Products" && table.Columns.Count == 0)
                {
                    DBColumn categoryIdColumn = new DBColumn { Name = "CategoryID" };
                    table.AddColumn(categoryIdColumn);
                    DBColumn supplierIdColumn = new DBColumn { Name = "SupplierID" };
                    table.AddColumn(supplierIdColumn);
                    DBColumn productNameColumn = new DBColumn { Name = "ProductName" };
                    table.AddColumn(productNameColumn);

                    DBForeignKey foreignKey1 = new DBForeignKey(
                        new[] { categoryIdColumn },
                        "Categories",
                        CustomDBSchemaProvider.CreatePrimaryKeys("CategoryID"));
                    table.ForeignKeys.Add(foreignKey1);
                    DBForeignKey foreignKey2 = new DBForeignKey(
                        new[] { supplierIdColumn },
                        "Suppliers",
                        CustomDBSchemaProvider.CreatePrimaryKeys("SupplierID"));
                    table.ForeignKeys.Add(foreignKey2);
                }
                if (table.Name == "Suppliers" && table.Columns.Count == 0)
                {
                    DBColumn supplierIdColumn = new DBColumn { Name = "SupplierID" };
                    table.AddColumn(supplierIdColumn);
                    DBColumn companyNameColumn = new DBColumn { Name = "CompanyName" };
                    table.AddColumn(companyNameColumn);
                }
            }
        }

        public static StringCollection CreatePrimaryKeys(params string[] names)
        {
            StringCollection collection = new StringCollection();
            collection.AddRange(names);
            return collection;
        }

        public DBTable[] GetTables(SqlDataConnection connection, params string[] tableList)
        {
            var cp = connection.ConnectionParameters as Access97ConnectionParameters;
            if (cp != null && cp.FileName.Contains("nwind.mdb"))
            {
                if (tables != null)
                {
                    return tables;
                }

                tables = new DBTable[3];
                tables[0] = new DBTable("Categories");
                tables[1] = new DBTable("Products");
                tables[2] = new DBTable("Suppliers");
            }
            else
                tables = new DBTable[0];
            return tables;
        }

        public DBTable[] GetViews(SqlDataConnection connection, params string[] viewList)
        {
            DBTable[] views = new DBTable[0];
            return views;
        }

        public DBStoredProcedure[] GetProcedures(SqlDataConnection connection, params string[] procedureList)
        {
            DBStoredProcedure[] storedProcedures = new DBStoredProcedure[0];
            return storedProcedures;
        }
    }
}
