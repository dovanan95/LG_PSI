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
    public partial class MoldAdd : System.Web.UI.Page
    {

        String _mode_code = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ViewState["ACTION"] = "ADD";

            if (!Page.IsPostBack)
            {
                ViewState["ACTION"] = "ADD";
                _mode_code = Request.QueryString["MoldCode"];

                if (_mode_code == null)
                {
                    _mode_code = "";

                }
                else
                {
                    string vender = Session["VENDER"].ToString();
                    
                    
                    DataTable dtMolde = DataProvider.GetMoldListByModeCode(_mode_code, "", vender);
                    
                    if (dtMolde.Rows.Count > 0)
                    {
                        this.txtCapacity.Text = dtMolde.Rows[0]["CAPACITY"].ToString();
                        this.txtMachine.Text = dtMolde.Rows[0]["MACHINE"].ToString();
                        this.txtMoldID.Text = dtMolde.Rows[0]["MOLD_CODE"].ToString();
                        this.txtMoldName.Text = dtMolde.Rows[0]["MOLD_NAME"].ToString();
                        this.txtVender.Text = dtMolde.Rows[0]["VENDER"].ToString();
                        this.ddlDropDownORG.SelectedValue = dtMolde.Rows[0]["ORG_ID"].ToString();
                        ViewState["ACTION"] = "UPDATE";
                        this.txtMoldID.ReadOnly = true;
                        this.btnInsert.Text = "UPDATE";
                    }
                }
                string vender1 = Session["VENDER"].ToString();
                if (vender1 != "LGE")
                {
                    this.txtVender.Text = vender1;
                    this.txtVender.ReadOnly = true;
                }
                
                    string reaLGE = Session["USER_CODE"].ToString();
                    if (reaLGE == "readonly")
                {
                    this.btnDelete.Visible = false;
                    this.btnClear.Visible = false;
                    this.btnInsert.Visible = false;
                }
                this.rpMold.DataSource = DataProvider.GetMoldListByModeCode("", "", vender1);
                this.rpMold.DataBind();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbSQL1 = new StringBuilder();
            String strMoldID = this.txtMoldID.Text.Trim();
            String strMoldName = this.txtMoldName.Text.Trim();
            String strVender = this.txtVender.Text.Trim();
            String strCapacity = this.txtCapacity.Text.Trim();
            String strMachine = this.txtMachine.Text.Trim();
            String strORG = this.ddlDropDownORG.SelectedValue;
            string strUser = Session["USER_CODE"].ToString();
            string strDate = DateTime.Now.ToString();
            if (strORG == "-1")
            {
                return;
            }


            if (ViewState["ACTION"].ToString() == "ADD")
            {
                //CHECK MOLD EXITS OR NOT
                if (DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE MOLD_CODE = '" + strMoldID + "'").Rows.Count > 0)
                {
                    return;
                }


                sbSQL.Append("INSERT INTO PSI_MOLD(MOLD_CODE, MOLD_NAME, CAPACITY, MACHINE, ORG_ID, VENDER, UPDATE_PERSON, UPDATE_TIME) VALUES(");
                sbSQL.Append("'" + strMoldID + "'");
                sbSQL.Append(",'" + strMoldName + "'");
                sbSQL.Append("," + strCapacity);
                sbSQL.Append(",'" + strMachine + "'");
                sbSQL.Append(",'" + strORG + "'");
                sbSQL.Append(",'" + strVender + "'");
                sbSQL.Append(",'" + strUser + "'");
                sbSQL.Append(",'" + strDate + "'");
                sbSQL.Append(")");
            }
            else
            {
                float _capa = 0;
                string _moldname = "";
                string _machine = "";
                string _org = "";
                string _vender = "";
                string _updateperson = "";
                string _updatetime = "";
                DataTable _strPart1 = DataProvider.GetMOLDCapacity(strMoldID);
                foreach (DataRow dr in _strPart1.Rows)
                {
                    _capa = float.Parse(dr["CAPACITY"].ToString());
                    _moldname = dr["MOLD_NAME"].ToString();
                    _machine = dr["MACHINE"].ToString();
                    _org = dr["ORG_ID"].ToString();
                    _vender = dr["VENDER"].ToString();
                    _updateperson= dr["UPDATE_PERSON"].ToString();
                    _updatetime= dr["UPDATE_TIME"].ToString();
                }
                sbSQL1.Append("INSERT INTO PSI_MOLD_BACKUP(MOLD_CODE, MOLD_NAME, CAPACITY, MACHINE, ORG_ID, VENDER, UPDATE_PERSON, UPDATE_TIME) VALUES(");
                sbSQL1.Append("'" + strMoldID + "'");
                sbSQL1.Append(",'" + _moldname + "'");
                sbSQL1.Append(",'" + _capa + "'");
                sbSQL1.Append(",'" + _machine + "'");
                sbSQL1.Append(",'" + _org + "'");
                sbSQL1.Append(",'" + _vender + "'");
                sbSQL1.Append(",'" + _updateperson + "'");
                sbSQL1.Append(",'" + _updatetime + "'");
                sbSQL1.Append(")");
                Int32 iResult1 = DBConnection.ExcuteCommandText(sbSQL1.ToString());




                sbSQL.Append("UPDATE PSI_MOLD SET ");
                sbSQL.Append(" MOLD_NAME = '" + strMoldName + "'");
                sbSQL.Append(", CAPACITY = " + strCapacity);
                sbSQL.Append(", MACHINE = '" + strMachine + "'");
                sbSQL.Append(", ORG_ID = '" + strORG + "'");
                sbSQL.Append(", VENDER = '" + strVender + "'");
                sbSQL.Append(", UPDATE_PERSON = '" + strUser + "'");
                sbSQL.Append(", UPDATE_TIME = '" + strDate + "'");
                sbSQL.Append(" WHERE MOLD_CODE = '" + strMoldID + "'");
                ViewState["ACTION"] = "ADD";
                this.txtMoldID.ReadOnly = false;
                this.btnInsert.Text = "Insert";
            }

            Int32 iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());

            if (iResult > 0)
            {
                string vender = Session["VENDER"].ToString();
                this.rpMold.DataSource = DataProvider.GetMoldListByModeCode("", "", vender);
                this.rpMold.DataBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder sbWHERE = new StringBuilder();

            foreach (RepeaterItem item in this.rpMold.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var checkBox = (CheckBox)item.FindControl("chkRow");
                    
                    if (checkBox.Checked)
                    {
                        string strMoldCode = (item.FindControl("ltrMoldCode") as Literal).Text;
                        string strMoldName = (item.FindControl("ltrMoldName") as Literal).Text;
                        string strVender = (item.FindControl("ltrVender") as Literal).Text;
                        string strCapa = (item.FindControl("ltrCapa") as Literal).Text;
                        string strMachine = (item.FindControl("ltrMachine") as Literal).Text;
                        string strActive = (item.FindControl("ltrActive") as Literal).Text;
                        string strOrg = (item.FindControl("ltrOrg") as Literal).Text;
                        string strUser = Session["USER_CODE"].ToString();
                        string strDate = DateTime.Now.ToString();
                        StringBuilder sbSQL = new StringBuilder();
                        sbSQL.Append("INSERT INTO PSI_MOLD_BACKUP(MOLD_CODE, MOLD_NAME, CAPACITY, MACHINE, ORG_ID, VENDER, UPDATE_PERSON, UPDATE_TIME) VALUES(");
                        sbSQL.Append("'" + strMoldCode + "'");
                        sbSQL.Append(",'" + strMoldName + "'");
                        sbSQL.Append(",'" + strCapa+"'");
                        sbSQL.Append(",'" + strMachine + "'");
                        sbSQL.Append(",'" + strOrg + "'");
                        sbSQL.Append(",'" + strVender + "'");
                        sbSQL.Append(",'" + strUser + "'");
                        sbSQL.Append(",'" + strDate + "'");
                        sbSQL.Append(")");
                        Int32 iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());
                        Int32 result = DataProvider.DeleteMoldByMoldCode(strMoldCode);
                    }
                }
            }
            string vender = Session["VENDER"].ToString();
            this.rpMold.DataSource = DataProvider.GetMoldListByModeCode("", "", vender);
            this.rpMold.DataBind();
        }

        protected void ddlDropDownORG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtMachine_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        protected void rpMold_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }
    }
}