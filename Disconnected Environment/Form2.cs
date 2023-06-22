using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class FormDataProdi : Form
    {
        private string stringConnection = "data source = LAPTOP-H0P5FB59\\SARAHHANNA17;" +
            "database = Universitas; User ID = sa;Password=123";
        private SqlConnection koneksi;
        public FormDataProdi()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshform();
        }
         
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void refreshform()
        {
            nmp.Text = "";
            nmp.Enabled = false;
            txtID.Text = "";
            txtID.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodii";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string nmProdi = nmp.Text;
            string idProdi = txtID.Text;

            if(nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert into dbo.Prodii (id_prodi, nama_prodi)" + "values(@id_prodi, @nama_prodi)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id_prodi", idProdi));
                cmd.Parameters.Add(new SqlParameter("nama_prodi", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
        private void FormDataProdi_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHalamanUtama hu = new FormHalamanUtama();
            hu.Show();
            this.Hide();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            nmp.Enabled = true;
            txtID.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;

        }
    }
}
