using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.ComponentModel;
using System.Data;
using System.Configuration;

namespace Contacts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var sqlCommand = "SELECT * FROM Contact";
            uploadToListView(sqlCommand, listView1);        /* prilikom pokretanja forme prosledi komandu i listu koja je na ovoj formi */
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3();        /* prikazi formu na kojoj se unose podaci o kontaktu */
            form3.Show();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Form2();         /* prikazi formu preko koje se azuriraju podaci o kontaktu */
            form.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();       /* ugasi program */
        }

        private void btnSearch_Click(object sender, EventArgs e)        /* metoda koja pretrazuje kontakte u bazi po imenu ili prezimenu */
        {
            listView2.Items.Clear();
            var sqlComannd = $"SELECT * FROM Contact WHERE FirstName='{txtSearch.Text}' OR LastName='{txtSearch.Text}'";
            uploadToListView(sqlComannd, listView2);            /* metoda koja upisuje podatke o kontaktu u drugi listview */
        }
        private void uploadToListView(string sqlComannd, ListView list)     /* metoda za upisivanje podataka u listview u zavisnosti koji listview je prosledjen metodi i koja komanda treba da se izvrsi */
        {
            var connectionString = "Data Source=DESKTOP-ELB063I\\SQLEXPRESS01;Initial Catalog=Contact;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connectionString);
            var contacts = new List<string>();          /* lista u koju se upisuju podaci o kontaktima */

            try
            {
                con.Open();
                var command = new SqlCommand(sqlComannd, con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contacts.Add(reader["ContactId"].ToString());
                    contacts.Add(reader["FirstName"].ToString());
                    contacts.Add(reader["LastName"].ToString());
                    contacts.Add(reader["Email"].ToString());
                    contacts.Add(reader["MobilePhone"].ToString());
                }
                reader.Close();

                string[] data = new string[5];      /* niz koji trenutno cuva i prosledjuje podatke u listview o samo jednom kontaktu */

                for (int i = 0; i < contacts.Count - 4; i++)        /* petlja koja upisuje jedan kontakt u niz */
                {
                    if (i % 5 == 0)
                    {
                        data[0] = contacts[i].ToString();
                        data[1] = contacts[i + 1].ToString();
                        data[2] = contacts[i + 2].ToString();
                        data[3] = contacts[i + 3].ToString();
                        data[4] = contacts[i + 4].ToString();

                        var item = new ListViewItem(data);      /* prosledjivanje na listview iz niza */
                        foreach (var il in data)
                        {
                            item.SubItems.Add(il);
                        }
                        list.Items.Add(item);
                    }
                }
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
    }
}
