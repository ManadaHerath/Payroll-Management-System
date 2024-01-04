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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project1
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=payrolldb;Integrated Security=True");
        private void Settings_Load(object sender, EventArgs e)
        {
            BindData();
        }
        void BindData()
        {
            SqlCommand cnn = new SqlCommand("Select * from Settings", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            da.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cnn = new SqlCommand("Insert into Settings(date_range_for_cycle, cycle_begin_date, cycle_end_date, no_of_leaves_for_employee) values (@date_range_for_cycle ,@cycle_begin_date ,@cycle_end_date , @no_of_leaves_for_employeee)", con);

            cnn.Parameters.AddWithValue("@date_range_for_cycle", int.Parse(textBox1.Text));
            cnn.Parameters.AddWithValue("@no_of_leaves_for_employeee", int.Parse(textBox2.Text));
            
            DateTime selectedDate = dateTimePicker1.Value.Date;
            cnn.Parameters.AddWithValue("@cycle_begin_date", selectedDate);

            DateTime selectedDate2 = dateTimePicker2.Value.Date;
            cnn.Parameters.AddWithValue("@cycle_end_date", selectedDate2);

            cnn.ExecuteNonQuery();

            con.Close();

            BindData();
        }
    }
}
