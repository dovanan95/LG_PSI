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
    public partial class PartAdd : System.Web.UI.Page
    {

        DataTable _getVder = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            String strPartnumber = "";
            if (!Page.IsPostBack)
            {
                string vender = Session["VENDER"].ToString();
                this.rpPartLocal.DataSource = DataProvider.GetPartMana2( vender);
                this.rpPartLocal.DataBind();

                this.pnBOM.Visible = false;
                //LoadcBoxVender();
                string reaLGE = Session["USER_CODE"].ToString();
                if (reaLGE == "readonly")
                {
                    this.btnDelete.Visible = false;
                }
                
                strPartnumber = Request.QueryString["PartNumber"];
                if (strPartnumber == null)
                {
                    strPartnumber = "";

                }
                else
                {
                    DataTable tbPart = DataProvider.GetPSIPartNumberByPart(strPartnumber);
                    string _vender = "";
                    foreach (DataRow dr in tbPart.Rows)
                    {
                       _vender = dr["VENDER"].ToString();
                    }
                }
            }
        }

        private void LoadcBoxVender()
        {
            _getVder = DataProvider.GetVender();
            DropDownList cbo = grd_vendor.FindControl("cboVendor") as DropDownList;
            if (_getVder.Rows.Count > 0)
            {
                //string vender = Session["VENDER"].ToString();
                //if (vender == "LGE")
                //{
                    try
                    {
                        cbo.DataSource = _getVder;
                        cbo.DataTextField = "VENDER";
                        cbo.DataValueField = "VENDER";
                        cbo.DataBind();
                    }
                    catch (Exception ex) { }
                //}
                //else
                //{

                //    cbovendor.Items.Add("'" + vender + "'");
                //}

            }
        }
        protected void onRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType==DataControlRowType.DataRow)
            {
                DropDownList cbo = (e.Row.FindControl("cboVendor") as DropDownList);
                cbo.DataSource = DataProvider.GetVender();
                cbo.DataTextField = "VENDER";
                cbo.DataValueField = "VENDER";
                cbo.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder("SELECT distinct CHILD_MTRLID, CHILD_MTRLNAME, PLAN_LEVEL, CORP_ID, ORG_ID FROM tb_pom_bom_bas ");
            StringBuilder sbWHERE = new StringBuilder();
            LoadcBoxVender();
            if (this.txtPartNumber.Text.Trim().Length > 2)
            {
                sbWHERE.Append("WHERE child_mtrlid LIKE '%" + this.txtPartNumber.Text.Trim() + "%'");
            }
            else
            {
                return;
            }

            if (sbWHERE.ToString() == "")
            {
                this.grd_vendor.DataSource = null;
                this.grd_vendor.DataBind();
                this.pnBOM.Visible = false;
                return;
            }

            DataTable theTableLevel1 = DBConnection.GetDataTableByCommandTextGMES(sb.Append(sbWHERE).ToString());

            this.grd_vendor.DataSource = theTableLevel1;
            this.grd_vendor.DataBind();

            if (theTableLevel1.Rows.Count > 0)
            {
                this.pnBOM.Visible = true;
                string vender = Session["VENDER"].ToString();
                if (vender != "LGE")
                {
                    this.btnInsert.Visible = false;
                }
                string reaLGE = Session["USER_CODE"].ToString();
                if (reaLGE == "readonly")
                {
                    this.btnInsert.Visible = false;
                }
            }
            else
            {
                this.pnBOM.Visible = false;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            
            //string strChild_MTRLID = (grd_vender.FindControl("ltrChild_MTRLID") as Literal).Text;
            string strUser = Session["USER_CODE"].ToString();
            string strDate = DateTime.Now.ToString();
            foreach (GridViewRow item in grd_vendor.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    //try
                    //{
                        CheckBox checkBox = (item.Cells[0].FindControl("chkRox") as CheckBox);
                        if (checkBox.Checked)
                        {
                            string strPartNumber = item.Cells[1].Text;
                            string strPartName = item.Cells[2].Text;
                            string strPlanLevel = item.Cells[3].Text;
                            string strOrgID = item.Cells[4].Text;
                            string strOrgName = item.Cells[5].Text;
                            DropDownList grView = grd_vendor.Rows[0].FindControl("cboVendor") as DropDownList;
                            string strVender = grView.SelectedItem.Value;
                            //NOT INSERT WHEN EXITS PARTNUMBER IN PSI_PART (LOCAL DATABASE)
                            if (DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_PART WHERE PARTNUMBER = '" + strPartNumber + "'").Rows.Count > 0)
                            {
                                continue;
                            }
                            StringBuilder sbSQL = new StringBuilder();
                            sbSQL.Append("INSERT INTO PSI_PART(PARTNUMBER, PARTNAME, PLAN_LEVEL, ORG_ID, VENDER, UPDATE_PERSON, UPDATE_TIME) VALUES (");
                            sbSQL.Append("'" + strPartNumber + "'");
                            sbSQL.Append(",'" + strPartName + "'");
                            sbSQL.Append(",'" + strPlanLevel + "'");
                            sbSQL.Append(",'" + strOrgID + "'");
                            sbSQL.Append(",'" + strVender + "'");
                            sbSQL.Append(",'" + strUser + "'");
                            sbSQL.Append(",'" + strDate + "'");
                            sbSQL.Append(")");


                            int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());

                            if (iResult > 0)
                            {
                                string vender = Session["VENDER"].ToString();
                                this.rpPartLocal.DataSource = DataProvider.GetPartMana2( vender);
                                this.rpPartLocal.DataBind();
                            }

                        }
                        else
                        {
                            
                        }

                       
                    }
                //    catch (Exception ex) { }
                //}
            }
            //{
            //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            //    {
            //        var checkBox = (CheckBox)item.FindControl("chkRow");

            //        if (checkBox.Checked)
            //        {

            //            string strChild_MTRLID = (item.FindControl("ltrChild_MTRLID") as Literal).Text;

            //NOT INSERT WHEN EXITS PARTNUMBER IN PSI_PART (LOCAL DATABASE)
            //if (DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_PART WHERE PARTNUMBER = '" + strChild_MTRLID + "'").Rows.Count > 0)
            //{
            //    continue;
            //}

            //            //string strProdID = (item.FindControl("ltrProdID") as Literal).Text;
            //            string strChild_MTRLName = (item.FindControl("ltrChild_MTRLName") as Literal).Text;
            //            string strPlan_Level = (item.FindControl("ltrPlan_Level") as Literal).Text;
            //            string strOrg_ID = (item.FindControl("ltr_Org_ID") as Literal).Text;

            //StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append("INSERT INTO PSI_PART(PARTNUMBER, PARTNAME, PLAN_LEVEL, ORG_ID) VALUES (");
            //sbSQL.Append("'" + strChild_MTRLID + "'");
            //sbSQL.Append(",'" + strChild_MTRLName + "'");
            //sbSQL.Append(",'" + strPlan_Level + "'");
            //sbSQL.Append(",'" + strOrg_ID + "'");

            //sbSQL.Append(")");


            //int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());

            //if (iResult > 0)
            //{
            //    string vender = Session["VENDER"].ToString();
            //    this.rpPartLocal.DataSource = DataProvider.GetPartManagementByOrgID2("", "", "", vender);
            //    this.rpPartLocal.DataBind();
            //}
            //        }
            //    }
            //}
        }

        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder sbWHERE = new StringBuilder();
            string strUser = Session["USER_CODE"].ToString();
            string strDate = DateTime.Now.ToString();
            foreach (RepeaterItem item in this.rpPartLocal.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var checkBox = (CheckBox)item.FindControl("chkRow1");

                    if (checkBox.Checked)
                    {
                        string strPartNumber = (item.FindControl("ltrPartNumber") as Literal).Text;
                        string strPartName = (item.FindControl("ltrPartName") as Literal).Text;
                        string strPlan = (item.FindControl("ltrPlanLevel") as Literal).Text;
                        string strOrg = (item.FindControl("ltrOrg") as Literal).Text;
                        string strActive = (item.FindControl("ltrActive") as Literal).Text;
                        string strVender = (item.FindControl("ltrVender") as Literal).Text;
                        string strStockQty = "";
                        string strStockDate = "";
                        string strToolMsg = "";
                        DataTable _getPart = new DataTable();
                        _getPart = DataProvider.GetPSIPartNumberByPart(strPartNumber);
                        foreach (DataRow dr in _getPart.Rows)
                        {
                            strStockQty = dr["STOCK_QTY"].ToString();
                            strStockDate = dr["STOCK_DATE"].ToString();
                            strToolMsg = dr["TOOL_MSG"].ToString();
                        }
                        StringBuilder sbSQL = new StringBuilder();

                        sbSQL.Append("INSERT INTO PSI_PART_BACKUP(PARTNUMBER, PARTNAME, PLAN_LEVEL, ORG_ID, VENDER, UPDATE_PERSON, UPDATE_TIME, STOCK_QTY, STOCK_DATE, TOOL_MSG) VALUES (");
                        sbSQL.Append("'" + strPartNumber + "'");
                        sbSQL.Append(",'" + strPartName + "'");
                        sbSQL.Append(",'" + strPlan + "'");
                        sbSQL.Append(",'" + strOrg + "'");
                        sbSQL.Append(",'" + strVender + "'");
                        sbSQL.Append(",'" + strUser + "'");
                        sbSQL.Append(",'" + strDate + "'");
                        sbSQL.Append(",'" + strStockQty + "'");
                        sbSQL.Append(",'" + strStockDate + "'");
                        sbSQL.Append(",'" + strToolMsg + "'");
                        sbSQL.Append(")");


                        int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());

                        Int32 result = DataProvider.DeletePartByPartNumber(strPartNumber);
                    }
                }
            }
            string vender = Session["VENDER"].ToString();
            this.rpPartLocal.DataSource = DataProvider.GetPartMana2( vender);
            this.rpPartLocal.DataBind();
        }

        protected void btnSeachLocal_Click(object sender, EventArgs e)
        {
            string vender = Session["VENDER"].ToString();
            this.rpPartLocal.DataSource = DataProvider.GetPartManagementByOrgID2("", this.txtPartNumberLocal.Text.Trim(), "", vender);
            this.rpPartLocal.DataBind();
        }
    }
}