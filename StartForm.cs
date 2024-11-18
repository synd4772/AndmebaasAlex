using AndmebaasAlex.AndmebaasDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndmebaasAlex
{
    public partial class StartForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=andmebaas;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        int ID;
        public StartForm()
        {
            InitializeComponent();
            //NaitaAndmed();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'andmebaasDataSet.Toode' table. You can move, or remove it, as needed.
            this.toodeTableAdapter.Fill(this.andmebaasDataSet.Toode);

        }

        public void NaitaAndmed()
        {
            this.toodeTableAdapter.Fill(this.andmebaasDataSet.Toode);
        }

        private void LisaBtn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() !=string.Empty && Hind_txt.Text.Trim() != string.Empty) 
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO Toode(Nimetus,Kogus,Hind) VALUES (@toode,@kogus,@hind)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga!");
                }
            }
            else
            {
                MessageBox.Show("Sisesta Andmeid!");
            }
        }

        private void KustutaBtn_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("DELETE FROM Toode WHERE Nimetus = @Nimetus AND Kogus = @kogus AND Hind = @hind", conn);
            cmd.Parameters.AddWithValue("@Nimetus", Nimetus_txt.Text);
            cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
            cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("DBCC CHECKIDENT ('Toode', RESEED, 0)", conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            this.toodeTableAdapter.Fill(this.andmebaasDataSet.Toode);

        }
        private void UuendaBtn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE Toode SET Nimetus = @toode, Kogus = @kogus, Hind = @hind WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    NaitaAndmed();

                    Nimetus_txt.Text = "";
                    Kogus_txt.Text = "";
                    Hind_txt.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga!");
                }
            }
            else
            {
                MessageBox.Show("Sisesta Andmeid!");
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = e.RowIndex;
        }
        OpenFileDialog open;
        SaveFileDialog save;
        private void PildiOtsing_BackColorChanged(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Images Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.jpg";
            FileInfo openFile = new FileInfo(@"C:\Users\opilane\Pictures\" + open.FileName);
            if(open.ShowDialog() == DialogResult.OK && Nimetus_txt.Text != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\..\Pildid");
                string extension = Path.GetExtension(open.FileName);
                save.FileName = Nimetus_txt.Text + extension;
                save.Filter = "Images" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);
                if(save.ShowDialog() == DialogResult.OK && Nimetus_txt != null)
                {
                    File.Copy(open.FileName, save.FileName);
                    pictureBox1.Image = Image.FromFile(save.FileName);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                }
            }
        }
    }
}
