using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatórios_EqptParados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mv1.ActiveViewIndex = 0;
    }
    protected void menuViews_MenuItemClick(object sender, MenuEventArgs e)
    {
        mv1.ActiveViewIndex = int.Parse(menuViews.SelectedValue);
    }
}