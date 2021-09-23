using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            activeForm();      /* prikazi glavnu formu prilikom klik na button Nazad */

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            activeForm();      /* prikazi glavnu formu prilikom zatvaranja ove forme */
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtFirstName.TextLength == 0 || txtLastName.TextLength == 0 || txtEmail.TextLength == 0 || txtPhoneNumber.TextLength == 0)   /* ukoliko je bilo koji textbox predvidjen za unos novog kontakta prazan */
            {
                MessageBox.Show("Morate uneti podatke o kontaktu.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else    /* ako su svi podaci uneseni u textbox-ovima upisi ih u bazu */
            {
                var connectionString = "Data Source=DESKTOP-ELB063I\\SQLEXPRESS01;Initial Catalog=Contact;Integrated Security=True;";
                var con = new SqlConnection(connectionString);

                try
                {
                    var command = new SqlCommand();
                    command.Connection = con;
                    con.Open();
                    command.CommandText = @"INSERT INTO Contact (FirstName, LastName, Email, MobilePhone) VALUES (@fn, @ln, @em, @ph)";     /* prosledjivanje parametara novog kontakta i cuvanje u bazi */
                    command.Parameters.AddWithValue("@fn", txtFirstName.Text);
                    command.Parameters.AddWithValue("@ln", txtLastName.Text);
                    command.Parameters.AddWithValue("@em", txtEmail.Text);
                    command.Parameters.AddWithValue("@ph", txtPhoneNumber.Text);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                    activeForm();           /* prikazi glavnu formu koja ce prikazati i novi kontakt zajedno sa vec postojecim u bazi */
                }
            }
        }
        public void activeForm()
        {
            this.Hide();
            var form = new Form1();
            form.Show();
        }
    }
}
