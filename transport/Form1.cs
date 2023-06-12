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

namespace transport
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("Server=localhost;port=3306;Database=transport;username=monty;password=some_pass");

        public Form1()
        {
            InitializeComponent();
        }
        public int userIndex;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Не заповнене значення!");
            }
            else if (textBox2.Text == "")
            {
                errorProvider2.SetError(textBox2, "Не заповнене значення!");
            }
            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                int I;
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from users where login = '" + textBox1.Text + "'and password= '" + textBox2.Text + "'and role= 'користувач'";
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
                I = Convert.ToInt32(table.Rows.Count.ToString());
                if (I == 1)
                {
                    this.Hide();
                    Form3 main = new Form3();
                    main.Show();
                    userIndex = GetUserIDFromDatabase(textBox1.Text);
                }
                int I1;
                
                MySqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select * from users where login = '" + textBox1.Text + "'and password= '" + textBox2.Text + "'and role= 'менеджер'";
                DataTable table1 = new DataTable();
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                adapter1.Fill(table1);
                I1 = Convert.ToInt32(table1.Rows.Count.ToString());
                if (I1 == 1)
                {
                    this.Hide();
                    Form4 main = new Form4();
                    main.Show();
                }
                conn.Close();
            }
        }

        private int GetUserIDFromDatabase(string login)
        {
            int userID = 0;

            using (MySqlConnection connection = new MySqlConnection("Server = localhost; port = 3306; Database = transport; username = monty; password = some_pass"))
            {
                connection.Open();

                string selectQuery = "SELECT user_id FROM users WHERE login = @login";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@login", login);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        userID = Convert.ToInt32(result);
                    }
                }
            }

            return userID;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 reg = new Form2();
            reg.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
