using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PSI_WEBFORM
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lbMessage.Text = "";

            if (!Page.IsPostBack)
            {
                Session["USER_CODE"] = null;
                Session["USER_NAME"] = null;
                Session["ORG_ID"] = null;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            this.lbMessage.Text = "";

            String _user_code = this.txtUser.Text.Trim();
            String _user_pwd = this.txtPassword.Text.Trim();

            if (_user_code == "" || _user_pwd == "")
            {
                this.lbMessage.Text = "INPUT USER & PASSWORD";
                return;
            }

            DataTable theTable = DataProvider.GetUser(_user_code, _user_pwd);

            if (theTable.Rows.Count == 0)
            {
                this.lbMessage.Text = "ACCOUNT INVALID";
            }
            else
            {
                Session["USER_CODE"] = theTable.Rows[0]["USER_CODE"].ToString();
                Session["USER_NAME"] = theTable.Rows[0]["USER_NAME"].ToString();
                Session["ORG_ID"] = theTable.Rows[0]["ORG_ID"].ToString();

                Response.Redirect("Index.aspx");
            }
        }
    }
}