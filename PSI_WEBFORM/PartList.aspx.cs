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
    public partial class PartList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder("SELECT * FROM tb_pom_bom_bas ");
            StringBuilder sbWHERE = new StringBuilder();

            if (this.txtPartNumber.Text.Trim() != "")
            {
                sbWHERE.Append("WHERE child_mtrlid LIKE '%" + this.txtPartNumber.Text.Trim() + "%'");
            }

            if (this.txtModel.Text.Trim() != "")
            {
                if (sbWHERE.ToString() != "")
                {
                    sbWHERE.Append(" AND prodid LIKE '%" + this.txtModel.Text.Trim() + "%'");
                }
                else
                {
                    sbWHERE.Append("WHERE prodid LIKE '%" + this.txtModel.Text.Trim() + "%'");
                }
            }

            if (this.ddlLevel.SelectedValue != "-1")
            {
                if (sbWHERE.ToString() != "")
                {
                    sbWHERE.Append(" AND plan_level LIKE '%" + this.ddlLevel.SelectedValue + "%'");
                }
                else
                {
                    sbWHERE.Append("WHERE plan_level LIKE '%" + this.ddlLevel.SelectedValue + "%'");
                }
            }

            DataTable theTableLevel1 = DBConnection.GetDataTableByCommandTextGMES(sb.Append(sbWHERE).ToString());

            this.rpData.DataSource = theTableLevel1;
            this.rpData.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartAdd.aspx");
        }
    }
}