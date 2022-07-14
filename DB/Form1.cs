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

            MessageBox.Show("Record is inserted successfully into the DB!");

            //Clear the textboxes
            textBoxName.Clear();
            textBoxCity.Clear();
            textBoxCountry.Clear();
            textBoxName.Focus();
        }

        //Flow to connect and read data from DB:
           // 1-create a connection object to database
            //SqlConnection con = new SqlConnection(@"connection string");

            //2-open the connection
            //con.Open();

            //3-create a command object and set command type and command text(query) and then execute it
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            // cmd.CommandText = "SELECT * FROM table_db";
            //cmd.ExecuteNonQuery();

           // 4-create a temporary table in memory to save fetched data from database into it and then pass data to frontend(view grid)
            //DataTable dt = new DataTable();

            //5-Use sqlDataAdapter to fetch data from database and fill temporary table(dt object) in memory.
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);

            //6-After filling dt(temporary table in memory), assign this table to  DataSource attribute from dataGridViewDB object to show data in the grid:
            //dataGridView1.DataSource = dt;

            //7-close connection
            //con.Close();


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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM table_db WHERE name='"+textBoxName.Text+"'";
            cmd.ExecuteNonQuery();

            con.Close();

            //display data after modifications
            disp_data();
            MessageBox.Show("Record is deleted successfully!");

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE table_db SET name='"+textBoxCity.Text+"' WHERE name='"+textBoxName.Text+"'";
            cmd.ExecuteNonQuery();

            con.Close();

            //display data after modifications
            disp_data();
            MessageBox.Show("Record is updated successfully!");
        }
    }
}
