using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PSI_WEBFORM
{
    public class DataProvider
    {


        public static DataTable GetProdid(String _child_mtrlid, String _time)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select distinct P.MFG_ORDER,P.PRODID,P.DILY_PRDTN_QTY as PROD_PLAN,B.CHILD_MTRLID, B.COMPONENT_QUANTITY ");
            sbSQL.Append("from TB_POM_BOM_BAS B, TB_POM_dily_prdtn_pln_bas P ");
            sbSQL.Append("where ");
            sbSQL.Append("P.PRODID = B.PRODID ");
            sbSQL.Append("and B.CHILD_MTRLID = '"+ _child_mtrlid +"' ");
            sbSQL.Append("and P.PRDTN_YMD = '"+ _time +"' ");
            sbSQL.Append("and P.dept_code_name = 'FA' ");
            DataTable theTable1 = new DataTable();
            theTable1 = DBConnection.GetDataTableByCommandTextGMES(sbSQL.ToString());
            return theTable1;
        }


        public static DataTable GetProdid1(String _child_mtrlid)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select * from PSI_PLANPRODUCTION WHERE PARTNUMBER = '" + _child_mtrlid + "' ");
            
            DataTable theTable1 = new DataTable();
            theTable1 = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable1;
        }
        public static DataTable GetPart()
        {

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select DISTINCT tool_msg FROM PSI_PART WHERE TOOL_MSG is not null ORDER BY TOOL_MSG DESC ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }

        public static DataTable GetVender()
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select DISTINCT VENDER FROM PSI_MOLD WHERE VENDER is not null ORDER BY VENDER DESC");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }
        public static DataTable GetPartInTool(String _tool_msg, String _Name)
        {

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select * FROM PSI_PART WHERE TOOL_MSG = '" + _tool_msg + "' AND PARTNAME = '" + _Name + "' ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }


        public static DataTable GetPartPartNumber()
        {

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select DISTINCT PARTNUMBER from PSI_PART ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }

        public static DataTable GetPartTool1(String _tool_msg)
        {

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select * FROM PSI_PART WHERE TOOL_MSG = '" + _tool_msg + "' ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }

        public static DataTable GetPSIPartNumberByPart(String _partNumber)
        {

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select * FROM PSI_PART WHERE PARTNUMBER = '" + _partNumber + "' ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }
        public static DataTable GetMold89(String ORG ,String vender)
        {

            StringBuilder sbSQL = new StringBuilder();
            if (vender != "LGE")
            {
                sbSQL.Append("select * from PSI_MOLD WHERE ORG_ID = '" + ORG + "' AND VENDER = '" + vender + "' ");
            }
            else
            {
                sbSQL.Append("select * from PSI_MOLD WHERE ORG_ID = '" + ORG + "' ");
            }
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }
         /*
        public static DataTable GetDaylyPlan(String _time, String _prodid)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select p.dily_prdtn_qty, p.* from tb_pom_dily_prdtn_pln_bas p where PRDTN_YMD = '" + _time + "' ");
            sbSQL.Append("and prodid in ('"+ _prodid +"') ");
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandTextGMES(sbSQL.ToString());
            return theTable;
        }
          */
        public static DataTable GetProductionPlan(String _org_id, String _start, String _end)
        {
            StringBuilder sbSQL = new StringBuilder();
            
            sbSQL.Append("select PRODID PART_NO,SUM(DILY_PRDTN_QTY) QUANTITY ");
            sbSQL.Append("from tb_pom_dily_prdtn_pln_bas ");
            sbSQL.Append("where dept_code_name = 'FA' AND org_id  = '" + _org_id + "'   and  to_char(Prdtn_strt_date,'YYYYMMDD') = '" + _start + "' ");
            //sbSQL.Append("where dept_code_name = 'FA' AND org_id  = '" + _org_id + "' and PRDTN_YMD ='" + _start + "'  ");
            sbSQL.Append("group by PRODID ");



           // sbSQL.Append("select * from tb_pom_dily_prdtn_pln_bas where sffx_name is not null and Prdtn_strt_date >= '" + _start + "' and Prdtn_end_date <= '" + _end + "' and org_id = '" + _org_id + "'");
            /*
            sbSQL.Append("select PART_NO,SUM(TOTAL_QUANTITY) QUANTITY ");
            sbSQL.Append("from");
            sbSQL.Append(" ( ");
            sbSQL.Append(" select PART_NO, DEMAND_ID,TOTAL_QUANTITY, PRODUCTION_DATE, row_number() over ( partition by DEMAND_ID order by LAST_UPDATE_DATE desc) rn ");
            sbSQL.Append("  from IF_GERP_R_XXFPM_MES_PP ");
            sbSQL.Append("  where ORGANIZATION_ID = '" + _org_id + "' and SCHEDULE_FIX_FLAG = 'Y' and SCHEDULE_GROUP_NAME = 'FA' and USE_FLAG = 'Y' ");
            sbSQL.Append("  and  PRODUCTION_DATE >= '" + _start + "' and PRODUCTION_DATE <= '" + _end + "'");
            sbSQL.Append(" ) ");
            sbSQL.Append(" where rn = 1  group by PART_NO");
         */   
            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandTextGMES(sbSQL.ToString());
            return theTable;
        }
        
        public static float CaculateScaleByPartNumber(String _partNumber, String _prodid, Int32 _level)
        {
            DataTable theTable = GetPartPATH(_partNumber, _prodid, _level);

            if (theTable.Rows.Count > 0)
            {
                Int32 amountOfItem = theTable.Rows.Count;

                float scale = 1;

                for (int i = 0; i < amountOfItem; ++i)
                {
                    scale = scale * float.Parse(theTable.Rows[i]["COMPONENT_QUANTITY"].ToString());
                }

                return scale;
            }
            else
            {
                return 0;
            }
        }
        public static DataTable GetPartPATH(String _partNumber, String _prodid, Int32 _level)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT  PRODID, CHILD_MTRLID, PARENT_MTRLID, PLAN_LEVEL, COMPONENT_QUANTITY ");
            sbSQL.Append("FROM tb_pom_bom_bas ");
            sbSQL.Append("WHERE ");
            sbSQL.Append("PRODID = '" + _prodid + "' ");
            sbSQL.Append("AND  ROWNUM <= " + _level + " ");
            sbSQL.Append("START WITH CHILD_MTRLID = '" + _partNumber + "' ");
            sbSQL.Append("CONNECT BY PRIOR PARENT_MTRLID = CHILD_MTRLID");

            DataTable theTable = new DataTable();
            theTable = DBConnection.GetDataTableByCommandText(sbSQL.ToString());
            return theTable;
        }
        public static DataTable GetToolManagementByOrgID(String _org_id, String _toolinfo, String _vender)
        {
            DataTable theTable = new DataTable();
            StringBuilder sbWHERE = new StringBuilder("WHERE P.PARTNUMBER = PM.PARTNUMBER AND PM.MOLD_CODE = M.MOLD_CODE AND P.ACTIVE = 1 ");
            if (_vender != "LGE")
            {
                sbWHERE.Append(" AND M.VENDER = '" + _vender + "' ");
            }
            
            if (_org_id != "")
            {
                sbWHERE.Append(" AND P.ORG_ID = '" + _org_id + "' ");
            }

            if (_toolinfo != "" && _toolinfo != "ALL")
            {
                sbWHERE.Append(" AND P.TOOL_MSG = '" + _toolinfo + "' ");
            }

            sbWHERE.Append(" ORDER BY P.TOOL_MSG ");

            theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT P.PARTNAME, P.TOOL_MSG, P.ORG_ID, M.VENDER FROM PSI_PART P, PSI_PART_MOLD PM, PSI_MOLD M " + sbWHERE.ToString());

            return theTable;
        }
        public static DataTable GetPartMana2(String vender)
        {
            DataTable theTable = new DataTable();
            StringBuilder sbWHERE = new StringBuilder();
            if (vender != "LGE")
            {
                sbWHERE.Append(" FROM PSI_PART P, PSI_PART_MOLD PM, PSI_MOLD M ");
            }
            else
            {
                sbWHERE.Append(" FROM PSI_PART P ");
            };

            sbWHERE.Append("WHERE P.ACTIVE = 1 ");
            
            if (vender != "LGE")
            {
                sbWHERE.Append(" AND P.VENDER = '" + vender + "' OR (P.PARTNUMBER = PM.PARTNUMBER AND PM.MOLD_CODE = M.MOLD_CODE AND M.VENDER ='" + vender + "')  ");
            }

            sbWHERE.Append(" ORDER BY P.PARTNUMBER ");

            theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT P.PARTNUMBER, P.PARTNAME, P.TOOL_MSG,P.STOCK_QTY, P.PLAN_LEVEL, P.ORG_ID, P.ACTIVE, P.STOCK_DATE, P.VENDER  " + sbWHERE.ToString());

            return theTable;
        }
        public static DataTable GetPartManagementByOrgID2(String _org_id, String _partNumber, String _toolinfo, String vender)
        {
            DataTable theTable = new DataTable();
            StringBuilder sbWHERE = new StringBuilder();
            if (vender != "LGE")
            {
                sbWHERE.Append(" , M.VENDER FROM PSI_PART P, PSI_PART_MOLD PM, PSI_MOLD  M ");
            }
            else
            {
                sbWHERE.Append(" FROM PSI_PART P ");
            };

            sbWHERE.Append("WHERE P.ACTIVE = 1 ");
            if (_org_id != "")
            {
                sbWHERE.Append(" AND P.ORG_ID = '" + _org_id + "' ");
            }

            if (vender != "LGE")
            {
                sbWHERE.Append(" AND P.PARTNUMBER = PM.PARTNUMBER AND PM.MOLD_CODE = M.MOLD_CODE AND M.VENDER ='"+ vender +"' ");
            }

            if (_toolinfo != "" && _toolinfo != "ALL")
            {
                sbWHERE.Append(" AND P.TOOL_MSG = '" + _toolinfo + "' ");
            }


            if (_partNumber != "")
            {
                sbWHERE.Append(" AND P.PARTNUMBER LIKE '%" + _partNumber + "%' ");
            }

            sbWHERE.Append(" ORDER BY PARTNAME ");

            theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT P.PARTNUMBER, P.PARTNAME, P.TOOL_MSG,P.STOCK_QTY, P.PLAN_LEVEL, P.ORG_ID, P.ACTIVE, P.STOCK_DATE, P.VENDER  " + sbWHERE.ToString());

            return theTable;
        }



        public static DataTable GetPartManagementByOrgID(String _org_id, String _partNumber, String _toolinfo, String vender)
        {
            DataTable theTable = new DataTable();
            StringBuilder sbWHERE = new StringBuilder("WHERE P.PARTNUMBER = PM.PARTNUMBER AND PM.MOLD_CODE = M.MOLD_CODE AND P.ACTIVE = 1 ");

            if (_org_id != "")
            {
                sbWHERE.Append(" AND P.ORG_ID = '" + _org_id + "' ");
            }

            if (vender != "LGE")
            {
                sbWHERE.Append(" AND M.VENDER = '" + vender + "' ");
            }

            if (_toolinfo != "" && _toolinfo != "ALL")
            {
                sbWHERE.Append(" AND P.TOOL_MSG = '" + _toolinfo + "' ");
            }

            
            if (_partNumber != "")
            {
                sbWHERE.Append(" AND P.PARTNUMBER LIKE '%" + _partNumber + "%' ");
            }

            sbWHERE.Append(" ORDER BY PARTNAME ");

            theTable = DBConnection.GetDataTableByCommandText("SELECT DISTINCT P.PARTNUMBER, P.PARTNAME, P.TOOL_MSG,P.STOCK_QTY, P.PLAN_LEVEL, P.ORG_ID, P.ACTIVE, P.STOCK_DATE, M.VENDER FROM PSI_PART P, PSI_PART_MOLD PM, PSI_MOLD M " + sbWHERE.ToString());

            return theTable;
        }

        public static DataTable GetBOMByModelPart(String _model, String _partNumber)
        {
            StringBuilder sbWHERE = new StringBuilder(" WHERE ITEM_STATUS_CODE = 'Active' ");

            if (_model != "")
            {
                sbWHERE.Append(" AND PRODID = '" + _model + "'");
            }


            if (_partNumber != "")
            {
                sbWHERE.Append(" AND CHILD_MTRLID = '" + _partNumber + "'");

            }

            DataTable theTable = DBConnection.GetDataTableByCommandTextGMES("SELECT * FROM TB_POM_BOM_BAS " + sbWHERE.ToString());
            return theTable;
        }



        public static DataTable GetDataPartMoldByParNumber(String _partnumber)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT P.PARTNUMBER , P.PARTNAME, P.PLAN_LEVEL, M.MOLD_CODE, M.MOLD_NAME, M.CAPACITY,M.MACHINE, PM.RATE, PM.ACTIVE ");
            //sb.Append("FROM PSI_PART_MOLD PM INNER JOIN PSI_PART P ON PM.PARTNUMBER = P.PARTNUMBER ");
            //sb.Append("INNER JOIN PSI_MOLD M ON PM.MOLD_CODE = M.MOLD_CODE ");
            //sb.Append("WHERE P.PARTNUMBER = '" + _partNumber + "'  ORDER BY P.PARTNAME ");


            sb.Append("SELECT DISTINCT PM.MOLD_CODE, M.MOLD_NAME, M.MACHINE, M.CAPACITY ");
            sb.Append("FROM PSI_PART_MOLD PM, PSI_PART P, PSI_MOLD M ");
            sb.Append("WHERE PM.MOLD_CODE = M.MOLD_CODE and PM.PARTNUMBER = P.PARTNUMBER ");
            sb.Append("AND P.PARTNUMBER IN( SELECT PARTNUMBER FROM PSI_PART WHERE TOOL_MSG = '" + _partnumber + "' ) ");
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }


        public static DataTable GetDataPartMoldByParNumber1(String _partnumber)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT P.PARTNUMBER , P.PARTNAME, P.PLAN_LEVEL, M.MOLD_CODE, M.MOLD_NAME, M.CAPACITY,M.MACHINE, PM.RATE, PM.ACTIVE ");
            //sb.Append("FROM PSI_PART_MOLD PM INNER JOIN PSI_PART P ON PM.PARTNUMBER = P.PARTNUMBER ");
            //sb.Append("INNER JOIN PSI_MOLD M ON PM.MOLD_CODE = M.MOLD_CODE ");
            //sb.Append("WHERE P.PARTNUMBER = '" + _partNumber + "'  ORDER BY P.PARTNAME ");


            sb.Append("SELECT DISTINCT P.PARTNUMBER, PM.MOLD_CODE, M.MOLD_NAME, M.MACHINE, M.CAPACITY ");
            sb.Append("FROM PSI_PART_MOLD PM, PSI_PART P, PSI_MOLD M ");
            sb.Append("WHERE PM.MOLD_CODE = M.MOLD_CODE and PM.PARTNUMBER = P.PARTNUMBER ");
            sb.Append("AND P.PARTNUMBER IN( SELECT PARTNUMBER FROM PSI_PART WHERE PARTNUMBER = '" + _partnumber + "' ) ");
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }

        public static DataTable GetNumberName(String _partnumber)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT P.PARTNUMBER , P.PARTNAME, P.PLAN_LEVEL, M.MOLD_CODE, M.MOLD_NAME, M.CAPACITY,M.MACHINE, PM.RATE, PM.ACTIVE ");
            //sb.Append("FROM PSI_PART_MOLD PM INNER JOIN PSI_PART P ON PM.PARTNUMBER = P.PARTNUMBER ");
            //sb.Append("INNER JOIN PSI_MOLD M ON PM.MOLD_CODE = M.MOLD_CODE ");
            //sb.Append("WHERE P.PARTNUMBER = '" + _partNumber + "'  ORDER BY P.PARTNAME ");


            sb.Append("SELECT DISTINCT P.PARTNUMBER, PM.MOLD_CODE, M.MOLD_NAME, M.MACHINE, M.CAPACITY, M.VENDER ");
            sb.Append("FROM PSI_PART_MOLD PM, PSI_PART P, PSI_MOLD M ");
            sb.Append("WHERE PM.MOLD_CODE = M.MOLD_CODE and PM.PARTNUMBER = P.PARTNUMBER ");
            sb.Append("AND P.PARTNUMBER IN( SELECT PARTNUMBER FROM PSI_PART WHERE PARTNUMBER = '" + _partnumber + "' ) ");
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }

        public static DataTable GetUser(String user)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM PSI_USER WHERE USER_CODE = '" + user + "' ");
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }
        public static DataTable GetMoldListByModeCode(string _mode_code, string _org_id, String _vender)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWHERE = new StringBuilder("  WHERE ACTIVE = 1");
            DataTable theTable = new DataTable();

            if (_mode_code != "")
            {
                sbWHERE.Append(" AND MOLD_CODE = '" + _mode_code + "'");
            }

            if (_vender != "LGE")
            {
                sbWHERE.Append(" AND VENDER = '" + _vender + "' ");
            }
            if (_org_id != "")
            {
                sbWHERE.Append(" AND ORG_ID = '" + _org_id + "'");
            }

            sbWHERE.Append(" ORDER BY MOLD_CODE ");

            theTable = DBConnection.GetDataTableByCommandText(" SELECT * FROM PSI_MOLD " + sbWHERE.ToString());

            return theTable;
        }

        public static Int32 DeleteMoldByMoldCode(string _mode_code)
        {
            Int32 iResult = DBConnection.ExcuteCommandText("DELETE PSI_MOLD WHERE MOLD_CODE = '" + _mode_code + "'");
            return iResult;
        }

        public static Int32 DeletePartByPartNumber(string _partnumber)
        {
            Int32 iResult = DBConnection.ExcuteCommandText("DELETE PSI_PART WHERE PARTNUMBER = '" + _partnumber + "'");
            return iResult;
        }


        public static Int32 UpdateStockQtyByPartNumber(String _partNumber, String _tool_msg, String _quantity, String _cur_date)
        {
            String strSQL = "UPDATE PSI_PART SET STOCK_QTY = " + _quantity + " , STOCK_DATE = '" + _cur_date + "', TOOL_MSG = '" + _tool_msg + "' WHERE PARTNUMBER = '" + _partNumber + "'";

            Int32 iResult = DBConnection.ExcuteCommandText(strSQL);
            return iResult;
        }

        public static DateTime GetCurrentDateTime()
        {
            DataTable dt = DBConnection.GetDataTableByCommandText(@"select sysdate d from dual");
            return (Convert.ToDateTime(dt.Rows[0]["d"]));
        }

        public static DataTable GetDataPartMoldByMoldCode(String _moldCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_PART_MOLD ");

            if (_moldCode != "")
            {
                sb.Append("WHERE MOLD_CODE = '" + _moldCode + "'");
            }

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }

        public static DataTable GetDataMoldScheduleByModeCode(String _moldCode, String _date_list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_MOLD_SCHEDULE ");
            sb.Append("WHERE MOLD_CODE = '" + _moldCode + "' AND PLAN_DATE IN " + _date_list + " ORDER BY MOLD_CODE");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }

        public static DataTable GetDataMoldScheduleByDate(String _date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_MOLD_SCHEDULE ");
            sb.Append("WHERE PLAN_DATE = '" + _date + "'");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }



        public static DataTable GetDataMoldScheduleFromCurrentDate(String _date, String _toolinfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_MOLD_SCHEDULE ");
            sb.Append("WHERE PLAN_DATE >= '" + _date + "' ");
            //if (_toolinfo != "" && _toolinfo != "ALL")
            //{
            //    sb.Append(" AND TOOL_MSG = '" + _toolinfo + "' ");
            //}
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }


        public static DataTable Getcapa(String _date, String _moldcode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_MOLD_SCHEDULE ");
            sb.Append("WHERE PLAN_DATE = '" + _date + "' ");
            sb.Append(" AND MOLD_CODE = '" + _moldcode + "' ");
            
            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }
        public static DataTable GetUser(String _user_code, String _password)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_USER ");
            sb.Append("WHERE USER_CODE = '" + _user_code + "' AND USER_PWD = '" + _password + "' AND ACTIVE = 1 ");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString()); 
            return theTable;
        }

        public static DataTable GetDataPSIPart(String _partnumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_PART ");
            sb.Append("WHERE PLAN_DATE >= '" + _partnumber + "'");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }

        public static DataTable GetMOLDCapacity(String _moldcode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append("FROM PSI_MOLD ");
            sb.Append("WHERE MOLD_CODE = '" + _moldcode + "'");

            DataTable theTable = DBConnection.GetDataTableByCommandText(sb.ToString());
            return theTable;
        }
    }
}