using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace PSI_WEBFORM
{
    public partial class PartStockUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string reaLGE = Session["USER_CODE"].ToString();
                if (reaLGE == "readonly")
                {
                    this.btnSave.Visible = false;
                }
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            String _cur_date = DataProvider.GetCurrentDateTime().ToString("yyyyMMdd");

            foreach (RepeaterItem item in this.rpPartStock.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                  

                    string strPartNumber = (item.FindControl("ltrPartNumber") as Literal).Text;
                    DataTable _getPart = new DataTable();
                    _getPart = DataProvider.GetPSIPartNumberByPart(strPartNumber);
                    string strStock = "";
                    string strDate = "";
                    string strMsg  = "";
                    foreach (DataRow dr in _getPart.Rows)
                    {
                        strStock = dr["STOCK_QTY"].ToString();
                        strDate = dr["STOCK_DATE"].ToString();
                        strMsg = dr["TOOL_MSG"].ToString();
                    }
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.Append("INSERT INTO PSI_PART_BACKUP(PARTNUMBER, STOCK_QTY, STOCK_DATE, TOOL_MSG) VALUES (");
                    sbSQL.Append("'" + strPartNumber + "'");
                    sbSQL.Append(",'" + strStock + "'");
                    sbSQL.Append(",'" + strDate + "'");
                    sbSQL.Append(",'" + strMsg + "'");
                    sbSQL.Append(")");
                    int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());

                    string strToolMsg = (item.FindControl("txtToolInfo") as TextBox).Text;
                    string strQuantity = (item.FindControl("txtStock") as TextBox).Text;
                    StringBuilder sbSQL1 = new StringBuilder();
                    string strUser = Session["USER_CODE"].ToString();
                    strDate = DateTime.Now.ToString();
                    sbSQL1.Append("INSERT INTO PSI_PART_BACKUP(PARTNUMBER, UPDATE_PERSON, UPDATE_TIME, STOCK_QTY, STOCK_DATE, TOOL_MSG) VALUES (");
                    sbSQL1.Append("'" + strPartNumber + "'");
                    sbSQL1.Append(",'" + strUser + "'");
                    sbSQL1.Append(",'" + strDate + "'");
                    sbSQL1.Append(",'" + strQuantity + "'");
                    sbSQL1.Append(",'" + _cur_date + "'");
                    sbSQL1.Append(",'" + strToolMsg + "'");
                    sbSQL1.Append(")");


                    int iResult1 = DBConnection.ExcuteCommandText(sbSQL1.ToString());
                    try
                    {
                        float tmp = float.Parse(strQuantity);

                        if (tmp < 0)
                        {
                            strQuantity = "0";
                        }
                    }
                    catch
                    {
                        strQuantity = "0";
                    }

                    Int32 result = DataProvider.UpdateStockQtyByPartNumber(strPartNumber, strToolMsg, strQuantity, _cur_date);
                }
            }
        }

        public String CheckStockDate(string _stock_qty, string _stock_date)
        {
            String _cur_date = DataProvider.GetCurrentDateTime().ToString("yyyyMMdd");

            if (_stock_date == _cur_date)
                return _stock_qty;
            else
                return "0";
        }

        public String GetCurrentDateTime()
        {
           return DataProvider.GetCurrentDateTime().ToString("dd-MM");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            String _org_id = "";

            if (this.ddlDropDownORG.SelectedValue != "-1")
            {
                _org_id = this.ddlDropDownORG.SelectedValue;
            }
            string vender = Session["VENDER"].ToString();
            this.rpPartStock.DataSource = DataProvider.GetPartManagementByOrgID(_org_id, "", "ALL", vender);
            this.rpPartStock.DataBind();
        }
    }
}