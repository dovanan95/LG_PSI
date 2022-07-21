using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace PSI_WEBFORM
{
    public partial class Index : System.Web.UI.Page
    {

        Int32 _max_day = 30;
        DateTime _cur_date = DataProvider.GetCurrentDateTime();
        String _org_id = "-1";
        String _tool = "-1";
        String _vender = "-1";
        Hashtable _htScale = new Hashtable();
        Hashtable _htProductionPlan = new Hashtable();
        DataTable _tbPartManagement = new DataTable();
        DataTable _tbMoldList = new DataTable();
        DataTable _tbMoldSchedule = new DataTable();
        DataTable _strChid = new DataTable();
        DataTable _dailyPro1 = new DataTable();
        DataTable _ToolInfo = new DataTable();
        DataTable _getVender = new DataTable();
        DataTable _strPart = new DataTable();
        DataTable _tbToolManagement = new DataTable();
        DataTable _tbMold = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                LoadDataToDropDownList1();
                //LoadcBoxVender();
               
            }

        }
        private void LoadcBoxVender()
        {
            _getVender = DataProvider.GetVender();
            DropDownList2.DataSource = _getVender;
            DropDownList2.DataTextField = "VENDER"; 
            DropDownList2.DataValueField = "VENDER";
            DropDownList2.DataBind();
            DropDownList2.Items.Add("ALL");
        }
        private void LoadDataToDropDownList1()
        {
                
                _ToolInfo = DataProvider.GetPart();

                
                DropDownList1.DataSource = _ToolInfo;
                
                DropDownList1.DataTextField = "tool_msg";
                DropDownList1.DataValueField = "tool_msg";
                
                DropDownList1.DataBind();

                DropDownList1.Items.Add("ALL");
            
        }

        //private void LoadData()
        //{
        //    _tbPartManagement = DataProvider.GetPartManagementByOrgID(_org_id, "", _tool);
        //    _tbMoldSchedule = DataProvider.GetDataMoldScheduleFromCurrentDate(_cur_date.ToString("yyyyMMdd"), _tool);
            
        //   // _strPart = DataProvider.GetDataPSIPart(_tool);
        //    if (_tbPartManagement.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < _max_day; ++i)
        //        {
        //            _htProductionPlan[_cur_date.AddDays(i).ToString("yyyyMMdd")] = DataProvider.GetProductionPlan(_org_id, _cur_date.AddDays(i).ToString("yyyyMMdd"), _cur_date.AddDays(i).ToString("YYYYMMDD"));
        //        }
        //    }
        //}

        private void LoadData1()
        {
            //_tbPartManagement = DataProvider.GetPartManagementByOrgID(_org_id, "", _tool);
            _tbMoldSchedule = DataProvider.GetDataMoldScheduleFromCurrentDate(_cur_date.ToString("yyyyMMdd"), _tool);

           
        }

        private void RederLayout()
        {
            string vender = Session["VENDER"].ToString();
            StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append("<div class='scrollbar'>");
            _tbToolManagement = DataProvider.GetToolManagementByOrgID(_org_id, _tool, vender);
            foreach (DataRow dr in _tbToolManagement.Rows)
            {

                String _tool_msg = dr["TOOL_MSG"].ToString();
                String _Name = dr["PARTNAME"].ToString();
                sbHTML.Append(RenderLayoutForTool(_tool_msg, _Name).ToString());
            }
            
            this.ltrHTML.Text = sbHTML.ToString();
            sbHTML.Append("</div>");
        }

        private void RederLayout1()
        {
            string vender = Session["VENDER"].ToString();
            StringBuilder sbHTML = new StringBuilder();
            sbHTML.Append("<div class='scrollbar'>");
            if(_vender =="-1"){
                _tbToolManagement = DataProvider.GetToolManagementByOrgID(_org_id, _tool, vender);
            }
            else
            {
                _tbToolManagement = DataProvider.GetToolManagementByOrgID(_org_id, _tool, _vender);
            }
            
            foreach (DataRow dr in _tbToolManagement.Rows)
            {

                String _tool_msg = dr["TOOL_MSG"].ToString();
                String _Name = dr["PARTNAME"].ToString();
                sbHTML.Append(RenderLayoutForTool1(_tool_msg, _Name).ToString());
            }

            this.ltrHTML.Text = sbHTML.ToString();
            sbHTML.Append("</div>");
        }

        private StringBuilder RenderLayoutForTool1(String _tool_msg, String _Name)
        {
            StringBuilder sbHTML = new StringBuilder();
            float[] arrCapa = new float[_max_day];
            float[] arrPlanConsumsion = new float[_max_day];
            //CREATE HEADER
            
            sbHTML.Append("<div class='scrollbar1'>");
            sbHTML.Append("<table class='table-shedule'>");
            sbHTML.Append("<tr>");
            sbHTML.Append("<td class='sizetool'>Tool Info </td>");
            sbHTML.Append("<td class='sizetool1'>Part Name</td>");
            sbHTML.Append("<td class='sizetool2'>Part Number</td>");
            sbHTML.Append("<td class='sizetool3'></td><td class='sizetool4'></td>");
            for (Int32 i = 0; i < _max_day; ++i)
            {
                sbHTML.Append("<td class='d" + (i + 1).ToString() + " day'>" + _cur_date.AddDays(i).ToString("MMM-d") + "</td>");

                //SET VALUE for arrCapa and ArrPlanConsumsion
                arrCapa[i] = 0;
                arrPlanConsumsion[i] = 0;
            }
            sbHTML.Append("</tr>");

            int dem = 0;
            int dem1 = 0;
            // GET PARTNUMBER DATA
            float _stock = 0;
            DataTable _strPart1 = DataProvider.GetPartInTool(_tool_msg, _Name);
            foreach (DataRow dr in _strPart1.Rows)
            {
                dem++;
                dem1++;
            }

            DataTable theTableMold = DataProvider.GetDataPartMoldByParNumber(_tool_msg);

            foreach (DataRow dr1 in theTableMold.Rows)
            {
                dem++;
            }
            sbHTML.Append("<tr>");
            dem = dem + 1;
            sbHTML.Append("<td rowspan = '" + dem + "'>" + _tool_msg + "</td>");

            sbHTML.Append("<td rowspan = '" + dem1 + "'>" + _Name + "</td>");
            String _partName = "";
            String _partNumber = "";
            foreach (DataRow dr in _strPart1.Rows)
            {
                _partNumber = dr["PARTNUMBER"].ToString();
                _partName = dr["PARTNAME"].ToString();

                float _stock1 = float.Parse(dr["STOCK_QTY"].ToString());
                _stock += _stock1;
                //CREATE PLAN ROW



                sbHTML.Append("<td class='partnumber'>" + _partNumber + "</td>");
                sbHTML.Append("<td>LG Plan</td><td></td>");

                for (Int32 i = 0; i < _max_day; ++i)
                {
                    

                    float total = 0;

                    DataTable _strDeman = DataProvider.GetProdid1(_partNumber);
                    foreach (DataRow dr1 in _strDeman.Rows)
                    {
                        total = float.Parse(dr1["D"+(i + 1)+""].ToString());

                    }
                    arrPlanConsumsion[i] = arrPlanConsumsion[i] + total;
                    string css = total > 0 ? "yes-chedule" : "no-schedule";
                    sbHTML.Append("<td class='p" + (i + 1).ToString() + " plan " + css + "'>" + (total > 0 ? total.ToString() : "-") + "</td>");


                }

                sbHTML.Append("</tr>");





            }



            //CREATE CAPA ROW



            DataTable theTableMold32 = DataProvider.GetNumberName(_partNumber);
            String _venDer = "";
            foreach (DataRow dr1 in theTableMold32.Rows)
            {
                //CREATE HEADER
                String _modeCode = dr1["MOLD_CODE"].ToString();
                _venDer = dr1["VENDER"].ToString();
                sbHTML.Append(" <tr>");
                //sbHTML.Append("<td></td>");
                sbHTML.Append("<td>" + dr1["MOLD_NAME"].ToString() + "</td>");
                sbHTML.Append("<td>" + dr1["MACHINE"].ToString() + "</td>");
                sbHTML.Append("<td>Capa</td><td>" + dr1["CAPACITY"].ToString() + "</td>");

                for (Int32 i = 0; i < _max_day; ++i)
                {
                    String _plan_date = _cur_date.AddDays(i).ToString("yyyyMMdd");
                    float _quantity = 0;
                    DataTable _strPart2 = DataProvider.GetPartTool1(_tool_msg);
                    foreach (DataRow dr in _strPart2.Rows)
                    {
                        //String _partNumber = dr["PARTNUMBER"].ToString();
                        // String _partName = dr["PARTNAME"].ToString();
                        DataRow[] arrRow = _tbMoldSchedule.Select("MOLD_CODE = '" + _modeCode + "' AND PLAN_DATE = '" + _plan_date + "'");

                        if (arrRow.Length != 0)
                        {
                            _quantity = float.Parse(arrRow[0]["QUANTITY"].ToString());
                        }
                    }
                    arrCapa[i] = arrCapa[i] + _quantity;

                    string css = _quantity > 0 ? "yes-chedule" : "no-schedule";

                    sbHTML.Append("<td class='" + css + "'>" + (_quantity > 0 ? _quantity.ToString() : "-") + "</td>");
                }

                sbHTML.Append("</tr>");

            }

            //CREATE STOCK ROW
            sbHTML.Append("<tr>");
            //sbHTML.Append("<td></td>");
            sbHTML.Append("<td class='sizetool1'>" + _venDer.ToString() + "</td><td></td>");
            sbHTML.Append("<td>STOCK</td><td>" + _stock.ToString() + "</td>");

            float _cur_stock = _stock;

            for (Int32 i = 0; i < _max_day; ++i)
            {
                _cur_stock = _cur_stock - arrPlanConsumsion[i];

                string css = _cur_stock > 0 ? "enought-stock" : "shortage-stock";
                //sbHTML.Append("<td class='" + css + "'>" + _cur_stock.ToString() + "</td>");
                sbHTML.Append("<td class='" + css + "'>" + _cur_stock.ToString() + "</td>");
                _cur_stock = _cur_stock + arrCapa[i];

            }

            sbHTML.Append("</table>");
            sbHTML.Append("</div>");



            return sbHTML;

        }
        private StringBuilder RenderLayoutForTool(String _tool_msg, String _Name)
        {
            StringBuilder sbHTML = new StringBuilder();
            float[] arrCapa = new float[_max_day];
            float[] arrPlanConsumsion = new float[_max_day];
            //CREATE HEADER

            sbHTML.Append("<div class='scrollbar1'>");
            sbHTML.Append("<table class='table-shedule'>");
            sbHTML.Append("<tr>");
            sbHTML.Append("<td class='sizetool'>Tool Info</td>");
            sbHTML.Append("<td class='sizetool1'>Part Name</td>");
            sbHTML.Append("<td class='sizetool2'>Part Number</td>");
            sbHTML.Append("<td class='sizetool3'></td><td class='sizetool4'></td>");
            for (Int32 i = 0; i < _max_day; ++i)
            {
                sbHTML.Append("<td class='d" + (i + 1).ToString() + " day'>" + _cur_date.AddDays(i).ToString("MMM-d") + "</td>");

                //SET VALUE for arrCapa and ArrPlanConsumsion
                arrCapa[i] = 0;
                arrPlanConsumsion[i] = 0;
            }
            sbHTML.Append("</tr>");

            int dem = 0;
            int dem1 = 0;
            // GET PARTNUMBER DATA
            float _stock = 0;
            DataTable _strPart1 = DataProvider.GetPartInTool(_tool_msg, _Name);
            foreach (DataRow dr in _strPart1.Rows)
            {
                dem++;
                dem1++;
            }

            DataTable theTableMold = DataProvider.GetDataPartMoldByParNumber(_tool_msg);

            foreach (DataRow dr1 in theTableMold.Rows)
            {
                dem++;
            }
            sbHTML.Append("<tr>");
            dem = dem + 1;
            sbHTML.Append("<td rowspan = '" + dem + "'>" + _tool_msg + "</td>");

            sbHTML.Append("<td rowspan = '" + dem1 + "'>" + _Name + "</td>");
            String _partName = "";
            String _partNumber = "";
            foreach (DataRow dr in _strPart1.Rows)
            {
                 _partNumber = dr["PARTNUMBER"].ToString();
                 _partName = dr["PARTNAME"].ToString();

                float _stock1 = float.Parse(dr["STOCK_QTY"].ToString());
                _stock += _stock1;
                //CREATE PLAN ROW

                

                sbHTML.Append("<td class='partnumber'>" + _partNumber + "</td>");
                sbHTML.Append("<td>LG Plan</td><td></td>");

                for (Int32 i = 0; i < _max_day; ++i)
                {
                    String strPlanDay = _cur_date.AddDays(i).ToString("yyyyMMdd");

                    float total = 0;

                    float _prodPlan = 0;
                    float _comQTY = 0;
                    DataTable _strDeman = DataProvider.GetProdid(_partNumber, strPlanDay);
                    foreach (DataRow dr1 in _strDeman.Rows)
                    {
                        _prodPlan = float.Parse(dr1["PROD_PLAN"].ToString());
                        _comQTY = float.Parse(dr1["COMPONENT_QUANTITY"].ToString());
                        total += _prodPlan * _comQTY;
                    }
                    arrPlanConsumsion[i] = arrPlanConsumsion[i] + total;
                    string css = total > 0 ? "yes-chedule" : "no-schedule";
                    sbHTML.Append("<td class='p" + (i + 1).ToString() + " plan " + css + "'>" + (total > 0 ? total.ToString() : "-") + "</td>");


                }

                sbHTML.Append("</tr>");


                
                

            }



            //CREATE CAPA ROW



            DataTable theTableMold32 = DataProvider.GetNumberName(_partNumber);
                
                foreach (DataRow dr1 in theTableMold32.Rows)
                {
                    //CREATE HEADER
                    String _modeCode = dr1["MOLD_CODE"].ToString();
                    sbHTML.Append(" <tr>");
                    //sbHTML.Append("<td></td>");
                    sbHTML.Append("<td>" + dr1["MOLD_NAME"].ToString() + "</td>");
                    sbHTML.Append("<td>" + dr1["MACHINE"].ToString() + "</td>");
                    sbHTML.Append("<td>Capa</td><td>" + dr1["CAPACITY"].ToString() + "</td>");

                    for (Int32 i = 0; i < _max_day; ++i)
                    {
                        String _plan_date = _cur_date.AddDays(i).ToString("yyyyMMdd");
                        float _quantity = 0;
                        DataTable _strPart2 = DataProvider.GetPartTool1(_tool_msg);
                        foreach (DataRow dr in _strPart2.Rows)
                        {
                            //String _partNumber = dr["PARTNUMBER"].ToString();
                           // String _partName = dr["PARTNAME"].ToString();
                            DataRow[] arrRow = _tbMoldSchedule.Select("MOLD_CODE = '" + _modeCode + "' AND PLAN_DATE = '" + _plan_date + "'");

                            if (arrRow.Length != 0)
                            {
                                _quantity = float.Parse(arrRow[0]["QUANTITY"].ToString());
                            }
                        }
                        arrCapa[i] = arrCapa[i] + _quantity;

                        string css = _quantity > 0 ? "yes-chedule" : "no-schedule";

                        sbHTML.Append("<td class='" + css + "'>" + (_quantity > 0 ? _quantity.ToString() : "-") + "</td>");
                    }

                    sbHTML.Append("</tr>");
                
            }

                //CREATE STOCK ROW
                sbHTML.Append("<tr>");
                //sbHTML.Append("<td></td>");
                sbHTML.Append("<td></td><td></td>");
                sbHTML.Append("<td>STOCK</td><td>" + _stock.ToString() + "</td>");

                float _cur_stock = _stock;

                for (Int32 i = 0; i < _max_day; ++i)
                {
                    _cur_stock = _cur_stock - arrPlanConsumsion[i];

                    string css = _cur_stock > 0 ? "enought-stock" : "shortage-stock";
                    //sbHTML.Append("<td class='" + css + "'>" + _cur_stock.ToString() + "</td>");
                    sbHTML.Append("<td class='" + css + "'>" + _cur_stock.ToString() + "</td>");
                    _cur_stock = _cur_stock + arrCapa[i];

                }

            sbHTML.Append("</table>");
            sbHTML.Append("</div>");


            
            return sbHTML;

        }
        private StringBuilder RenderLayoutForPartNumber(string _partNumber, string _partName, Int32 _partLevel, float _stock, String _tool_msg)
        {
            StringBuilder sbHTML = new StringBuilder();
            float[] arrCapa = new float[_max_day];
            float[] arrPlanConsumsion = new float[_max_day];

            //CREATE HEADER
            
            sbHTML.Append("<div class='scrollbar1'>");
            sbHTML.Append("<table class='table-shedule'>");
            sbHTML.Append("<tr>");
            sbHTML.Append("<td class='sizetool'>Tool Info</td>");
            sbHTML.Append("<td class='sizetool1'>Part Name</td>");
            sbHTML.Append("<td class='sizetool2'>Part Number</td>");
            sbHTML.Append("<td class='sizetool3'></td><td class='sizetool4'></td>");

            for (Int32 i = 0; i < _max_day; ++i)
            {
                sbHTML.Append("<td class='d" + (i + 1).ToString() + " day'>" + _cur_date.AddDays(i).ToString("MMM-d") + "</td>");

                //SET VALUE for arrCapa and ArrPlanConsumsion
                arrCapa[i] = 0;
                arrPlanConsumsion[i] = 0;
            }

            sbHTML.Append("</tr>");

            //CREATE PLAN ROW
            sbHTML.Append("<tr>");
            sbHTML.Append("<td>" + _tool_msg + "</td>");
            sbHTML.Append("<td>" + _partName + "</td>");
            sbHTML.Append("<td class='partnumber'>" + _partNumber + "</td>");
            sbHTML.Append("<td>LG Plan</td><td></td>");

            for (Int32 i = 0; i < _max_day; ++i)
            {
                String strPlanDay = _cur_date.AddDays(i).ToString("yyyyMMdd");
                
                float total = 0;
               
                float _prodPlan = 0;
                float _comQTY = 0;
                DataTable _strDeman = DataProvider.GetProdid(_partNumber,strPlanDay);
                foreach (DataRow dr1 in _strDeman.Rows)
                {
                     _prodPlan = float.Parse(dr1["PROD_PLAN"].ToString());
                     _comQTY = float.Parse(dr1["COMPONENT_QUANTITY"].ToString());
                    total += _prodPlan * _comQTY;
                }
                arrPlanConsumsion[i] = total;
                string css = total > 0 ? "yes-chedule" : "no-schedule";
                sbHTML.Append("<td class='p" + (i + 1).ToString() + " plan " + css + "'>" + (total > 0 ? total.ToString() : "-") + "</td>");


            }

            sbHTML.Append("</tr>");

            //CREATE CAPA ROW
            DataTable theTableMold1 = DataProvider.GetDataPartMoldByParNumber1(_partNumber);

            foreach (DataRow dr in theTableMold1.Rows)
            {
                //CREATE HEADER
                String _modeCode = dr["MOLD_CODE"].ToString();
                sbHTML.Append(" <tr>");
                sbHTML.Append("<td></td>");
                sbHTML.Append("<td>" + dr["MOLD_NAME"].ToString() + "</td>");
                sbHTML.Append("<td>" + dr["MACHINE"].ToString() + "</td>");
                sbHTML.Append("<td>Capa</td><td>" + dr["CAPACITY"].ToString() + "</td>");

                for (Int32 i = 0; i < _max_day; ++i)
                {
                    String _plan_date = _cur_date.AddDays(i).ToString("yyyyMMdd");
                    float _quantity = 0;

                    DataRow[] arrRow = _tbMoldSchedule.Select("PARTNUMBER = '" + _partNumber + "' AND MOLD_CODE = '" + _modeCode + "' AND PLAN_DATE = '" + _plan_date + "'");

                    if (arrRow.Length != 0)
                    {
                        _quantity = float.Parse(arrRow[0]["QUANTITY"].ToString());
                    }

                    arrCapa[i] = arrCapa[i] + _quantity;

                    string css = _quantity > 0 ? "yes-chedule" : "no-schedule";

                    sbHTML.Append("<td class='" + css + "'>" + (_quantity > 0 ? _quantity.ToString() : "-") + "</td>");
                }

                sbHTML.Append("</tr>");
            }

            //CREATE STOCK ROW
            sbHTML.Append("<tr>");
            sbHTML.Append("<td></td>");
            sbHTML.Append("<td></td><td></td>");
            sbHTML.Append("<td>STOCK</td><td>" + _stock.ToString() + "</td>");

            float _cur_stock = _stock;

            for (Int32 i = 0; i < _max_day; ++i)
            {
                _cur_stock = _cur_stock - arrPlanConsumsion[i];

                string css = _cur_stock > 0 ? "enought-stock" : "shortage-stock";
                sbHTML.Append("<td class='" + css + "'>" + _cur_stock.ToString() + "</td>");

                _cur_stock = _cur_stock + arrCapa[i];

            }

            sbHTML.Append("</table>");
            sbHTML.Append("</div>");
            return sbHTML;
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (this.ddlDropDownORG.SelectedValue != "-1" )
            {
                _org_id = this.ddlDropDownORG.SelectedValue;
                _tool = this.DropDownList1.SelectedValue;
               // LoadData();
                RederLayout();
            }
            else
            {
                this.ltrHTML.Text = "";
                return;
            }
        }


        protected void btnView1_Click(object sender, EventArgs e)
        {
            if (this.ddlDropDownORG.SelectedValue != "-1")
            {
                _org_id = this.ddlDropDownORG.SelectedValue;
                _tool = this.DropDownList1.SelectedValue;
                _vender = this.DropDownList2.SelectedValue;
                LoadData1();
                RederLayout1();
            }
            else
            {
                this.ltrHTML.Text = "";
                return;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDropDownORG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            DataTable _strPartnumber = DataProvider.GetPartPartNumber();
            int dem = 0;
             String _partNumber = "";
             float[] a = new float[40]; 
             foreach (DataRow dr in _strPartnumber.Rows)
             {
                 _partNumber = dr["PARTNUMBER"].ToString();
                 for (Int32 i = 0; i <= 30; ++i)
                 {
                     String strPlanDay = _cur_date.AddDays(i).ToString("yyyyMMdd");

                     int total = 0;
                      
                     int _prodPlan = 0;
                     int _comQTY = 0;
                     DataTable _strDeman = DataProvider.GetProdid(_partNumber, strPlanDay);
                     foreach (DataRow dr1 in _strDeman.Rows)
                     {
                         _prodPlan = int.Parse(dr1["PROD_PLAN"].ToString());
                         _comQTY = int.Parse(dr1["COMPONENT_QUANTITY"].ToString());
                         total += _prodPlan * _comQTY;
                         
                     }
                     a[i] = total;

                 }


                 if (DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_PLANPRODUCTION WHERE PARTNUMBER = '" + _partNumber + "'").Rows.Count < 1)
                 {
                     StringBuilder sbSQL = new StringBuilder();
                     sbSQL.Append("INSERT INTO PSI_PLANPRODUCTION(PARTNUMBER, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29, D30 ) VALUES(");
                     sbSQL.Append("'" + _partNumber + "'");
                     sbSQL.Append(",'" + a[0] + "'");
                     sbSQL.Append(",'" + a[1] + "'");
                     sbSQL.Append(",'" + a[2] + "'");
                     sbSQL.Append(",'" + a[3] + "'");
                     sbSQL.Append(",'" + a[4] + "'");
                     sbSQL.Append(",'" + a[5] + "'");
                     sbSQL.Append(",'" + a[6] + "'");
                     sbSQL.Append(",'" + a[7] + "'");
                     sbSQL.Append(",'" + a[8] + "'");
                     sbSQL.Append(",'" + a[9] + "'");
                     sbSQL.Append(",'" + a[10] + "'");
                     sbSQL.Append(",'" + a[11] + "'");
                     sbSQL.Append(",'" + a[12] + "'");
                     sbSQL.Append(",'" + a[13] + "'");
                     sbSQL.Append(",'" + a[14] + "'");
                     sbSQL.Append(",'" + a[15] + "'");
                     sbSQL.Append(",'" + a[16] + "'");
                     sbSQL.Append(",'" + a[17] + "'");
                     sbSQL.Append(",'" + a[18] + "'");
                     sbSQL.Append(",'" + a[19] + "'");
                     sbSQL.Append(",'" + a[20] + "'");
                     sbSQL.Append(",'" + a[21] + "'");
                     sbSQL.Append(",'" + a[22] + "'");
                     sbSQL.Append(",'" + a[23] + "'");
                     sbSQL.Append(",'" + a[24] + "'");
                     sbSQL.Append(",'" + a[25] + "'");
                     sbSQL.Append(",'" + a[26] + "'");
                     sbSQL.Append(",'" + a[27] + "'");
                     sbSQL.Append(",'" + a[28] + "'");
                     sbSQL.Append(",'" + a[29] + "'");
                     sbSQL.Append(") ");
                    int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());
                    
                     dem = 2;
                 }
                 else
                 {
                     StringBuilder sbSQL = new StringBuilder();
                     sbSQL.Append("UPDATE PSI_PLANPRODUCTION SET ");
                     sbSQL.Append(" D1 = '" + a[0] + "'");
                     sbSQL.Append(", D2 = '" + a[1] + "'");
                     sbSQL.Append(", D3 = '" + a[2] + "'");
                     sbSQL.Append(", D4 = '" + a[3] + "'");
                     sbSQL.Append(", D5 = '" + a[4] + "'");
                     sbSQL.Append(", D6 = '" + a[5] + "'");
                     sbSQL.Append(", D7 = '" + a[6] + "'");
                     sbSQL.Append(", D8 = '" + a[7] + "'");
                     sbSQL.Append(", D9 = '" + a[8] + "'");
                     sbSQL.Append(", D10 = '" + a[9] + "'");
                     sbSQL.Append(", D11 = '" + a[10] + "'");
                     sbSQL.Append(", D12 = '" + a[11] + "'");
                     sbSQL.Append(", D13 = '" + a[12] + "'");
                     sbSQL.Append(", D14 = '" + a[13] + "'");
                     sbSQL.Append(", D15 = '" + a[14] + "'");
                     sbSQL.Append(", D16 = '" + a[15] + "'");
                     sbSQL.Append(", D17 = '" + a[16] + "'");
                     sbSQL.Append(", D18 = '" + a[17] + "'");
                     sbSQL.Append(", D19 = '" + a[18] + "'");
                     sbSQL.Append(", D20 = '" + a[19] + "'");
                     sbSQL.Append(", D21 = '" + a[20] + "'");
                     sbSQL.Append(", D22 = '" + a[21] + "'");
                     sbSQL.Append(", D23 = '" + a[22] + "'");
                     sbSQL.Append(", D24 = '" + a[23] + "'");
                     sbSQL.Append(", D25 = '" + a[24] + "'");
                     sbSQL.Append(", D26 = '" + a[25] + "'");
                     sbSQL.Append(", D27 = '" + a[26] + "'");
                     sbSQL.Append(", D28 = '" + a[27] + "'");
                     sbSQL.Append(", D29 = '" + a[28] + "'");
                     sbSQL.Append(", D30 = '" + a[29] + "'");
                     sbSQL.Append(" WHERE PARTNUMBER = '" + _partNumber + "' ");
                     int iResult = DBConnection.ExcuteCommandText(sbSQL.ToString());
                    
                     dem = 2; 
                 }

                 

                 

                 




             }






        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}