using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Kyrsovaya
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();

            searchCriteriaComboBox.Items.AddRange(new string[] { "Ім'я", "Адреса", "ID клієнта", "ID замовлення" });

            whereField.ForeColor = Color.Gray;
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;
        }

        private string userRole;

        public ClientForm(string role)
        {
            InitializeComponent();
            userRole = role;
            HideButtonsBasedOnRole();

            searchCriteriaComboBox.Items.AddRange(new string[] { "Ім'я", "Адреса", "ID клієнта", "ID замовлення" });

            whereField.ForeColor = Color.Gray;
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
            textBox7.ForeColor = Color.Gray;
            textBox6.ForeColor = Color.Gray;
            textBox5.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;

            
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
                textBox6.Visible = false;
                textBox6.Enabled = false;
                textBox7.Visible = false;
                textBox7.Enabled = false;
                whereField.Visible = false;
                whereField.Enabled = false;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


        //  Відстеження і зміна положення форми
        Point lastPoint;
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
        private void FillClientDataGridView()
        {
            DB db3 = new DB();
            db3.openConnection();

            try
            {
                string query1 = "SELECT * FROM client";
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(query1, db3.getConnection());

                DataTable table1 = new DataTable();
                adapter1.Fill(table1);

                dataGridView1.DataSource = table1;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка під час завантаження даних: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася непередбачена помилка: " + ex.Message);
            }
            finally
            {
                db3.closeConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillClientDataGridView();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;
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

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            if (textBox2.Text == "Введіть адресу")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть адресу";
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

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "ID замовлення")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "ID замовлення";
                textBox4.ForeColor = Color.Gray;
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



        //  додати
        private void addButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand maxIdCommand = new MySqlCommand("SELECT MAX(ID_CLIENT) FROM client", db.getConnection());
                object maxIdObject = maxIdCommand.ExecuteScalar();
                int maxId = (maxIdObject == DBNull.Value) ? 0 : Convert.ToInt32(maxIdObject);

                int newId = maxId + 1;

                MySqlCommand checkIdCommand = new MySqlCommand("SELECT COUNT(*) FROM client WHERE ID_CLIENT = @id", db.getConnection());
                checkIdCommand.Parameters.AddWithValue("@id", newId);
                int idCount = Convert.ToInt32(checkIdCommand.ExecuteScalar());

                if (idCount > 0)
                {
                    MessageBox.Show("Запись с ID " + newId + " уже существует.");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `client` (`ID_CLIENT`, `CLIENT_ADRESS`, `CONTACT_INFORMATION`, `NAME`, `SURNAME`, `MIDLLE_NAME`, `ID_ORDER`) VALUES (@1, @2, @3, @4, @5, @6, @7)", db.getConnection());
                insertCommand.Parameters.Add("@1", MySqlDbType.Int32).Value = newId;
                insertCommand.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBox2.Text;
                insertCommand.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBox3.Text;
                insertCommand.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBox7.Text;
                insertCommand.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBox6.Text;
                insertCommand.Parameters.Add("@6", MySqlDbType.VarChar).Value = textBox5.Text;
                insertCommand.Parameters.Add("@7", MySqlDbType.Int32).Value = Convert.ToInt32(textBox4.Text);

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


        //  оновлення
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("UPDATE `client` SET `ID_CLIENT` = @1, `CLIENT_ADRESS` = @2, `CONTACT_INFORMATION` = @3, `NAME` = @4, `SURNAME` = @5, `MIDLLE_NAME` = @6, `ID_ORDER` = @7 WHERE `ID_CLIENT` = @8", db.getConnection());
                command.Parameters.Add("@1", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
                command.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@3", MySqlDbType.VarChar).Value = textBox3.Text;
                command.Parameters.Add("@4", MySqlDbType.VarChar).Value = textBox7.Text;
                command.Parameters.Add("@5", MySqlDbType.VarChar).Value = textBox6.Text;
                command.Parameters.Add("@6", MySqlDbType.VarChar).Value = textBox5.Text;
                command.Parameters.Add("@7", MySqlDbType.Int32).Value = Convert.ToInt32(textBox4.Text);
                command.Parameters.Add("@8", MySqlDbType.Int32).Value = Convert.ToInt32(whereField.Text);

                // Налагоджувальний вивід
                Console.WriteLine("ID_CLIENT: " + textBox1.Text);
                Console.WriteLine("CLIENT_ADRESS: " + textBox2.Text);
                Console.WriteLine("CONTACT_INFORMATION: " + textBox3.Text);
                Console.WriteLine("NAME: " + textBox7.Text);
                Console.WriteLine("SURNAME: " + textBox6.Text);
                Console.WriteLine("MIDLLE_NAME: " + textBox5.Text);
                Console.WriteLine("ID_ORDER: " + textBox4.Text);
                Console.WriteLine("WHERE ID_CLIENT: " + whereField.Text);

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
                MessageBox.Show("Введіть правильні значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка під час оновлення запису: " + ex.Message);
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


        //  видалення
        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM client WHERE ID_CLIENT = @d", db.getConnection());
                command.Parameters.Add("@d", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
                command.ExecuteNonQuery();

                MySqlCommand updateCommand = new MySqlCommand("SET @count = 0; UPDATE client SET ID_CLIENT = (@count:= @count + 1) ORDER BY ID_CLIENT;", db.getConnection());
                updateCommand.ExecuteNonQuery();

                MessageBox.Show("Запис успішно видалено.");
            }
            finally
            {
                db.closeConnection();
            }
        }

        //  пошук
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

            string query = "SELECT * FROM client WHERE ";

            switch (searchCriteria)
            {
                case "Ім'я":
                    query += "NAME LIKE @searchText";
                    break;
                case "Адреса":
                    query += "CLIENT_ADRESS LIKE @searchText";
                    break;
                case "ID клієнта":
                    query += "ID_CLIENT = @searchText";
                    break;
                case "ID замовлення":
                    query += "ID_ORDER = @searchText";
                    break;
                default:
                    MessageBox.Show("Виберіть критерій пошуку.");
                    return;
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            if (searchCriteria == "ID клієнта" || searchCriteria == "ID замовлення")
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

            dataGridView1.DataSource = table;

            db.closeConnection();

            decimal sum = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["CLIENT_ADRESS"].Value != null && row.Cells["ID_ORDER"].Value != null)
                    {
                        decimal addressLength = Convert.ToDecimal(row.Cells["CLIENT_ADRESS"].Value.ToString().Length);
                        int orderId = Convert.ToInt32(row.Cells["ID_ORDER"].Value);
                        sum += addressLength * orderId;
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm(userRole);
            mainForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeForm employeeForm = new EmployeeForm(userRole);
            employeeForm.Show();
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
