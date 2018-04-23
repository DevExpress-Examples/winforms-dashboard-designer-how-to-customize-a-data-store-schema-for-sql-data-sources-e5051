using System.Collections.Specialized;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars.Ribbon;

namespace Dashboard_CustomSchemaProvider {
    public partial class Form1 : RibbonForm {
        public Form1() {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();

            // Specifies a custom schema provider.
            dashboardDesigner1.CustomDBSchemaProvider = new CustomDBSchemaProvider();

            // Loads a dashboard from an XML file.
            dashboardDesigner1.LoadDashboard(@"..\..\Data\dashboard.xml");
        }

        // Creates a new class that defines a custom data store schema by implementing the 
        // IDBSchemaProvider interface.
        class CustomDBSchemaProvider : IDBSchemaProvider {
            public DBSchema GetSchema(SqlDataConnection connection, SchemaLoadingMode schemaLoadingMode) {
                var cp = connection.ConnectionParameters as Access97ConnectionParameters;
                if (cp != null && cp.FileName == @"..\..\Data\nwind.mdb") {

                    DBTable[] tables;
                    if (schemaLoadingMode.HasFlag(SchemaLoadingMode.TablesAndViews)) {
                        // Creates two tables with required columns to be added to a data store schema.
                        tables = new DBTable[2];
                        DBTable categoriesTable = new DBTable("Categories");
                        DBColumn categoryNameColumn1 = new DBColumn { Name = "CategoryName" };
                        categoriesTable.AddColumn(categoryNameColumn1);
                        DBColumn categoryIdColumn1 = new DBColumn { Name = "CategoryID" };
                        categoriesTable.AddColumn(categoryIdColumn1);
                        tables[0] = categoriesTable;

                        DBTable productsTable = new DBTable("Products");
                        DBColumn productNameColumn2 = new DBColumn { Name = "ProductName" };
                        productsTable.AddColumn(productNameColumn2);
                        DBColumn categoryIdColumn2 = new DBColumn { Name = "CategoryID" };
                        productsTable.AddColumn(categoryIdColumn2);
                        tables[1] = productsTable;

                        // Creates a foreign key for the 'Products' table that points to the 'CategoryID' 
                        // column in the 'Categories' table.
                        DBForeignKey foreignKey = new DBForeignKey(
                            new[] { categoryIdColumn2 },
                            categoriesTable.Name,
                            CustomDBSchemaProvider.CreatePrimaryKeys(categoryIdColumn1.Name));
                        productsTable.ForeignKeys.Add(foreignKey);
                    }
                    else
                        tables = new DBTable[0];

                    DBTable[] views = new DBTable[0];
                    return new DBSchema(tables, views);
                }
                else
                    return connection.GetDBSchema();
            }

            public void LoadColumns(SqlDataConnection connection, params DBTable[] tables) {
                connection.LoadDBColumns(tables);
            }

            public static StringCollection CreatePrimaryKeys(params string[] names) {
                StringCollection collection = new StringCollection();
                collection.AddRange(names);
                return collection;
            }
        }
    }
}
