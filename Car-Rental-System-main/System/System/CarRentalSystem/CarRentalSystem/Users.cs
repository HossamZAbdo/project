using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarRentalSystem
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
            sys s = new sys();
            s.Show();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\HOSSAM\DESKTOP\CAR-RENTAL-SYSTEM-MAIN\SYSTEM\SYSTEM\CARRENTALSYSTEM\CARRENTALSYSTEM\CARRENTALDB.MDF;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            con.Open();
            string Query = "select * from userTbl";
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            SqlCommandBuilder buldier = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (textBoxid.Text == "" || textBoxname.Text == "" || textBoxpass.Text == "")
            {
                MessageBox.Show("Check Entered Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "insert into userTbl values(" + textBoxid.Text + ",'" + textBoxname.Text + "','" + textBoxpass.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User is successfully added to system DB");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                    con.Close();
                }
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (textBoxid.Text == "")
            {
                MessageBox.Show("Check Entered Information");
                textBoxid.Focus();
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "delete from userTbl where Id =" + textBoxid.Text + ";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User is successfully deleted from system DB");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                    con.Close();
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxid.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            textBoxname.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxpass.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (textBoxid.Text == "" || textBoxname.Text == "" || textBoxpass.Text == "")
            {
                MessageBox.Show("Check Entered Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "update userTbl set Username ='" + textBoxname.Text + "',Password ='" + textBoxpass.Text + "'where Id = " + textBoxid.Text + ";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User is successfully updated in system DB");
                    con.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                    con.Close();
                }
            }
        }
    }
}