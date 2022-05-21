using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalSystem
{
    public partial class sys : Form
    {
        public sys()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cars_Click(object sender, EventArgs e)
        {
            Cars c = new Cars();
            c.Show();
            this.Close();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I hope you had fun. Thanks For Using");
            this.Close();
            Login l = new Login();
            l.Show();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            Users u = new Users();
            u.Show();
            this.Close();
        }

        private void Customers_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }

        private void Rental_Click(object sender, EventArgs e)
        {
            Rental r = new Rental();
            r.Show();
            this.Close();
        }
    }
}
