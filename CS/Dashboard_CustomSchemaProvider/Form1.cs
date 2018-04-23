using DevExpress.XtraBars.Ribbon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess;
using DevExpress.Xpo.DB;
using DevExpress.DataAccess.Sql;

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
            public DBSchema GetSchema(SqlDataConnection dataConnection) {
                var cp = dataConnection.ConnectionParameters as Access97ConnectionParameters;
                if (cp != null && cp.FileName == @"..\..\Data\nwind.mdb") {

                    // Adds two tables with required columns to a data store schema.
                    DBTable[] tables = new DBTable[2];
                    tables[0] = new DBTable("Categories");
                    tables[0].AddColumn(new DBColumnWithAlias("CategoryName", "Category"));
                    tables[0].AddColumn(new DBColumnWithAlias("CategoryID", "Category ID"));

                    tables[1] = new DBTableWithAlias("Products", "Product list");
                    tables[1].AddColumn(new DBColumnWithAlias("ProductName", "Product"));
                    tables[1].AddColumn(new DBColumnWithAlias("CategoryID", "Category ID"));

                    DBTable[] views = new DBTable[0];
                    return new DBSchema(tables, views);
                }
                else
                    return dataConnection.GetDBSchema();
            }
        }
    }
}
