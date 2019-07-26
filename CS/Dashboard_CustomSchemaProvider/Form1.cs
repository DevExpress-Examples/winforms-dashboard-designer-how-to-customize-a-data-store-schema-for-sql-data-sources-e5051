using DevExpress.DashboardWin;

namespace Dashboard_CustomSchemaProvider
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();
            dashboardDesigner1.CustomizeDashboardTitle += DashboardDesigner1_CustomizeDashboardTitle;

            dashboardDesigner1.LoadDashboard(@"Data\dashboard-with-products.xml");
        }

        private void DashboardDesigner1_CustomizeDashboardTitle(object sender, DevExpress.DashboardWin.CustomizeDashboardTitleEventArgs e)
        {
            DashboardToolbarItem itemSave = new DashboardToolbarItem(
                (args) => UseCustomDBSchemaProvider())
            {
                Caption = "Use Custom DBSchema Provider",
            };
            e.Items.Clear();
            e.Items.Add(itemSave);
        }

        private void UseCustomDBSchemaProvider()
        {
            dashboardDesigner1.CustomDBSchemaProviderEx = new CustomDBSchemaProvider();
        }
    }
}
