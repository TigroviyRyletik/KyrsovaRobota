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

namespace Kyrsovaya
{
    public partial class StoragesForm : Form
    {
        public StoragesForm()
        {
            InitializeComponent();
            searchCriteriaComboBox.Items.AddRange(new string[] { "Кількість", "ID постачальника", "ID" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
        }


        private string userRole;

        public StoragesForm(string role)
        {
            InitializeComponent();
            userRole = role;
            HideButtonsBasedOnRole();
            searchCriteriaComboBox.Items.AddRange(new string[] { "Кількість", "ID постачальника", "ID" });

            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            whereField.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;
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
                whereField.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientForm clientForm = new ClientForm(userRole);
            clientForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeForm employeeForm = new EmployeeForm(userRole);
            employeeForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm(userRole);
            mainForm.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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


        private void FillEmployeeDataGridView()
        {
            DB db3 = new DB();
            db3.openConnection();

            try
            {
                string query1 = "SELECT * FROM storages";
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(query1, db3.getConnection());

                DataTable table1 = new DataTable();
                adapter1.Fill(table1);

                dataGridView1.DataSource = table1;
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
            if (textBox2.Text == "Кількість")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Кількість";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "ID поставника")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "ID поставника";
                textBox3.ForeColor = Color.Gray;
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

        //  додати
        private void addButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO storages (QUANTITY, ID_PURVEYOR) VALUES (@quantity, @purveyorId)", db.getConnection());
                command.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox2.Text)); // textBox2 - Кількість
                command.Parameters.AddWithValue("@purveyorId", Convert.ToInt32(textBox3.Text)); // textBox3 - ID постачальника
                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно додано.");
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


        //  оновлення
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("UPDATE storages SET QUANTITY = @quantity, ID_PURVEYOR = @purveyorId WHERE ID_INVENTORY = @id", db.getConnection());
                command.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox2.Text)); // textBox2 - Кількість
                command.Parameters.AddWithValue("@purveyorId", Convert.ToInt32(textBox3.Text)); // textBox3 - ID постачальника
                command.Parameters.AddWithValue("@id", Convert.ToInt32(whereField.Text)); // whereField - ID для оновлення
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
                MySqlCommand command = new MySqlCommand("DELETE FROM storages WHERE ID_INVENTORY = @id", db.getConnection());
                command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text)); // textBox1 - ID
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

            string searchText = searchTextBox.Text;
            string searchCriteria = searchCriteriaComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Виберіть критерій пошуку.");
                return;
            }

            string query = "SELECT * FROM storages WHERE ";

            switch (searchCriteria)
            {
                case "Кількість":
                    query += "QUANTITY = @searchText";
                    break;
                case "ID постачальника":
                    query += "ID_PURVEYOR = @searchText";
                    break;
                case "ID":
                    query += "ID_INVENTORY = @searchText";
                    break;
                default:
                    MessageBox.Show("Виберіть критерій пошуку.");
                    return;
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            if (searchCriteria == "Кількість" || searchCriteria == "ID постачальника" || searchCriteria == "ID")
            {
                command.Parameters.AddWithValue("@searchText", Convert.ToInt32(searchText));
            }
            else
            {
                command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table; // Припустимо, що таблиця для відображення результатів пошуку має ім'я dataGridView1

            db.closeConnection();
        }
    }
}
