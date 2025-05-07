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

namespace Kyrsovaya
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
            searchCriteriaComboBox.Items.AddRange(new string[] { "ID клієнта", "ID працівника", "Ціна", "ID замовлення", "Дата" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;


        }

        private string userRole;

        public OrdersForm(string role)
        {
            InitializeComponent();
            userRole = role;
            HideButtonsBasedOnRole();
            searchCriteriaComboBox.Items.AddRange(new string[] { "ID клієнта", "ID працівника", "Ціна", "ID замовлення", "Дата" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;

        }

        private void HideButtonsBasedOnRole()
        {
            if (userRole == "user")
            {
                button2.Visible = false;
                button2.Enabled = false;
                addButton.Visible = false;
                addButton.Enabled = false;
                button3.Visible = false;
                button3.Enabled = false;


                textBox1.Visible = false;
                textBox1.Enabled = false;
                textBox2.Visible = false;
                textBox2.Enabled = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox4.Enabled = false;
                textBox3.Enabled = false;
                textBox5.Visible = false;
                textBox5.Enabled = false;
                whereField.Visible = false;
                whereField.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeForm employeeForm = new EmployeeForm(userRole);
            employeeForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientForm clientForm = new ClientForm(userRole);
            clientForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm(userRole);
            mainForm.Show();
        }


        //  Відстеження і зміна положення форми
        Point lastPoint;
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FillEmployeeDataGridView()
        {
            DB db3 = new DB();
            db3.openConnection();

            try
            {
                string query1 = "SELECT * FROM orders";
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(query1, db3.getConnection());

                DataTable table1 = new DataTable();
                adapter1.Fill(table1);

                dataGridView1.DataSource = table1; // Предполагаем, что DataGridView называется dataGridView1
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message);
            }
            finally
            {
                db3.closeConnection();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FillEmployeeDataGridView();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введіть ID")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введіть ID";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Дата")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Дата";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "ID клієнта")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "ID клієнта";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "ID працівника")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "ID працівника";
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Введіть критерій")
            {
                searchTextBox.Text = "";
                searchTextBox.ForeColor = Color.Black;
            }
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                searchTextBox.Text = "Введіть критерій";
                searchTextBox.ForeColor = Color.Gray;
            }
        }

        private void whereField_Enter(object sender, EventArgs e)
        {
            if (whereField.Text == "Що оновити?")
            {
                whereField.Text = "";
                whereField.ForeColor = Color.Black;
            }
        }

        private void whereField_Leave(object sender, EventArgs e)
        {
            if (whereField.Text == "")
            {
                whereField.Text = "Що оновити?";
                whereField.ForeColor = Color.Gray;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                int employeeId = Convert.ToInt32(textBox5.Text);

                MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM employee WHERE ID_EMPLOYEE = @id", db.getConnection());
                checkCommand.Parameters.AddWithValue("@id", employeeId);
                int employeeCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (employeeCount == 0)
                {
                    MessageBox.Show("Працівника з ID " + employeeId + " не знайдено.");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `orders` (`ORDER_DATE`, `ORDER_AMOUNT`, `ID_CLIENT`, `ID_EMPLOYEE`) VALUES (@1, @2, @3, @4)", db.getConnection());
                insertCommand.Parameters.Add("@1", MySqlDbType.Date).Value = Convert.ToDateTime(textBox2.Text);
                insertCommand.Parameters.Add("@2", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBox4.Text);
                insertCommand.Parameters.Add("@3", MySqlDbType.Int32).Value = Convert.ToInt32(textBox3.Text);
                insertCommand.Parameters.Add("@4", MySqlDbType.Int32).Value = employeeId;

                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Запис успішно додано.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при додаванні запису: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася непередбачена помилка: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Ціна")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Ціна";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("UPDATE orders SET ID_CLIENT = @client, ID_EMPLOYEE = @employee, ORDER_AMOUNT = @amount, ORDER_DATE = @orderDate WHERE ID_ORDER = @orderId", db.getConnection());

                // Перетворення тексту з textBox2 на DateTime
                DateTime orderDate = DateTime.Parse(textBox2.Text);

                command.Parameters.AddWithValue("@client", Convert.ToInt32(textBox3.Text)); // textBox3 - ID клієнта
                command.Parameters.AddWithValue("@employee", Convert.ToInt32(textBox5.Text)); // textBox5 - ID працівника
                command.Parameters.AddWithValue("@amount", Convert.ToDecimal(textBox4.Text)); // textBox4 - Ціна
                command.Parameters.AddWithValue("@orderId", Convert.ToInt32(whereField.Text)); // whereField - ID замовлення для оновлення
                command.Parameters.AddWithValue("@orderDate", orderDate); // Додаємо дату

                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно оновлено.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при оновленні запису: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася непередбачена помилка: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM orders WHERE ID_ORDER = @d", db.getConnection());
                command.Parameters.Add("@d", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text); // Використовуємо whereField для ID замовлення
                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно видалено.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при видаленні запису: " + ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректний ID.");
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            string searchText = "";
            string searchCriteria = searchCriteriaComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Виберіть критерій пошуку.");
                return;
            }

            string query = "SELECT * FROM orders WHERE ";

            switch (searchCriteria)
            {
                case "ID клієнта":
                    query += "ID_CLIENT = @searchText";
                    searchText = searchTextBox.Text;
                    break;
                case "ID працівника":
                    query += "ID_EMPLOYEE = @searchText";
                    searchText = searchTextBox.Text;
                    break;
                case "Ціна":
                    query += "ORDER_AMOUNT = @searchText";
                    searchText = searchTextBox.Text;
                    break;
                case "ID замовлення":
                    query += "ID_ORDER = @searchText";
                    searchText = searchTextBox.Text;
                    break;
                case "Дата":
                    query += "ORDER_DATE = @searchText";
                    searchText = searchTextBox.Text;
                    break;
                default:
                    MessageBox.Show("Виберіть критерій пошуку.");
                    return;
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            if (searchCriteria == "Ціна")
            {
                command.Parameters.AddWithValue("@searchText", Convert.ToDecimal(searchText));
            }
            else if (searchCriteria == "ID клієнта" || searchCriteria == "ID працівника" || searchCriteria == "ID замовлення")
            {
                command.Parameters.AddWithValue("@searchText", Convert.ToInt32(searchText));
            }
            else if (searchCriteria == "Дата")
            {
                command.Parameters.AddWithValue("@searchText", Convert.ToDateTime(searchText));
            }
            else
            {
                command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table;

            db.closeConnection();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            PurveyorForm purveyourForm = new PurveyorForm(userRole);
            purveyourForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            StoragesForm storagesForm = new StoragesForm(userRole);
            storagesForm.Show();
        }
    }
}
