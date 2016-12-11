using System;
using System.Web.UI.WebControls;

namespace MyDoco
{
    public partial class MySite : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label lbl = ((Label)ContentPlaceHolder1.FindControl("lblContentItemName"));
                // Local cache of category data
                CategoryCollection collection = Common.GetRootCategories();
                var item = Common.GetCurrentItem();
                // Bind the data source to your collection
                if (collection.Count != 0)
                {
                    uxTreeView.DataSource = collection;
                    uxTreeView.DataBind();
                    lbl.Text = uxTreeView.Nodes[0].ValuePath;
                }      
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void uxTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {

            Label lbl = ((Label)ContentPlaceHolder1.FindControl("lblContentItemName"));
            if (uxTreeView.SelectedNode != null)
            {               
                lbl.Text = uxTreeView.SelectedNode.ValuePath;
                ((Label)ContentPlaceHolder1.FindControl("lblContentItemName")).Text= uxTreeView.SelectedNode.ValuePath;
                Session["YourKey"] = uxTreeView.SelectedNode.ValuePath;
            }
            else
            {
                lbl.Text = uxTreeView.Nodes[0].ValuePath;
            }
        }
    }
}