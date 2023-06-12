using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace transport
{
    public partial class Form2 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;port=3306;Database=transport;username=monty;password=some_pass");
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string script = $"insert into users (`login`,`password`,`role`) values('{textBox1.Text}','{textBox2.Text}','{comboBox1.Text}');insert into clients (`name`,`last_name`,`address`, `phone`, `email`) values('{textBox7.Text}','{textBox6.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";
            conn.Open();
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;port=3306;Database=transport;username=monty;password=some_pass"))
            {
                if (comboBox1.Text == "менеджер")
                {
                    MySqlCommand command = new MySqlCommand(script, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    MessageBox.Show("Регестрація пройшла успішно!");
                    this.Hide();
                    Form4 log = new Form4();
                    log.Show();
                    conn.Close();
                }
                if (comboBox1.Text == "користувач")
                {
                    MySqlCommand command = new MySqlCommand(script, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    MessageBox.Show("Регестрація пройшла успішно!");
                    this.Hide();
                    Form3 log = new Form3();
                    log.Show();
                    conn.Close();
                }
                conn.Close();

                
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 reg = new Form1();
            reg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
