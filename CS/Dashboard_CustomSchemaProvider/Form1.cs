using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars.Ribbon;
using System.Collections.Specialized;

namespace Dashboard_CustomSchemaProvider {
    public partial class Form1 : RibbonForm {
        public Form1() {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();

            // Specifies a custom schema provider.
            dashboardDesigner1.CustomDBSchemaProviderEx = new CustomDBSchemaProvider();

            // Loads a dashboard from an XML file.
            dashboardDesigner1.LoadDashboard(@"..\..\Data\dashboard.xml");
        }

        // Creates a new class that defines a custom data store schema by implementing the 
        // IDBSchemaProvider interface.
        class CustomDBSchemaProvider : IDBSchemaProviderEx {
            DBTable[] tables;
            public void LoadColumns(SqlDataConnection connection, params DBTable[] tables) {
                foreach(DBTable table in tables) {
                    if(table.Name == "Categories" && table.Columns.Count == 0) {
                        DBColumn categoryIdColumn = new DBColumn { Name = "CategoryID" };
                        table.AddColumn(categoryIdColumn);
                        DBColumn categoryNameColumn = new DBColumn {Name = "CategoryName"};
                        table.AddColumn(categoryNameColumn);
                    }
                    if(table.Name == "Products" && table.Columns.Count == 0) {
                        DBColumn categoryIdColumn = new DBColumn { Name = "CategoryID" };
                        table.AddColumn(categoryIdColumn);
                        DBColumn productNameColumn = new DBColumn {Name = "ProductName"};
                        table.AddColumn(productNameColumn);

                        DBForeignKey foreignKey = new DBForeignKey(
                            new[] { categoryIdColumn },
                            "Categories",
                            CustomDBSchemaProvider.CreatePrimaryKeys("CategoryID"));
                        table.ForeignKeys.Add(foreignKey);
                    }
                }
            }

            public static StringCollection CreatePrimaryKeys(params string[] names) {
                StringCollection collection = new StringCollection();
                collection.AddRange(names);
                return collection;
            }

            public DBTable[] GetTables(SqlDataConnection connection, params string[] tableList) {
                var cp = connection.ConnectionParameters as Access97ConnectionParameters;
                if (cp != null && cp.FileName == @"..\..\Data\nwind.mdb") {
                    if (tables != null) {
                        return tables;
                    }
                    tables = new DBTable[2];

                    DBTable categoriesTable = new DBTable("Categories");
                    tables[0] = categoriesTable;

                    DBTable productsTable = new DBTable("Products");
                    tables[1] = productsTable;                  
                }
                else
                    tables = new DBTable[0];
                return tables;
            }

            public DBTable[] GetViews(SqlDataConnection connection, params string[] viewList) {
                DBTable[] views = new DBTable[0];
                return views;
            }

            public DBStoredProcedure[] GetProcedures(SqlDataConnection connection, params string[] procedureList) {
                DBStoredProcedure[] storedProcedures = new DBStoredProcedure[0];
                return storedProcedures;
            }
        }
    }
}
