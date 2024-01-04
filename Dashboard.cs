using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 formEmployee = new Form1();

            formEmployee.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            EmpView Empview = new EmpView();
            Empview.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings SettingForm = new Settings();
            SettingForm.Show();
        }
    }
}
