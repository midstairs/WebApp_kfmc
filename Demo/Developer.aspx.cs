using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDevs.App_Code;

namespace WebDevs.Demo
{
    public partial class Developer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateGvDeveloper();
            if (!Page.IsPostBack) //run this code only once per user 
            {
                populateddlCourses();

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void populateGvDeveloper()
        {
            CRUD myCRUD = new CRUD();
            string mySql = @"select * from v_developerInfo";
           SqlDataReader dr= myCRUD.getDrPassSql(mySql);
            gvDeveloper.DataSource = dr;
            gvDeveloper.DataBind();
}
        protected void populateddlCourses()
        {
            CRUD myCRUD = new CRUD();
            string mySql = @"select coursesId, courseName from courses ";
            SqlDataReader dr = myCRUD.getDrPassSql(mySql);
            ddlCourses.DataTextField = "courseName";
            ddlCourses.DataValueField = "coursesId";
            ddlCourses.DataSource = dr;
            ddlCourses.DataBind();
        }

        protected void btnInser_Click(object sender, EventArgs e)
        {
            CRUD myCRUD = new CRUD();
            string mySql = @"insert developer (FName,LName,Email,CoursesId)
                             values (@FName,@LName,@Email,@CoursesId)";
            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@FName", txtFName.Text },
                { "@LName", txtLName.Text },
                { "@Email", txtEmail.Text },
                { "CoursesId", ddlCourses.SelectedItem.Value }
            };
            int rtn = myCRUD.InsertUpdateDelete(mySql, myPara);
            if (rtn>=1)
            {
                lblOutput.Text = "Successful Operation!";
          
            }
            else
            {
                lblOutput.Text = "Failed Operation";

            }
            populateGvDeveloper();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CRUD myCRUD = new CRUD();
            string mySql = @"update developer set FName=@FName,LName=@LName,Email=@Email,CoursesId=@CoursesId
                             where DeveloperId=@DeveloperId";
            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@DeveloperId", int.Parse(txtDeveloperId.Text )},
                { "@FName", txtFName.Text },
                { "@LName", txtLName.Text },
                { "@Email", txtEmail.Text },
                { "CoursesId", ddlCourses.SelectedItem.Value }
                
            };
            int rtn = myCRUD.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "Successful Operation!";

            }
            else
            {
                lblOutput.Text = "Failed Operation";

            }
            populateGvDeveloper();


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CRUD myCRUD = new CRUD();
            string mySql = @"delete developer 
                             where DeveloperId=@DeveloperId";
            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@DeveloperId", int.Parse(txtDeveloperId.Text) }
            };
            int rtn = myCRUD.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            {
                lblOutput.Text = "Successful Operation!";

            }
            else
            {
                lblOutput.Text = "Failed Operation";

            }
            populateGvDeveloper();

        }
        protected void gvDeveloper_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void populateForm_Click(object sender, EventArgs e)
        {
            int PK = int.Parse((sender as LinkButton).CommandArgument);
            //lblOuput.Text = PK.ToString();

            string mySql = @"select DeveloperId,FName,LName,Email,CoursesId
                           from developer
                           where DeveloperId=@DeveloperId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@DeveloperId", PK);
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        String DeveloperId = dr["DeveloperId"].ToString();
                        String FName = dr["FName"].ToString();
                        String LName = dr["LName"].ToString();
                        String Email = dr["Email"].ToString();
                        String CoursesId = dr["CoursesId"].ToString();

                        //lblOuput.Text = empId + employee+ depId;
                        txtDeveloperId.Text = DeveloperId;
                        txtFName.Text = FName;
                        txtLName.Text = LName;
                        txtEmail.Text = Email;
                        ddlCourses.SelectedValue = CoursesId;
                    }
                }
            }
        }

        public static void ExportGridToExcel(GridView myGv) // working 1
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "";
            string FileName = "ExportedReport_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            myGv.GridLines = GridLines.Both;
            myGv.HeaderStyle.Font.Bold = true;
            myGv.RenderControl(htmltextwrtter);
            HttpContext.Current.Response.Write(strwritter.ToString());
            HttpContext.Current.Response.End();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvDeveloper);

        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            mailMgr mymailMgr = new mailMgr();
         String msg=  mymailMgr.sendEmailViaGmail2("webdevs743@gmail.com", "latevek497@grassdev.com", "my message is ....");
            lblOutput.Text = msg;                                    
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDeveloperId.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtEmail.Text = "";
            ddlCourses.SelectedValue = "1";


        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}