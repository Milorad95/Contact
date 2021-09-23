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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            activeForm();         /* metoda za prikazivanje glavne forme prilikom zatvaranja ove forme */
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;            /* sakrivanje button-a "Save" i zamrzavanje textbox-ova kako bi korisnik mogao da unese samo ID kontakta za koji treba da se azuriraju podaci */
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhoneNumber.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs ex)
        {
            activeForm();           /* metoda za prikazivanje glavne forme prilikom zatvaranja ove forme */
        }
        private void activeForm()   /* ova metoda prikazuje glavnu formu */
        {
            this.Hide();
            var form = new Form1();
            form.Show();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;         /* prilikom klik-a na button Find, korisniku postaju dostupni textbox-ovi i button Save za cuvanje i izmenu podataka o kontaktu */
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtEmail.Enabled = true;
            txtPhoneNumber.Enabled = true;

            var connectionString = "Data Source=DESKTOP-ELB063I\\SQLEXPRESS01;Initial Catalog=Contact;Integrated Security=True;";
            var con = new SqlConnection(connectionString);
            var ID = txtID.Text;        /* promenjiva koja cuva podatak o ID kontakta koju je korisnik uneso na formi */

            try
            {
                con.Open();
                var command = new SqlCommand($"SELECT * FROM Contact WHERE ContactId={ID}", con);       /* prosledjivanje upita bazi koji na osnovu ID kontakta vraca sve podatke o tom kontaktu */
                var reader = command.ExecuteReader();

                while (reader.Read())       /* ucitavanje podataka o kontaktu u textbox-ove */
                {
                    txtID.Text = reader["ContactId"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhoneNumber.Text = reader["MobilePhone"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var connectionString = "Data Source=DESKTOP-ELB063I\\SQLEXPRESS01;Initial Catalog=Contact;Integrated Security=True;";
            var con = new SqlConnection(connectionString);

            try
            {
                var command = new SqlCommand();
                command.Connection = con;
                con.Open();
                command.CommandText = $"Update Contact SET FirstName=@fn, LastName=@ln, Email=@em, MobilePhone=@ph WHERE ContactId={txtID.Text}";       /* azuriraj kontakt u bazi sa unesenim ID-jem na formi */
                command.Parameters.AddWithValue("@fn", txtFirstName.Text);      /* prosledjivanje svih parametara kontakta iz textbox-ova koji se nalaze na formi za azuriranje */
                command.Parameters.AddWithValue("@ln", txtLastName.Text);
                command.Parameters.AddWithValue("@em", txtEmail.Text);
                command.Parameters.AddWithValue("@ph", txtPhoneNumber.Text);
                command.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
                activeForm();       /* metoda koja prikazuje glavnu formu */
            }
        }
    }
}
