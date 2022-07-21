using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSI_WEBFORM
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string vender = "";
                if (Session["USER_CODE"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    string user = Session["USER_CODE"].ToString();
                    this.hlLink.Text = Session["USER_NAME"] + " (LOG OUT ) ";

                    DataTable _srtUser = DataProvider.GetUser(user);

                    foreach (DataRow dr1 in _srtUser.Rows)
                    {
                        vender = dr1["VENDER"].ToString();
                        Session["VENDER"] = vender;
                    }

                }
            }
        }
    }
}