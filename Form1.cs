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
namespace Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=payrolldb;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            SqlCommand cnn = new SqlCommand("Select * from Employees", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cnn = new SqlCommand("Insert into Employees(first_name, last_name, gender, date_of_birth, address, contact_number , hired_date, overtime_rate, basic_salary, allowances) values (@first_name , @last_name , @gender, @date_of_birth, @address, @contact_number, @hired_date, @overtime_rate, @basic_salary,@allowances)", con);

            cnn.Parameters.AddWithValue("@first_name",textBox1.Text);
            cnn.Parameters.AddWithValue("@last_name", textBox2.Text);
            if (radioButton1.Checked)
            {
                cnn.Parameters.AddWithValue("@gender", "M");
            }
            else if (radioButton2.Checked)
            {
                cnn.Parameters.AddWithValue("@gender", "F");
            }
            DateTime selectedDate = dateTimePicker1.Value.Date;
            cnn.Parameters.AddWithValue("@date_of_birth", selectedDate);

            cnn.Parameters.AddWithValue("@address", textBox3.Text);
            cnn.Parameters.AddWithValue("@contact_number", textBox4.Text);

            DateTime selectedDate2 = dateTimePicker2.Value.Date;
            cnn.Parameters.AddWithValue("@hired_date", selectedDate2);

            cnn.Parameters.AddWithValue("@overtime_rate", int.Parse(textBox5.Text));

            cnn.Parameters.AddWithValue("@basic_salary", decimal.Parse(textBox6.Text));

            cnn.Parameters.AddWithValue("@allowances", decimal.Parse(textBox7.Text));

            cnn.ExecuteNonQuery();

            con.Close();

            BindData();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }   

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton1.Checked= false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
