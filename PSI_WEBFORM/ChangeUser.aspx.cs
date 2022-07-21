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
    public partial class ChangeUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            this.lbMessage.Text = "";
            String _user_code = this.txtUser.Text.Trim();
            String _user_pwd = this.txtPassword.Text.Trim();
            String _new_pwd = this.txtNewPassword.Text.Trim();
            String _conf_pwd = this.txtconfirmPass.Text.Trim();
            if (_user_code == "" || _user_pwd == "")
            {
                this.lbMessage.Text = "INPUT USER & PASSWORD";
                return;
            }

            DataTable theTable = DataProvider.GetUser(_user_code, _user_pwd);

            if (theTable.Rows.Count == 0)
            {
                //this.lbMessage.Text = "Your Pass is not correct. Please, check again";
                WebMsgBox.Show("Your Pass is not correct. Please, check again");
            }
            else
            {
                if(_new_pwd == "" || _conf_pwd =="" )
                {
                    WebMsgBox.Show("New password or confirm passwrd is empty");
                }else
                {
                    if(_new_pwd == _conf_pwd)
                    {
                        StringBuilder sbSQL = new StringBuilder();
                        
                        sbSQL.Append("UPDATE PSI_USER SET ");
                        sbSQL.Append(" USER_PWD = '" + _new_pwd + "'");
                        sbSQL.Append(" WHERE USER_CODE = '" + _user_code + "'");
                        Int32 iResult1 = DBConnection.ExcuteCommandText(sbSQL.ToString());
                        WebMsgBox.Show("Change password complete!!");
                    }
                    else
                    {
                    //this.lbMessage.Text = "New password and confirm password is different. Please, check again";
                    WebMsgBox.Show("New password and confirm password is different. Please, check again");
                    }
                }
                
            }
        }
    }
}