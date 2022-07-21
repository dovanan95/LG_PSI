using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSI_WEBFORM
{
    public partial class MoldSchedule : System.Web.UI.Page
    {

        private DataTable theTablePartNumber = new DataTable();
        private DataTable theTableMoldScheulde = new DataTable();
        private DataTable theTableMoldScheulde1 = new DataTable();
        private DataTable theTableMoldScheulde111 = new DataTable();
        private const Int32 _MAX_DAY = 30;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMoldList();
                this.cdStart.SelectedDate = DateTime.Now;
                string reaLGE = Session["USER_CODE"].ToString();
                if (reaLGE == "readonly")
                {
                    this.btnSave.Visible = false;
                }
            }
        }

        private void LoadData()
        {

            //PART NUMBER FOR MOLD
            theTablePartNumber = DataProvider.GetDataPartMoldByMoldCode(ddlMold.SelectedValue);
            //END ************************************************************

            //GET PLAN PRODUCTION FOR MOLD
            DateTime dt = cdStart.SelectedDate;
            StringBuilder sbDateList = new StringBuilder("('" + dt.ToString("yyyyMMdd") + "'");

            for (Int32 i = 1; i < _MAX_DAY; ++i)
            {
                sbDateList.Append(",'" + dt.AddDays(i).ToString("yyyyMMdd") + "'");
            }

            sbDateList.Append(")");

            theTableMoldScheulde111 = DataProvider.GetDataMoldScheduleByModeCode(ddlMold.SelectedValue, sbDateList.ToString());
            //END ************************************************************
        }


        private void RenderHTML()
        {
            string mold1 = ddlMold.SelectedValue;
            StringBuilder sbHTML = new StringBuilder();
            DateTime dtStart = cdStart.SelectedDate;
            if (mold1 != "ALL")
            {

                sbHTML.Append("<div class='scrollbar3'>");
                sbHTML.Append("<table class='table-list6'>");
                sbHTML.Append("<tr class='sizetool14'>");
                sbHTML.Append("<th class='sizetool10' rowspan = '2'>" + ddlMold.SelectedValue + "</th>");
                //sbHTML.Append("<th class='sizetool10' style='width: 80px'> VENDER</th>");
                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<th class='sizetool13'>" + dtStart.AddDays(i).ToString("dd-MMM") + "</th>");
                }

                sbHTML.Append("</tr>");

                sbHTML.Append("<tr>");

                //sbHTML.Append("<td class='sizetool11'> adsđfggfd </td>");


                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<td class='sizetool11'>");
                    sbHTML.Append(RenderPartListControl(ddlMold.SelectedValue, dtStart.AddDays(i).ToString("yyyyMMdd")));
                    sbHTML.Append("</td>");
                }


                sbHTML.Append("</tr>");

                sbHTML.Append("</table>");
                sbHTML.Append("</div>");
                this.ltrHTML.Text = sbHTML.ToString();
            }
            else
            {
                string vender = Session["VENDER"].ToString();
                if (vender == "LGE")
                {
                    sbHTML.Append("<div class='scrollbar3'>");
                    sbHTML.Append("<table class='table-list6'>");
                    DataTable _strmold = DataProvider.GetMold89(ddlORG.SelectedValue, vender);
                    sbHTML.Append("<tr class='sizetool14'>");
                    sbHTML.Append("<th class='sizetool10' style='width: 130px;'> MOLD CODE</th>");
                    //sbHTML.Append("<th class='sizetool10' style='width: 80px'> VENDER</th>");
                    for (Int32 i = 0; i < 30; ++i)
                    {
                        sbHTML.Append("<th class='sizetool13' style='width: 55px;'>" + dtStart.AddDays(i).ToString("dd-MMM") + "</th>");
                    }

                    sbHTML.Append("</tr>");
                    
                    foreach (DataRow dr in _strmold.Rows)
                    {
                        string _partmold = dr["MOLD_CODE"].ToString();
                        string _vender = dr["VENDER"].ToString();

                        sbHTML.Append("<tr>");

                        //sbHTML.Append("<td class='sizetool11'> adsđfggfd </td>");
                        sbHTML.Append("<th class='sizetool10'>" + _partmold + "</th>");

                        for (Int32 i = 0; i < 30; ++i)
                        {
                            sbHTML.Append("<td class='sizetool11'>");
                            sbHTML.Append(RenderPartListControl(_partmold, dtStart.AddDays(i).ToString("yyyyMMdd")));
                            sbHTML.Append("</td>");
                        }


                        sbHTML.Append("</tr>");
                    }
                    sbHTML.Append("</table>");
                    sbHTML.Append("</div>");
                    this.ltrHTML.Text = sbHTML.ToString();
                }
                else
                {

                }
            }
        }

        private void LoadMoldList()
        {
            DataTable theTable = new DataTable();
            string vender = Session["VENDER"].ToString();
            string vender1 = "";
            if (vender != "LGE")
            {
                vender1 = "AND VENDER = '" + vender + "' ";
            }
            theTable = DBConnection.GetDataTableByCommandText("SELECT * FROM PSI_MOLD WHERE ACTIVE = 1 " + vender1 + "" );
            
            this.ddlMold.DataTextField = "MOLD_NAME";
            this.ddlMold.DataValueField = "MOLD_CODE";
            this.ddlMold.DataSource = theTable;
            this.ddlMold.DataBind();
            this.ddlMold.Items.Add("ALL");

            if (vender != "LGE")
            {
                this.DropDownList1.Items.Add(""+ vender +"");
            }
            else
            {
                theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT VENDER FROM PSI_MOLD WHERE ACTIVE = 1 and VENDER is not null ");

                this.DropDownList1.DataTextField = "VENDER";
                this.DropDownList1.DataValueField = "VENDER";
                this.DropDownList1.DataSource = theTable;
                this.DropDownList1.DataBind();
            }
        }

        
        private String RenderPartListControl(String _moldeCode, String _date)
        {
            StringBuilder sbHTML = new StringBuilder();

            sbHTML.Append("<table>");

            //foreach (DataRow dr in theTablePartNumber.Rows)
            //{
                float _quanlity = 0;

                //String _text_id = _moldeCode + "_" + dr["PARTNUMBER"].ToString() + "_" + _date;
                //DataRow[] arrRows = theTableMoldScheulde.Select("PARTNUMBER = '" + dr["PARTNUMBER"].ToString() + "' AND PLAN_DATE = '" + _date + "'");

                String _text_id = _moldeCode + "_" + _date;


                DateTime dt = cdStart.SelectedDate;
                StringBuilder sbDateList = new StringBuilder("('" + dt.ToString("yyyyMMdd") + "'");

                for (Int32 i = 1; i < _MAX_DAY; ++i)
                {
                    sbDateList.Append(",'" + dt.AddDays(i).ToString("yyyyMMdd") + "'");
                }

                sbDateList.Append(")");

                theTableMoldScheulde111 = DataProvider.GetDataMoldScheduleByModeCode(_moldeCode, sbDateList.ToString());
                DataRow[] arrRows = theTableMoldScheulde111.Select(" PLAN_DATE = '" + _date + "'");

                if (arrRows.Length > 0)
                {
                    _quanlity = float.Parse(arrRows[0]["QUANTITY"].ToString());
                }

                sbHTML.Append("<tr>");
                //sbHTML.Append("<td>" + dr["PARTNUMBER"].ToString() + "</td>");
                sbHTML.Append("<td class='sizetool12'><input name='" + _text_id + "' type='text' class='text-small' style='width: 70px;' value='" + _quanlity.ToString() + "'>");
                sbHTML.Append("<input name='original_" + _text_id + "' type='text' style='display:none; width: 50px;' value='" + _quanlity.ToString() + "'></td>");
                sbHTML.Append("</tr>");
            //}

            sbHTML.Append("</table>");
            
            return sbHTML.ToString();
        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            LoadData();

            //RENDER HTML FOR DISPLAYING
            RenderHTML();
        }

        private String RenderPartListControl1(String _moldeCode, String _date)
        {
            StringBuilder sbHTML = new StringBuilder();

            //sbHTML.Append("<table>");
            float _quanlity = 0;
            String _text_id = _moldeCode + "_" + _date;

            DateTime dt = cdStart.SelectedDate;
            StringBuilder sbDateList = new StringBuilder("('" + dt.ToString("yyyyMMdd") + "'");

            for (Int32 i = 1; i < _MAX_DAY; ++i)
            {
                sbDateList.Append(",'" + dt.AddDays(i).ToString("yyyyMMdd") + "'");
            }

            sbDateList.Append(")");

            theTableMoldScheulde111 = DataProvider.GetDataMoldScheduleByModeCode(_moldeCode, sbDateList.ToString());

            DataRow[] arrRows = theTableMoldScheulde111.Select(" PLAN_DATE = '" + _date + "'");

            if (arrRows.Length > 0)
            {
                _quanlity = float.Parse(arrRows[0]["QUANTITY"].ToString());
            }

            //sbHTML.Append("<tr>");


            string css = _quanlity > 0 ? "yes-chedule" : "no-schedule";
            sbHTML.Append("<div class='p" + (1).ToString() + " plan " + css + "'  >" + (_quanlity > 0 ? _quanlity.ToString() : "-") + "</div>");

            //sbHTML.Append("<td style='padding-left:30px;'>" + _quanlity.ToString() + "</td>");
            //sbHTML.Append("</tr>");
        

            //sbHTML.Append("</table>");

            return sbHTML.ToString();
        }

        private void RenderHTML1()
        {
            string mold = ddlMold.SelectedValue;
            StringBuilder sbHTML = new StringBuilder();
            DateTime dtStart = cdStart.SelectedDate;
            if (mold != "ALL")
            {
                sbHTML.Append("<div class='scrollbar3'>");
                sbHTML.Append("<table class='table-list6'>");

                sbHTML.Append("<tr class='sizetool14'>");
                sbHTML.Append("<th class='sizetool15' rowspan = '2'>" + ddlMold.SelectedValue + "(" + ddlMold.SelectedItem.Text + ")</th>");
                sbHTML.Append("<th class='sizetool10' style='width: 80px'> VENDER</th>");
                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<th class='sizetool16'>" + dtStart.AddDays(i).ToString("dd-MMM") + "</th>");
                }

                sbHTML.Append("</tr>");

                sbHTML.Append("<tr>");

                string vender = Session["VENDER"].ToString();
                DataTable _strmold = DataProvider.GetMoldListByModeCode(ddlMold.SelectedValue, ddlORG.SelectedValue, vender);
                string _vender = "";
                foreach (DataRow dr in _strmold.Rows)
                {
                    string _partmold = dr["MOLD_CODE"].ToString();
                    _vender = dr["VENDER"].ToString();
                }
                sbHTML.Append("<td class='sizetool11'> "+ _vender.ToString() +" </td>");

                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<td class='sizetool11'>");

                    sbHTML.Append(RenderPartListControl1(ddlMold.SelectedValue, dtStart.AddDays(i).ToString("yyyyMMdd")));

                    sbHTML.Append("</td>");
                }


                sbHTML.Append("</tr>");



                sbHTML.Append("</table>");
                sbHTML.Append("</div>");
                this.ltrHTML.Text = sbHTML.ToString();
            }
            else
            {

                string vender = Session["VENDER"].ToString();
                DataTable _strmold = DataProvider.GetMold89(ddlORG.SelectedValue, vender);
                sbHTML.Append("<div class='scrollbar3'>");
                sbHTML.Append("<table class='table-list6'>");
                sbHTML.Append("<tr class='sizetool14'>");
                sbHTML.Append("<th class='sizetool15'>MOLD CODE</th>");
                sbHTML.Append("<th class='sizetool10' style='width: 80px'> VENDER</th>");
                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<th class='sizetool16'>" + dtStart.AddDays(i).ToString("dd-MMM") + "</th>");
                }

                sbHTML.Append("</tr>");

                foreach (DataRow dr in _strmold.Rows)
                {
                    string _partmold = dr["MOLD_CODE"].ToString();
                    string _vender = dr["VENDER"].ToString();
                    
                    //sbHTML.Append(RenderPartListControl1(_partmold, dtStart.AddDays(i).ToString("yyyyMMdd")));


                    
                    

                    sbHTML.Append("<tr>");

                    sbHTML.Append("<td class='sizetool11'> "+ _partmold +" </td>");
                    sbHTML.Append("<td class='sizetool11'> " + _vender +" </td>");

                    for (Int32 i = 0; i < 30; ++i)
                    {
                        sbHTML.Append("<td class='sizetool11'>");

                        sbHTML.Append(RenderPartListControl1(_partmold, dtStart.AddDays(i).ToString("yyyyMMdd")));

                        sbHTML.Append("</td>");
                    }


                    sbHTML.Append("</tr>");



                    
                }
                sbHTML.Append("</table>");
                sbHTML.Append("</div>");
                this.ltrHTML.Text = sbHTML.ToString();

            }
            
            

        }

        private void RenderHTMLALL()
        {

            StringBuilder sbHTML = new StringBuilder();
            DateTime dtStart = cdStart.SelectedDate;
            string vender = DropDownList1.SelectedValue;
            DataTable _strmold = DataProvider.GetMold89(ddlORG.SelectedValue, vender);
            sbHTML.Append("<div class='scrollbar3'>");
            sbHTML.Append("<table class='table-list6'>");
            sbHTML.Append("<tr class='sizetool14'>");
            sbHTML.Append("<th class='sizetool15'>MOLD CODE</th>");
            sbHTML.Append("<th class='sizetool10' style='width: 80px'> VENDER</th>");
            for (Int32 i = 0; i < 30; ++i)
            {
                sbHTML.Append("<th class='sizetool16'>" + dtStart.AddDays(i).ToString("dd-MMM") + "</th>");
            }

            sbHTML.Append("</tr>");
            foreach (DataRow dr in _strmold.Rows)
            {
                string _partmold = dr["MOLD_CODE"].ToString();
                string _vender = dr["VENDER"].ToString();

                //sbHTML.Append(RenderPartListControl1(_partmold, dtStart.AddDays(i).ToString("yyyyMMdd")));





                sbHTML.Append("<tr>");

                sbHTML.Append("<td class='sizetool11'> " + _partmold + " </td>");
                sbHTML.Append("<td class='sizetool11'> " + _vender + " </td>");

                for (Int32 i = 0; i < 30; ++i)
                {
                    sbHTML.Append("<td class='sizetool11'>");

                    sbHTML.Append(RenderPartListControl1(_partmold, dtStart.AddDays(i).ToString("yyyyMMdd")));

                    sbHTML.Append("</td>");
                }


                sbHTML.Append("</tr>");




            }
            sbHTML.Append("</table>");
            sbHTML.Append("</div>");
            this.ltrHTML.Text = sbHTML.ToString();

            
           
            
            

        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
            RenderHTML1();
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadData();
            RenderHTMLALL();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string mold1 = ddlMold.SelectedValue;
            StringBuilder sbHTML = new StringBuilder();

            if (mold1 != "ALL")
            {
                String strMoldCode = this.ddlMold.SelectedValue;
                DateTime dtStart = cdStart.SelectedDate;
                LoadData();
                Hashtable ht = new Hashtable();
                /*
                foreach (DictionaryEntry entry in hashtable)
                {
                    Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
                }*/


                //GET ALL DATA FROM CONTROL 
                for (int i = 0; i < _MAX_DAY; ++i)
                {
                    //foreach (DataRow dr in theTablePartNumber.Rows)
                    //{
                    string _str_name = strMoldCode + "_" + dtStart.AddDays(i).ToString("yyyyMMdd");
                    string value = String.Format("{0}", Request.Form[_str_name]);
                    string valueOriginal = String.Format("{0}", Request.Form["original_" + _str_name]);

                    if (value == valueOriginal)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            float f_value = float.Parse(value);
                            ht.Add(_str_name, value);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    //}
                }

                //INSERT 


                //CREATION SQL TEXT
                foreach (DictionaryEntry entry in ht)
                {
                    StringBuilder sbSQL = new StringBuilder();
                    String _moldCode = entry.Key.ToString().Split('_')[0];
                    //String _partNumber = entry.Key.ToString().Split('_')[1];
                    String _plan_date = entry.Key.ToString().Split('_')[1];
                    float _quantity = float.Parse(entry.Value.ToString());
                    float _capa = 0;
                    string strUser = Session["USER_CODE"].ToString();
                    string strDate = DateTime.Now.ToString();
                    DataTable _strPart1 = DataProvider.GetMOLDCapacity(_moldCode);
                    foreach (DataRow dr in _strPart1.Rows)
                    {
                         _capa = float.Parse(dr["CAPACITY"].ToString());
                    }
                    if(_capa >= _quantity)
                    {
                        DataRow[] arrRows = theTableMoldScheulde111.Select("MOLD_CODE = '" + _moldCode + "' AND PLAN_DATE = '" + _plan_date + "'");

                        if (arrRows.Length > 0)
                        {

                            DataTable _tb = DataProvider.GetDataMoldScheduleByModeCode(_moldCode, _plan_date);
                            float _quantity1 = 0;
                            string _updateperson = "";
                            string _updatetime  = "";
                            foreach (DataRow dr1 in _tb.Rows)
                            {
                              _quantity1 = float.Parse(dr1["QUANTITY"].ToString());
                              _updateperson = dr1["UPDATE_PERSON"].ToString();
                              _updatetime = dr1["UPDATE_TIME"].ToString();
                            }
                            StringBuilder sbSQL2 = new StringBuilder();
                            sbSQL2.Append(" INSERT INTO PSI_MOLD_SCHEDULE_BACKUP(MOLD_CODE, QUANTITY, PLAN_DATE, UPDATE_PERSON, UPDATE_TIME)");
                            sbSQL2.Append(" VALUES('" + _moldCode + "', " + _quantity1.ToString() + ",'" + _plan_date + "', '" + _updateperson + "', '" + _updatetime + "')");
                            Int32 result2 = DBConnection.ExcuteCommandText(sbSQL2.ToString());



                            sbSQL.Append(" UPDATE PSI_MOLD_SCHEDULE SET QUANTITY = " + _quantity.ToString() + ", UPDATE_PERSON = '" + strUser + "', UPDATE_TIME = '"+ strDate +"' ");
                            sbSQL.Append(" WHERE MOLD_CODE = '" + _moldCode + "'");
                            //sbSQL.Append(" AND PARTNUMBER = '" + _partNumber + "'");
                            sbSQL.Append(" AND PLAN_DATE = '" + _plan_date + "'");


                        }
                        else
                        {
                            sbSQL.Append(" INSERT INTO PSI_MOLD_SCHEDULE(MOLD_CODE, QUANTITY, PLAN_DATE, UPDATE_PERSON, UPDATE_TIME)");
                            sbSQL.Append(" VALUES('" + _moldCode + "', " + _quantity.ToString() + ",'" + _plan_date + "', '"+ strUser +"', '"+ strDate +"')");


                            //sbSQL.Append(" INSERT INTO PSI_MOLD_SCHEDULE(MOLD_CODE, PARTNUMBER, QUANTITY, PLAN_DATE)");
                            //sbSQL.Append(" VALUES('" + _moldCode + "', '" + _partNumber + "'," + _quantity.ToString() + ",'" + _plan_date + "')");

                        }

                        if (sbSQL.ToString() != "")
                        {
                            Int32 result = DBConnection.ExcuteCommandText(sbSQL.ToString());
                        }
                    }else
                    {
                        WebMsgBox.Show("ERROR: Maximum Capacity = "+ _capa +"");
                    }

                    

                }

                //LOAD DATA
                LoadData();
                //RENDER HTML FOR DISPLAYING
                RenderHTML();
            }
            else
            {
                string vender = Session["VENDER"].ToString();
                if (vender == "LGE")
                {
                    DataTable _strmold = DataProvider.GetMold89(ddlORG.SelectedValue, vender);
                    foreach (DataRow dr in _strmold.Rows)
                    {
                        string _partmold = dr["MOLD_CODE"].ToString();
                        string _vender = dr["VENDER"].ToString();

                        String strMoldCode = dr["MOLD_CODE"].ToString();
                        DateTime dtStart = cdStart.SelectedDate;
                        LoadData();
                        Hashtable ht = new Hashtable();
                        /*
                        foreach (DictionaryEntry entry in hashtable)
                        {
                            Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
                        }*/


                        //GET ALL DATA FROM CONTROL 
                        for (int i = 0; i < _MAX_DAY; ++i)
                        {
                            //foreach (DataRow dr in theTablePartNumber.Rows)
                            //{
                            string _str_name = strMoldCode + "_" + dtStart.AddDays(i).ToString("yyyyMMdd");
                            string value = String.Format("{0}", Request.Form[_str_name]);
                            string valueOriginal = String.Format("{0}", Request.Form["original_" + _str_name]);

                            if (value == valueOriginal)
                            {
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    float f_value = float.Parse(value);
                                    ht.Add(_str_name, value);
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                            //}
                        }

                        //INSERT 


                        //CREATION SQL TEXT
                        foreach (DictionaryEntry entry in ht)
                        {
                            StringBuilder sbSQL = new StringBuilder();
                            String _moldCode = entry.Key.ToString().Split('_')[0];
                            //String _partNumber = entry.Key.ToString().Split('_')[1];
                            String _plan_date = entry.Key.ToString().Split('_')[1];
                            float _quantity = float.Parse(entry.Value.ToString());

                            DataRow[] arrRows = theTableMoldScheulde111.Select("MOLD_CODE = '" + _moldCode + "' AND PLAN_DATE = '" + _plan_date + "'");

                            if (arrRows.Length > 0)
                            {
                                sbSQL.Append(" UPDATE PSI_MOLD_SCHEDULE SET QUANTITY = " + _quantity.ToString());
                                sbSQL.Append(" WHERE MOLD_CODE = '" + _moldCode + "'");
                                //sbSQL.Append(" AND PARTNUMBER = '" + _partNumber + "'");
                                sbSQL.Append(" AND PLAN_DATE = '" + _plan_date + "'");
                            }
                            else
                            {
                                sbSQL.Append(" INSERT INTO PSI_MOLD_SCHEDULE(MOLD_CODE, QUANTITY, PLAN_DATE)");
                                sbSQL.Append(" VALUES('" + _moldCode + "', " + _quantity.ToString() + ",'" + _plan_date + "')");


                                //sbSQL.Append(" INSERT INTO PSI_MOLD_SCHEDULE(MOLD_CODE, PARTNUMBER, QUANTITY, PLAN_DATE)");
                                //sbSQL.Append(" VALUES('" + _moldCode + "', '" + _partNumber + "'," + _quantity.ToString() + ",'" + _plan_date + "')");

                            }

                            if (sbSQL.ToString() != "")
                            {
                                Int32 result = DBConnection.ExcuteCommandText(sbSQL.ToString());
                            }

                        }

                        //LOAD DATA
                        LoadData();
                        //RENDER HTML FOR DISPLAYING
                        RenderHTML();

                    }
                }
                else
                {



                }
            }
        }
    }
}