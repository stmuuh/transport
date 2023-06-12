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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                string connString = "Server=localhost;port=3306;Database=transport;username=monty;password=some_pass";
                
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select*from " + selectedValue, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "Server=localhost;port=3306;Database=transport;username=monty;password=some_pass";
            MySqlConnection connection = new MySqlConnection(connString);
            string tableName = comboBox1.SelectedItem.ToString();

            // Создайте команду SELECT для выбранной таблицы
            string selectQuery = $"SELECT * FROM {tableName}";
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Создайте адаптер данных и настройте команду UPDATE
            MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM {tableName}", connection);
            MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter);

            // Сохраните изменения в базе данных
            adapter.Update(dataTable);
            connection.Close();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
