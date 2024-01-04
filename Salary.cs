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
using System.Globalization;

namespace Project1
{
    public partial class Salary : Form
    {
        private int employeeId;
        private int overtimeRate;
        private decimal basicSalary;
        private decimal allowances;
        private decimal noPayValue;
        private decimal basePay;
        private decimal grossPay;
        
        public Salary(int selectedEmployeeId, decimal selectedBasicSalary, decimal selectedAllowances, int selectedEmployeerate)
        {
            InitializeComponent();
            this.employeeId = selectedEmployeeId;
            this.basicSalary = selectedBasicSalary;
            this.allowances = selectedAllowances;
            this.overtimeRate = selectedEmployeerate;
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=payrolldb;Integrated Security=True");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Salary_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
            
        {
            DateTime BeginDate;
            DateTime EndDate;
            con.Open();

            
            string selectQuery = "SELECT TOP 1 cycle_begin_date, cycle_end_date FROM Settings ORDER BY setting_id DESC";

            using (SqlCommand cmd = new SqlCommand(selectQuery, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        BeginDate = (DateTime)reader["cycle_begin_date"];
                        EndDate = (DateTime)reader["cycle_end_date"];


                        if (BeginDate.Date == dateTimePicker1.Value.Date && EndDate.Date == dateTimePicker2.Value.Date)
                        {
                            TimeSpan dateRange = dateTimePicker2.Value - dateTimePicker1.Value;
                            int salaryCycleDateRange = dateRange.Days;

                            int no_of_leaves = int.Parse(textBox1.Text);
                            int no_of_hours = int.Parse(textBox2.Text);

                            decimal no_pay_value = Math.Round((basicSalary / salaryCycleDateRange) * no_of_leaves, 2);
                            decimal base_pay_value = Math.Round(basicSalary + allowances + (overtimeRate * no_of_hours), 2);
                            decimal gross_pay = Math.Round(base_pay_value - (no_pay_value + base_pay_value * (decimal)0.01), 2);


                            label7.Text = $"No Pay Value: Rs. {no_pay_value}";
                            label8.Text = $"Base Pay Value: Rs. {base_pay_value}";
                            label9.Text = $"Gross Pay: Rs. {gross_pay}";
                            noPayValue = no_pay_value;
                            basePay = base_pay_value;
                            grossPay = gross_pay;

                        }
                        else
                        {
                            MessageBox.Show("Begin and End Dates you picked do not match the system data.Try again");
                        }


                    }
                    else
                    {
                        MessageBox.Show("No rows found in the Settings table.");
                    }
                }
            }

            con.Close();
            


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Salary (employee_id, month, no_pay_value, base_pay_value, gross_pay) " + "VALUES (@EmployeeId, @Month, @NoPayValue, @BasePayValue, @GrossPay)", con))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.AddWithValue("@EmployeeId", employeeId); 
                cmd.Parameters.AddWithValue("@Month", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@NoPayValue", noPayValue);
                cmd.Parameters.AddWithValue("@BasePayValue",basePay);
                cmd.Parameters.AddWithValue("@GrossPay", grossPay);

                
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Salary data added to the database.");
        }

        private void button3_Click(object sender, EventArgs e)

        {
            

            SqlCommand cmd = new SqlCommand("SELECT * FROM Salary where employee_id =@EmployeeId", con);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);

            dataGridView1.DataSource = table;
        }
    }
}