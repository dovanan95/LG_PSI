using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSI_WEBFORM
{
    public partial class Editpart : System.Web.UI.Page
    {
        String strPart = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                strPart = Request.QueryString["PartNumber"];
                
                this.txtPartNumber.Text = strPart;
                this.txtPartNumber.ReadOnly = true;
                LoadVENDERList();
            }
        }
        private void LoadVENDERList()
        {
            DataTable theTable = new DataTable();
            string vender = Session["VENDER"].ToString();
            

            if (vender != "LGE")
            {
                this.DropDownList1.Items.Add("" + vender + "");
            }
            else
            {
                theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT VENDER FROM PSI_USER WHERE ACTIVE = 1 and VENDER is not null ");

                this.DropDownList1.DataTextField = "VENDER";
                this.DropDownList1.DataValueField = "VENDER";
                this.DropDownList1.DataSource = theTable;
                this.DropDownList1.DataBind();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            String strPart = this.txtPartNumber.Text.Trim();
            String strVender = this.DropDownList1.Text.Trim();
            string strUser = Session["USER_CODE"].ToString();
            string strDate = DateTime.Now.ToString();
            sbSQL.Append("UPDATE PSI_PART SET ");
            sbSQL.Append("VENDER = '"+ strVender +"' ");
            sbSQL.Append(", UPDATE_PERSON = '" + strUser + "'");
            sbSQL.Append(", UPDATE_TIME = '" + strDate + "'");
            sbSQL.Append(" WHERE PARTNUMBER = '" + strPart + "'");
            Int32 iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());
        }
    }
}