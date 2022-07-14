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

namespace DB
{
    public partial class Form1 : Form
    {

        //connection string
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NQF55SP;Initial Catalog=sqldb;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();

            //Display contents on start the app
            disp_data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            con.Open(); //open DB connection

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO table_db VALUES('"+textBoxName.Text+"', '"+textBoxCity.Text+ "', '"+textBoxCountry.Text+"')";
            cmd.ExecuteNonQuery();

            con.Close(); //close DB connection

            disp_data();

            MessageBox.Show("Information is inserted successfully into the DB!");

            //Clear the textboxes
            textBoxName.Clear();
            textBoxCity.Clear();
            textBoxCountry.Clear();
            textBoxName.Focus();
        }


        public void disp_data()
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM table_db";
            cmd.ExecuteNonQuery();
            
            //to show on the dataGridView
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridViewDB.DataSource = dt;

            con.Close();

        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to exit?", "Confirmation", 
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
