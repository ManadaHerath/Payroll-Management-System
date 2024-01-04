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
    public partial class EmpUpdate : Form
    {
        private int employeeId;

        public EmpUpdate(int selectedEmployeeId)
        {
            InitializeComponent();
            this.employeeId = selectedEmployeeId;

            
            LoadEmployeeData();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=payrolldb;Integrated Security=True");
        private void EmpUpdate_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadEmployeeData()
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE employee_id = @employeeId", con))
            {
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox1.Text = reader["first_name"].ToString();
                        textBox2.Text = reader["last_name"].ToString();

                        
                        string gender = reader["gender"].ToString();
                        if (gender == "M")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (gender == "F")
                        {
                            radioButton2.Checked = true;
                        }

                        dateTimePicker1.Value = (DateTime)reader["date_of_birth"];
                        textBox3.Text = reader["address"].ToString();
                        textBox4.Text = reader["contact_number"].ToString();
                        dateTimePicker2.Value = (DateTime)reader["hired_date"];
                        textBox5.Text = reader["overtime_rate"].ToString();
                        textBox6.Text = reader["basic_salary"].ToString();
                        textBox7.Text = reader["allowances"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found.");
                        this.Close();
                    }
                }
            }
        }


        private void UpdateEmployee()
        { 
            
            using (SqlCommand cmd = new SqlCommand("UPDATE Employees SET " +
                "first_name = @first_name, " +
                "last_name = @last_name, " +
                "gender = @gender, " +
                "date_of_birth = @date_of_birth, " +
                "address = @address, " +
                "contact_number = @contact_number, " +
                "hired_date = @hired_date, " +
                "overtime_rate = @overtime_rate, " +
                "basic_salary = @basic_salary, " +
                "allowances = @allowances " +
                "WHERE employee_id = @employee_id", con))
            {
                cmd.Parameters.AddWithValue("@employee_id", employeeId);
                cmd.Parameters.AddWithValue("@first_name", textBox1.Text);
                cmd.Parameters.AddWithValue("@last_name", textBox2.Text);

                
                cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "M" : "F");

                cmd.Parameters.AddWithValue("@date_of_birth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@contact_number", textBox4.Text);
                cmd.Parameters.AddWithValue("@hired_date", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@overtime_rate", int.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@basic_salary", decimal.Parse(textBox6.Text));
                cmd.Parameters.AddWithValue("@allowances", decimal.Parse(textBox7.Text));

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Employee updated successfully.");
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Failed to update employee.");
                }
                
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
    }
}
