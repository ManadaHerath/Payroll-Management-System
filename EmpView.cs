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
using System.Data.SqlClient;

namespace Project1
{
    public partial class EmpView : Form
    {
        public EmpView()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=payrolldb;Integrated Security=True");


        private void EmpView_Load(object sender, EventArgs e)
        {
            SqlCommand cnn = new SqlCommand("Select * from Employees", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);

            DataTable table = new DataTable();

            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string searchKeyword = textBox1.Text.Trim();

            
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE first_name LIKE @searchTerm", con);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchKeyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();

                da.Fill(table);
                dataGridView1.DataSource = table;
            }
            else
            {
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                da.Fill(table);

                dataGridView1.DataSource = table;
            }
        }
        private void LoadEmployees()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);

            dataGridView1.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveEmployee();
            LoadEmployees();
        }
        private void RemoveEmployee()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;

                using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE employee_id = @selectedEmployeeId", con))
                {
                    cmd.Parameters.AddWithValue("@selectedEmployeeId", selectedEmployeeId);

                    
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Employee removed successfully.");
                        LoadEmployees(); 
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove the employee.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to remove.");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;

                
                EmpUpdate updateForm = new EmpUpdate(selectedEmployeeId);
                updateForm.Show();

                
                LoadEmployees();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["employee_id"].Value;
                int selectedEmployeerate = (int)dataGridView1.SelectedRows[0].Cells["overtime_rate"].Value;
                decimal selectedBasicSalary = (decimal)dataGridView1.SelectedRows[0].Cells["basic_salary"].Value;
                decimal selectedAllowances = (decimal)dataGridView1.SelectedRows[0].Cells["allowances"].Value;

                
                Salary salary = new Salary(selectedEmployeeId,selectedBasicSalary,selectedAllowances,selectedEmployeerate);
                salary.Show();

            }
        }
    }
}