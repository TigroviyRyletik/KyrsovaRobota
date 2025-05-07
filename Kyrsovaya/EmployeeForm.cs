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
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
            searchCriteriaComboBox.Items.AddRange(new string[] { "Ім'я", "Прізвище", "ID працівника", "Номер телефону", "Зарплата" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            sumTextBox.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
        }

        private string userRole;

        public EmployeeForm(string role)
        {
            InitializeComponent();
            userRole = role;
            HideButtonsBasedOnRole();

            searchCriteriaComboBox.Items.AddRange(new string[] { "Ім'я", "Прізвище", "ID працівника", "Номер телефону", "Зарплата" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            sumTextBox.ForeColor = Color.Gray;
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
                textBox3.Enabled = false;
                textBox5.Visible = false;
                textBox5.Enabled = false;
                textBox6.Visible = false;
                textBox6.Enabled = false;
                textBox7.Visible = false;
                textBox7.Enabled = false;
                whereField.Visible = false;
                whereField.Enabled = false;
            }
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
                string query1 = "SELECT * FROM employee";
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

        private void button1_Click_1(object sender, EventArgs e)
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
            if (textBox2.Text == "Введіть зарплату")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть зарплату";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Номер телефону")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Номер телефону";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Імя")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Імя";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Прізвище")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Прізвище";
                textBox6.ForeColor = Color.Gray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "По батькові")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "По батькові";
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

        private void sumTextBox_Enter(object sender, EventArgs e)
        {
            if (sumTextBox.Text == "сума")
            {
                sumTextBox.Text = "";
                sumTextBox.ForeColor = Color.Black;
            }
        }

        private void sumTextBox_Leave(object sender, EventArgs e)
        {
            if (sumTextBox.Text == "")
            {
                sumTextBox.Text = "сума";
                sumTextBox.ForeColor = Color.Gray;
            }
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

        //  додати
        private void addButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand maxIdCommand = new MySqlCommand("SELECT MAX(ID_EMPLOYEE) FROM employee", db.getConnection());
                object maxIdObject = maxIdCommand.ExecuteScalar();
                int maxId = (maxIdObject == DBNull.Value) ? 0 : Convert.ToInt32(maxIdObject);

                int newId = maxId + 1;

                MySqlCommand checkIdCommand = new MySqlCommand("SELECT COUNT(*) FROM employee WHERE ID_EMPLOYEE = @id", db.getConnection());
                checkIdCommand.Parameters.AddWithValue("@id", newId);
                int idCount = Convert.ToInt32(checkIdCommand.ExecuteScalar());

                if (idCount > 0)
                {
                    MessageBox.Show("Запис з ID " + newId + " вже існує.");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `employee` (`ID_EMPLOYEE`, `NAME`, `SURNAME`, `MIDLLE_NAME`, `PHONE_NUMBER`, `SALARY`) VALUES (@1, @2, @3, @4, @5, @6)", db.getConnection());
                insertCommand.Parameters.Add("@1", MySqlDbType.Int32).Value = newId;
                insertCommand.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBox7.Text;
                insertCommand.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBox6.Text;
                insertCommand.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBox5.Text;
                insertCommand.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBox3.Text;
                insertCommand.Parameters.Add("@6", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBox2.Text);

                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Запис успішно додано.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення.");
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

        //  оновити
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("UPDATE `employee` SET `ID_EMPLOYEE` = @1, `NAME` = @2, `SURNAME` = @3, `MIDLLE_NAME` = @4, `PHONE_NUMBER` = @5, `SALARY` = @6 WHERE `ID_EMPLOYEE` = @7", db.getConnection());
                command.Parameters.Add("@1", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
                command.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBox7.Text; // NAME
                command.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBox6.Text; // SURNAME
                command.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBox5.Text; // MIDLLE_NAME
                command.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBox3.Text; // PHONE_NUMBER
                command.Parameters.Add("@6", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBox2.Text); // SALARY
                command.Parameters.Add("@7", MySqlDbType.Int32).Value = Convert.ToInt32(whereField.Text); // WHERE ID_EMPLOYEE

                // Отладочный вывод
                Console.WriteLine("ID_EMPLOYEE: " + textBox1.Text);
                Console.WriteLine("NAME: " + textBox7.Text);
                Console.WriteLine("SURNAME: " + textBox6.Text);
                Console.WriteLine("MIDLLE_NAME: " + textBox5.Text);
                Console.WriteLine("PHONE_NUMBER: " + textBox3.Text);
                Console.WriteLine("SALARY: " + textBox2.Text);
                Console.WriteLine("WHERE ID_EMPLOYEE: " + whereField.Text);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Запис успішно оновлено.");
                }
                else
                {
                    MessageBox.Show("Запис не знайдено або не було оновлено.");
                }
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
                MySqlCommand command = new MySqlCommand("DELETE FROM employee WHERE ID_EMPLOYEE = @d", db.getConnection());
                command.Parameters.Add("@d", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно видалено.");
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

            string searchText = searchTextBox.Text;
            string searchCriteria = searchCriteriaComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Виберіть критерій пошуку.");
                return;
            }

            string query = "SELECT * FROM employee WHERE ";

            switch (searchCriteria)
            {
                case "Ім'я":
                    query += "NAME LIKE @searchText";
                    break;
                case "Прізвище":
                    query += "SURNAME LIKE @searchText";
                    break;
                case "ID працівника":
                    query += "ID_EMPLOYEE = @searchText";
                    break;
                case "Номер телефону":
                    query += "PHONE_NUMBER LIKE @searchText";
                    break;
                case "Зарплата":
                    query += "SALARY = @searchText";
                    break;
                default:
                    MessageBox.Show("Виберіть критерій пошуку.");
                    return;
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            if (searchCriteria == "ID працівника" || searchCriteria == "Зарплата")
            {
                if (searchCriteria == "ID працівника")
                {
                    command.Parameters.AddWithValue("@searchText", Convert.ToInt32(searchText));
                }
                else
                {
                    command.Parameters.AddWithValue("@searchText", Convert.ToDecimal(value : searchText));
                }
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

            decimal sum = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["PHONE_NUMBER"].Value != null && row.Cells["SALARY"].Value != null)
                    {
                        // Замініть PHONE_NUMBER і SALARY на стовпці, для яких ви хочете обчислити суму
                        // У вашій таблиці немає стовпця ціни і кількості, тому я використовую PHONE_NUMBER і SALARY як приклади
                        // Вам потрібно замінити їх на стовпці, які ви хочете використовувати для обчислення суми
                        decimal salary = Convert.ToDecimal(row.Cells["SALARY"].Value);
                        int phoneNumberLength = row.Cells["PHONE_NUMBER"].Value.ToString().Length;
                        sum += salary * phoneNumberLength;
                    }
                }
            }

            sumTextBox.Text = sum.ToString();

            decimal sum1 = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["SALARY"].Value != null)
                    {
                        decimal salary = Convert.ToDecimal(row.Cells["SALARY"].Value);
                        int months = 0;
                        try
                        {
                            months = Convert.ToInt32(textBox4.Text); // Отримуємо кількість місяців з TextBox
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Введіть коректне числове значення для кількості місяців.");
                            return;
                        }
                        sum1 += salary * months; // Розраховуємо зарплату за період
                    }
                }
            }

            sumTextBox.Text = sum1.ToString();
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Кількість місяців")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Кількість місяців";
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrdersForm ordersForm = new OrdersForm(userRole);
            ordersForm.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
