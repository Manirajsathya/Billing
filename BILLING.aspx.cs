using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication_BILLING
{

    public partial class BILLING : System.Web.UI.Page
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {

                    connection.Open();
                    string selectquery = "select * from [practice].[dbo].[List]";
                    SqlCommand cmd = new SqlCommand(selectquery, connection);
                    SqlDataReader Reader = cmd.ExecuteReader();
                    //IdVegDrop.DataSource = Reader;
                    //IdVegDrop.DataBind();
                    //IdVegDrop.DataTextField = "Vegetables";   here reader not required 
                    //IdVegDrop.DataValueField = "Vegetables";
                    //IdVegDrop.DataBind();
                    connection.Close();

                    SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    Adapter.Fill(Dt);
                    IdVegDrop.DataSource = Dt;
                    IdVegDrop.DataTextField = "Vegetables";
                    //IdVegDrop.DataValueField = "Id";      //without this also working properly 
                    IdVegDrop.DataBind();
                    connection.Close();
                }
                BindList();


            }
        }
        decimal TotalBill = 0;
        protected void IdAddButton_Click(object sender, EventArgs e)
        {
            string Vegetable = IdVegDrop.SelectedValue;
            //int Vegetable_Id = int.Parse(IdVegTextBox.Text);
            decimal User_Weight = decimal.Parse(IdWeightTextBox.Text);
            string formattedWeight = User_Weight.ToString("0.000");


            DataTable Table = new DataTable();
            Table.Columns.Add("Id", typeof(int));
            Table.Columns.Add("Vegtable", typeof(string));
            Table.Columns.Add("Weight", typeof(decimal));
            Table.Columns.Add("Price/Kg", typeof(decimal));
            Table.Columns.Add("Total", typeof(decimal));

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string selectquery = "select * from [practice].[dbo].[List] where Vegetables=@Vegetable";
                SqlCommand cmd = new SqlCommand(selectquery, connection);
                cmd.Parameters.AddWithValue("@Vegetable", Vegetable);
                SqlDataReader Reader = cmd.ExecuteReader();


                if (Reader.Read())
                {
                   
                    int Id = (int)Reader["Id"];
                    string vegetableName = Reader["Vegetables"].ToString();
                    decimal PricePerKg = (decimal)Reader["Price/Kg"];
                    decimal Total = PricePerKg * User_Weight;
                    TotalBill += Total;


                    Insert(Id, vegetableName, User_Weight, PricePerKg, Total); /*vegetableName*/

                    BindList2();
                }

            }
        
    
        }
        private void Insert(int Id, string vegetableName, decimal User_Weight, decimal PricePerUnit, decimal Total)

        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string InsertQuery = "insert into[practice].[dbo].[Bill](Id,Vegetable,Weight,[Price/Kg],Total)values(@Id,@vegetableName,@User_Weight,@pricePerUnit,@totalBill) ";
                SqlCommand cmd = new SqlCommand(InsertQuery, connection);
                //SqlDataReader Reader = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@vegetableName", vegetableName);
                cmd.Parameters.AddWithValue("@User_Weight", User_Weight);
                cmd.Parameters.AddWithValue("@pricePerUnit", PricePerUnit);
                cmd.Parameters.AddWithValue("@totalBill", Total);
                cmd.ExecuteNonQuery();


            }



        }


        protected void IdEndButton_Click(object sender, EventArgs e)
        {

        }

        private void BindList()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string selectquery = "select * from [practice].[dbo].[List]";
                SqlCommand cmd = new SqlCommand(selectquery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();


            }

        }

        private void BindList2()
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string selectquery = "select * from [practice].[dbo].[Bill]";
                SqlCommand cmd = new SqlCommand(selectquery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                GridView2.DataSource = ds;
                decimal totalSalary = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)   
                {
                    totalSalary += Convert.ToDecimal(dr["Total"]);
                }
                GridView2.Columns[5].FooterText = totalSalary.ToString();
                GridView2.DataBind();



            }

        }



        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            {
                TextBox IdTextBox = (TextBox)GridView2.Rows[e.RowIndex].FindControl("IdTextBox");
                TextBox VegText = (TextBox)GridView2.Rows[e.RowIndex].FindControl("VegText");
                TextBox WeightTextBox = (TextBox)GridView2.Rows[e.RowIndex].FindControl("WeightTextBox");                
                //TextBox TotalTextBox = (TextBox)GridView2.Rows[e.RowIndex].FindControl("TotalTextBox");
                int Id = int.Parse(IdTextBox.Text);
                string Vegetable = VegText.Text;
                decimal User_Weight = decimal.Parse(WeightTextBox.Text);
                //decimal Total = decimal.Parse(TotalTextBox.Text);
                decimal UpdateTotal = 0;
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string selectquery = "select * from [practice].[dbo].[List] where Vegetables=@Vegetable";
                    SqlCommand cmd = new SqlCommand(selectquery, connection);
                    cmd.Parameters.AddWithValue("@Vegetable", Vegetable);
                    SqlDataReader Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        //int Id = (int)Reader["Id"];
                        string vegetableName = Reader["Vegetables"].ToString();
                        decimal PricePerKg = (decimal)Reader["Price/Kg"];
                         UpdateTotal = PricePerKg * User_Weight;
                        //UpdateTotal += Total;
                        
                    }
                }



                        using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string UpdateQuery = "update [practice].[dbo].[Bill] set Vegetable=@Vegetable, Weight=@Weight,Total=@totalBill where Id=@Id ";
                    SqlCommand cmd = new SqlCommand(UpdateQuery, connection);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Vegetable", Vegetable);
                    cmd.Parameters.AddWithValue("@Weight", User_Weight);                   
                    cmd.Parameters.AddWithValue("@totalBill", UpdateTotal);
                    cmd.ExecuteNonQuery();
                    BindList2();
                }
            }
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            BindList2();
        }

        

        protected void GridView2_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            TextBox Textid = (TextBox)GridView2.Rows[e.RowIndex].FindControl("IdTextBox");
            int Id = int.Parse(Textid.Text);
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string DeleteQuery = " Delete from [practice].[dbo].[Bill] where Id=@Id";
                SqlCommand cmddltquery = new SqlCommand(DeleteQuery, connection);
                cmddltquery.Parameters.AddWithValue("@Id", Id);
                cmddltquery.ExecuteNonQuery();
            }
        }

        protected void Idclear_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string TruncateQuery = "Truncate table [practice].[dbo].[Bill]";
                SqlCommand cmddltquery = new SqlCommand(TruncateQuery, connection);                
                cmddltquery.ExecuteNonQuery();
            }



        }
    }
    }

