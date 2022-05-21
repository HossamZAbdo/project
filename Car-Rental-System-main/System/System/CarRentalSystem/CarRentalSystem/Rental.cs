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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustCombo();
            populate();
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

        private void fillcombo()
        {
            con.Open();
            string Query = "select RegNum from CarTbl where Available ='" + "YES" + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader read;
            read = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(read);
            comboBox1.ValueMember = "RegNum";
            comboBox1.DataSource = dt;
            con.Close();
        }

        private void fillCustCombo()
        {
            con.Open();
            string Query = "select CustId from CustomerTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader read;
            read = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(read);
            CustCombo.ValueMember = "CustId";
            CustCombo.DataSource = dt;
            con.Close();
        }

        private void fetchCustName()
        {
            con.Open();
            string Query = "select * from CustomerTbl where CustId =" + CustCombo.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                tbName.Text = dr["CustName"].ToString();
            }
            con.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void populate()
        {
            con.Open();
            string Query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            SqlCommandBuilder buldier = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "" || tbName.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Check Entered Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "insert into RentalTbl values(" + tbId.Text + ",'" + comboBox1.SelectedValue.ToString() + "','"
                        + tbName.Text + "','" + dateRent.Value.Date + "','" + dateReturn.Value.Date + "','" + tbFees.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rent is successfully added to system DB");
                    con.Close();
                    updateAvailability();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                    con.Close();
                }
            }
        }

        private void updateAvailability()
        {
            con.Open();
            string Query = "update CarTbl set Available ='" + "NO" + "'where RegNum = " + comboBox1.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void updateAvailabilityAfterDeletion()
        {
            con.Open();
            string Query = "update CarTbl set Available ='" + "YES" + "'where RegNum = " + comboBox1.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "")
            {
                MessageBox.Show("Check Entered Information");
                tbId.Focus();
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "delete from RentalTbl where RentId =" + tbId.Text + ";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rent is successfully deleted from system DB");
                    con.Close();
                    populate();
                    updateAvailabilityAfterDeletion();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                    con.Close();
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbId.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
            comboBox1.SelectedValue = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            tbName.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
            tbFees.Text = RentDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "" || comboBox1.SelectedValue.ToString() == "" || tbName.Text == "" || tbFees.Text == "")
            {
                MessageBox.Show("Check Entered Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "update RentalTbl set CarReg ='" + comboBox1.SelectedValue.ToString() + "',RentDate ='" + dateRent.Value.Date + 
                        "',ReturnDate ='" + dateReturn.Value.Date + "'where RentId = " + tbId.Text + ";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rent is successfully updated in system DB");
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
