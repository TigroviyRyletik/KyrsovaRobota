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
using System.Threading;
using System.IO;

namespace Kyrsovaya
{
    public partial class MainForm : Form
    {
        private Thread updateThread;
        public MainForm()
        {

            InitializeComponent();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSize = true;

            button1_Click(null, null);

            whereField.ForeColor = Color.Gray;
            deleteField.ForeColor = Color.Gray;
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            nameField.ForeColor = Color.Gray;
            quantityField.ForeColor = Color.Gray;
            textBox3.ForeColor = Color.Gray;
            priceField.ForeColor = Color.Gray;
            textBox4.ForeColor = Color.Gray;
            searchTextBox.ForeColor = Color.Gray;

            searchCriteriaComboBox.Items.AddRange(new string[] { "Назва", "Ціна", "ID" });
            searchCriteriaComboBox.SelectedIndex = 0;
        }
        
            private string userRole;

            public MainForm(string role)
            {
                InitializeComponent();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                userRole = role;
                HideButtonsBasedOnRole();
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoSize = true;

                button1_Click(null, null);

                whereField.ForeColor = Color.Gray;
                deleteField.ForeColor = Color.Gray;
                textBox1.ForeColor = Color.Gray;
                textBox2.ForeColor = Color.Gray;
                nameField.ForeColor = Color.Gray;
                quantityField.ForeColor = Color.Gray;
                textBox3.ForeColor = Color.Gray;
                priceField.ForeColor = Color.Gray;
                textBox4.ForeColor = Color.Gray;
                searchTextBox.ForeColor = Color.Gray;

                searchCriteriaComboBox.Items.AddRange(new string[] { "Назва", "Ціна", "ID" });
                searchCriteriaComboBox.SelectedIndex = 0;
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
                button11.Visible = false;
                button11.Enabled = false;


                deleteField.Visible = false;
                deleteField.Enabled = false;
                nameField.Visible = false;
                nameField.Enabled = false;
                quantityField.Visible = false;
                quantityField.Enabled = false;
                priceField.Visible = false;
                priceField.Enabled = false;
                whereField.Visible = false;
                whereField.Enabled = false;
                textBox1.Visible = false;
                textBox1.Enabled = false;
                textBox2.Visible = false;
                textBox2.Enabled = false;
                textBox3.Visible = false;
                textBox3.Enabled = false;
                textBox4.Visible = false;
                textBox4.Enabled = false;
            }
            }
        



        private void StartUpdateThread()
        {
            updateThread = new Thread(UpdateThread_Run);
            updateThread.Start();
        }

        private void UpdateThread_Run()
        {
            while (true)
            {
                // Здесь нужно проверить, произошло ли изменение в базе данных
                // Например, проверить значение поля, которое обновляется триггером
                if (CheckForDatabaseChanges())
                {
                    Invoke(new Action(() => button1_Click(null, null))); // Обновляем DataGridView в потоке UI
                }
                Thread.Sleep(5000); // Интервал проверки (5 секунд)
            }
        }

        private bool CheckForDatabaseChanges()
        {
            // Здесь нужно реализовать логику проверки изменений в базе данных
            // Например, проверить значение поля, которое обновляется триггером
            // Вернуть true, если изменения произошли, иначе false
            // ...
            return false; // Заглушка, нужно реализовать логику
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

        //   Оновлення датаГрид
        private void button1_Click(object sender, EventArgs e)
        {

            DB db2 = new DB();
            db2.openConnection();

            string query = "SELECT ID_MENU_ITEM, POSITION_NAME, QUANTITY, PRICE FROM menu";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db2.getConnection());

            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table; // Привязываем DataTable к DataGridView


            db2.closeConnection();
        }

        //   Видалення
        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM menu WHERE ID_MENU_ITEM = @d", db.getConnection());
                command.Parameters.Add("@d", MySqlDbType.Int32).Value = Convert.ToInt32(deleteField.Text);
                command.ExecuteNonQuery();

                // Оновлення ID_MENU_ITEM
                MySqlCommand updateCommand = new MySqlCommand("SET @count = 0; UPDATE menu SET ID_MENU_ITEM = (@count:= @count + 1) ORDER BY ID_MENU_ITEM;", db.getConnection());


                MessageBox.Show("Запис успішно видалено.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть корректний ID.");
            }
            finally
            {
                db.closeConnection();
            }
        }


        //  Додавання полів
        private void button3_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                // Отримання максимального ID
                MySqlCommand maxIdCommand = new MySqlCommand("SELECT MAX(ID_MENU_ITEM) FROM menu", db.getConnection());
                object maxIdObject = maxIdCommand.ExecuteScalar();
                int maxId = (maxIdObject == DBNull.Value) ? 0 : Convert.ToInt32(maxIdObject);

                // Перетворення зображення на масив байтів
                byte[] imageData = null;
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        imageData = ms.ToArray();
                    }
                }

                // Запит INSERT з параметром для зображення
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `menu` (`ID_MENU_ITEM`, `POSITION_NAME`, `QUANTITY`, `PRICE`, `PHOTO`) VALUES (@Id, @Po, @Q, @Pr, @Photo)", db.getConnection());
                insertCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = maxId + 1;
                insertCommand.Parameters.Add("@Po", MySqlDbType.VarChar).Value = nameField.Text;
                insertCommand.Parameters.Add("@Q", MySqlDbType.Int32).Value = Convert.ToInt32(quantityField.Text);
                insertCommand.Parameters.Add("@Pr", MySqlDbType.Decimal).Value = Convert.ToDecimal(priceField.Text);
                insertCommand.Parameters.Add("@Photo", MySqlDbType.Blob).Value = imageData;

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
            finally
            {
                db.closeConnection();
            }
        }


        //  оновлення полів
        private void button3_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            try
            {
                // Перетворення зображення на масив байтів
                byte[] imageData = null;
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        imageData = ms.ToArray();
                    }
                }

                // Запит UPDATE з параметром для зображення
                MySqlCommand command = new MySqlCommand("UPDATE `menu` SET `ID_MENU_ITEM` = @1, `POSITION_NAME` = @2, `QUANTITY` = @3, `PRICE` = @4, `PHOTO` = @6 WHERE `menu`.`ID_MENU_ITEM` = @5", db.getConnection());
                command.Parameters.Add("@1", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
                command.Parameters.Add("@2", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@3", MySqlDbType.Int32).Value = Convert.ToInt32(textBox3.Text);
                command.Parameters.Add("@4", MySqlDbType.Decimal).Value = Convert.ToDecimal(textBox4.Text);
                command.Parameters.Add("@5", MySqlDbType.Int32).Value = Convert.ToInt32(whereField.Text);
                command.Parameters.Add("@6", MySqlDbType.Blob).Value = imageData; // Додавання параметра для зображення

                command.ExecuteNonQuery();

                MessageBox.Show("Запис успішно оновлено.");
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні числові значення.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка при оновленні запису: " + ex.Message);
            }
            finally
            {
                db.closeConnection();
            }
        }

        //  підсказки в текстбоксі
        private void deleteField_Enter(object sender, EventArgs e)
        {
            if (deleteField.Text == "Введіть ID")
            {
                deleteField.Text = "";
                deleteField.ForeColor = Color.Black;
            }
        }

        private void deleteField_Leave(object sender, EventArgs e)
        {
            if (deleteField.Text == "")
            {
                deleteField.Text = "Введіть ID";
                deleteField.ForeColor = Color.Gray;
            }
        }

        private void whereField_Enter(object sender, EventArgs e)
        {
            if (whereField.Text == "Введіть ID")
            {
                whereField.Text = "";
                whereField.ForeColor = Color.Black;
            }
        }

        private void whereField_Leave(object sender, EventArgs e)
        {
            if (whereField.Text == "")
            {
                whereField.Text = "Введіть ID";
                whereField.ForeColor = Color.Gray;
            }
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
            if (textBox2.Text == "Введіть назву")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Введіть назву";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void nameField_Enter(object sender, EventArgs e)
        {
            if (nameField.Text == "Введіть назву")
            {
                nameField.Text = "";
                nameField.ForeColor = Color.Black;
            }
        }

        private void nameField_Leave(object sender, EventArgs e)
        {
            if (nameField.Text == "")
            {
                nameField.Text = "Введіть назву";
                nameField.ForeColor = Color.Gray;
            }
        }

        private void quantityField_Enter(object sender, EventArgs e)
        {
            if (quantityField.Text == "Введіть кількість")
            {
                quantityField.Text = "";
                quantityField.ForeColor = Color.Black;
            }
        }

        private void quantityField_Leave(object sender, EventArgs e)
        {
            if (quantityField.Text == "")
            {
                quantityField.Text = "Введіть кількість";
                quantityField.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введіть кількість")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Введіть кількість";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void priceField_Enter(object sender, EventArgs e)
        {
            if (priceField.Text == "Введіть ціну")
            {
                priceField.Text = "";
                priceField.ForeColor = Color.Black;
            }
        }

        private void priceField_Leave(object sender, EventArgs e)
        {
            if (priceField.Text == "")
            {
                priceField.Text = "Введіть ціну";
                priceField.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Введіть ціну")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Введіть ціну";
                textBox4.ForeColor = Color.Gray;
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


        //  пошук
        private void searchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            string searchText = searchTextBox.Text;
            string searchCriteria = searchCriteriaComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(searchCriteria))
            {
                MessageBox.Show("Выберите критерий поиска.");
                return;
            }

            string query = "SELECT ID_MENU_ITEM, POSITION_NAME, QUANTITY, PRICE FROM menu WHERE ";

            switch (searchCriteria)
            {
                case "Назва":
                    query += "POSITION_NAME LIKE @searchText";
                    break;
                case "Ціна":
                    query += "PRICE = @searchText";
                    break;
                case "ID":
                    query += "ID_MENU_ITEM = @searchText";
                    break;
                default:
                    MessageBox.Show("Виберіть критерий пошуку.");
                    return;
            }

            MySqlCommand command = new MySqlCommand(query, db.getConnection());

            if (searchCriteria == "Ціна")
            {
                command.Parameters.AddWithValue("@searchText", Convert.ToDecimal(searchText));
            }
            else if (searchCriteria == "ID")
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

            if (table.Rows.Count > 0)
            {
                // Отримати ID першого результату
                int id = Convert.ToInt32(table.Rows[0]["ID_MENU_ITEM"]);

                // Окремий запит для отримання зображення
                string imageQuery = "SELECT PHOTO FROM menu WHERE ID_MENU_ITEM = @id";
                MySqlCommand imageCommand = new MySqlCommand(imageQuery, db.getConnection());
                imageCommand.Parameters.AddWithValue("@id", id);

                object imageResult = imageCommand.ExecuteScalar();

                if (imageResult != null && imageResult != DBNull.Value)
                {
                    byte[] imageData = (byte[])imageResult;

                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

            db.closeConnection();

            decimal sum = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["PRICE"].Value != null && row.Cells["QUANTITY"].Value != null)
                    {
                        decimal price = Convert.ToDecimal(row.Cells["PRICE"].Value);
                        int quantity = Convert.ToInt32(row.Cells["QUANTITY"].Value);
                        sum += price * quantity;
                    }
                }
            }

            // Отображение результата
            sumTextBox.Text = sum.ToString();
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

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            openFileDialog1.Title = "Виберіть файл зображення";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
