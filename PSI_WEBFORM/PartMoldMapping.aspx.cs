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
    public partial class PartMoldMapping : System.Web.UI.Page
    {

        String strPartNumber = "";
        String strMoldCode = "";
     
        protected void Page_Load(object sender, EventArgs e)
        {

           strPartNumber = Request.QueryString["PartNumber"].ToString();
            this.txtPartNumber.Text = strPartNumber;
            //if (null!= Request.QueryString["MOLD_CODE"])
            //{
            //    strMoldCode = Request.QueryString["MOLD_CODE"].ToString();
            //    this.lbmoldname.Text = strMoldCode;
            //}else
            //{
            //    this.lbmoldname.Text = "no name";
            //}
            
            

            if (!Page.IsPostBack)
            {
                ViewState["ACTION"] = "ADD";
                strMoldCode = Request.QueryString["MoldCode"];
                if (strMoldCode == null)
                {
                    strMoldCode = "";   

                }
                else
                {
                    string vender = Session["VENDER"].ToString();
                    DataTable dtMolde = DataProvider.GetMoldListByModeCode(strMoldCode, "", vender);

                    if (dtMolde.Rows.Count > 0)
                    {
                        //this.ddlMold.Text = Request.QueryString["MoldCode"].ToString();
                        ViewState["ACTION"] = "UPDATE";
                        this.txtPartNumber.ReadOnly = true;
                        this.btnMapping.Text = "UPDATE";
                        //this.txtRate.Text = dtMolde.Rows[0]["RATE"].ToString();
                        strMoldCode = Request.QueryString["MoldCode"];
                        this.ddlMold.Enabled = false;
                        DataTable theTable = new DataTable();
                        theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_PART_MOLD WHERE PARTNUMBER = '" + strPartNumber + "' AND MOLD_CODE = '" + strMoldCode + "'");
                        this.txtRate.Text = theTable.Rows[0]["RATE"].ToString();
                    }
                }
                LoadMoldList();
                LoadDataPartMold(this.txtPartNumber.Text.Trim());
            }
        }

        private void LoadDataPartMold(String _partNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT P.PARTNUMBER , P.PARTNAME, P.PLAN_LEVEL, M.MOLD_CODE, M.MOLD_NAME, M.CAPACITY, PM.RATE, PM.ACTIVE ");
            sb.Append("FROM PSI_PART_MOLD PM INNER JOIN PSI_PART P ON PM.PARTNUMBER = P.PARTNUMBER ");
            sb.Append("INNER JOIN PSI_MOLD M ON PM.MOLD_CODE = M.MOLD_CODE ");
            sb.Append("WHERE P.PARTNUMBER = '" + _partNumber + "'");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());

            this.rpPartMold.DataSource = theTable;
            this.rpPartMold.DataBind();

        }

        private void LoadMoldList()
        {
            DataTable theTable = new DataTable();
            strMoldCode = Request.QueryString["MoldCode"];

            if (strMoldCode == null)
            {
                string vender = Session["VENDER"].ToString();
                string vender1 = "";
                if (vender != "LGE")
                {
                    vender1 = "AND VENDER = '" + vender + "' ";
                }
                //theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 AND MOLD_CODE NOT IN (SELECT MOLD_CODE FROM PSI_PART_MOLD )");
                theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 " + vender1 + " AND MOLD_CODE NOT IN (SELECT MOLD_CODE FROM PSI_PART_MOLD WHERE PARTNUMBER= '" + strPartNumber + "'  ) ORDER BY MOLD_CODE ASC");

                this.ddlMold.DataTextField = "MOLD_NAME";
                this.ddlMold.DataValueField = "MOLD_CODE";
                this.ddlMold.DataSource = theTable;
                this.ddlMold.DataBind();
            }
            else
            {
                string vender = Session["VENDER"].ToString();
                string vender1 = "";
                if (vender != "LGE")
                {
                    vender1 = "AND VENDER = '" + vender + "' ";
                }
                //theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 AND MOLD_CODE = '" + strMoldCode + "' OR MOLD_CODE NOT IN (SELECT MOLD_CODE FROM PSI_PART_MOLD ) ");
                theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 AND MOLD_CODE = '" + strMoldCode + "' " + vender1 + "");

                this.ddlMold.DataTextField = "MOLD_NAME";
                this.ddlMold.DataValueField = "MOLD_CODE";
                this.ddlMold.DataSource = theTable;
                this.ddlMold.DataBind();
            }
        }

        protected void btnMapping_Click(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            if (ViewState["ACTION"].ToString() == "ADD")
            {
                
                sbSQL.Append("INSERT INTO PSI_PART_MOLD(PARTNUMBER, MOLD_CODE, RATE) VALUES (");
                sbSQL.Append("'" + this.txtPartNumber.Text.Trim() + "'");
                sbSQL.Append(",'" + this.ddlMold.SelectedValue + "'");
                sbSQL.Append("," + this.txtRate.Text);
                sbSQL.Append(")");
            }
            else
            {
                String strRate = this.txtRate.Text.Trim();
                strMoldCode = Request.QueryString["MoldCode"];
                sbSQL.Append("UPDATE PSI_PART_MOLD SET ");
                sbSQL.Append(" RATE = '" + strRate + "'");
                sbSQL.Append(" WHERE MOLD_CODE = '" + strMoldCode + "' AND PARTNUMBER = '" + strPartNumber + "'");
                ViewState["ACTION"] = "ADD";
                this.btnMapping.Text = "Mapping";
                this.txtRate.Text = "";
                this.ddlMold.Enabled = true;
                DataTable theTable = new DataTable();
                string vender = Session["VENDER"].ToString();
                string vender1 = "";
                if (vender != "LGE")
                {
                    vender1 = "AND VENDER = '" + vender + "' ";
                }
                theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 AND MOLD_CODE NOT IN (SELECT MOLD_CODE FROM PSI_PART_MOLD WHERE PARTNUMBER = '" + this.txtPartNumber.Text.Trim() + "') " + vender1 + "");
                this.ddlMold.DataTextField = "MOLD_NAME";
                this.ddlMold.DataValueField = "MOLD_CODE";
                this.ddlMold.DataSource = theTable;
                this.ddlMold.DataBind();
               
            }
            int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());
            
            if (iResult > 0)
            {
                LoadDataPartMold(this.txtPartNumber.Text.Trim());
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //sbSQL.Append("DELETE PSI_PART_MOLD WHERE");
            foreach (RepeaterItem item in this.rpPartMold.Items)
            {
                StringBuilder sbSQL = new StringBuilder();
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var checkBox = (CheckBox)item.FindControl("chkRow");

                    if (checkBox.Checked)
                    {
                        string strPartNumber = (item.FindControl("ltrPartNumber") as Literal).Text;
                        string strMoldCode = (item.FindControl("ltrMoldCode") as Literal).Text;

                        sbSQL.Append("DELETE FROM PSI_PART_MOLD WHERE PARTNUMBER = '" + strPartNumber + "' AND MOLD_CODE = '" + strMoldCode + "'");
                    }
                    //if (sbSQL.ToString() != "")
                    //{
                    //    Int32 iResult = 
                    DBConnection.ExcuteCommandText(sbSQL.ToString());

                    //    if (iResult > 0)
                    //    {
                    //        LoadDataPartMold(this.txtPartNumber.Text.Trim());
                            
                    //    }
                    //}
                }
            }
            LoadDataPartMold(this.txtPartNumber.Text.Trim());
        }

        protected void ddlMold_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rpPartMold_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}