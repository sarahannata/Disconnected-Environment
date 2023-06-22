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
    public partial class Form5 : Form
    {
        private string stringConnection = "data source = LAPTOP-H0P5FB59\\SARAHHANNA17;" +
            "database = Universitas; User ID = sa;Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        private BindingSource CustomerBindingSource = new BindingSource();
        public Form5()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bindingNavigator1.BindingSource = this.CustomerBindingSource;
            refreshform();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            cbxJenisKelamin.SelectedIndex = -1;
            cbxProdi.SelectedIndex = -1;
            dtTanggalLahir.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = txtNIM.Text.Trim();
            nama = txtNama.Text.Trim();
            alamat = txtAlamat.Text.Trim();
            jk = cbxJenisKelamin.SelectedItem.ToString();
            prodi = cbxProdi.SelectedValue.ToString();
            tgl = dtTanggalLahir.Value;
            if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(alamat) || string.IsNullOrEmpty(jk) || string.IsNullOrEmpty(prodi))
            {
                MessageBox.Show("Please fill in all identity fields!");
            }
            else
            {
                koneksi.Open();
                string str = "INSERT INTO Mahhasiswa (nim, nama_mahasiswa, alamat, jenis_kelamin, id_prodi, tgl_lahir) VALUES (@nim, @nama_mahasiswa, @alamat, @jenis_kelamin, @id_prodi, @tgl_lahir)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@nim", nim);
                cmd.Parameters.AddWithValue("@nama_mahasiswa", nama);
                cmd.Parameters.AddWithValue("@jenis_kelamin", jk);
                cmd.Parameters.AddWithValue("@alamat", alamat);
                cmd.Parameters.AddWithValue("@tgl_lahir", tgl);
                cmd.Parameters.AddWithValue("@id_prodi", prodi);
                cmd.ExecuteNonQuery();

                koneksi.Close();

                MessageBox.Show("Data Berhasil Disimpan");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtTanggalLahir.Value = DateTime.Today;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = true;
        }

        private void Form5_Load()
        {
            /*koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, " +
                "m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi From dbo.Mahhasiswa m " +
                "join dbo.Prodii p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.CustomerBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "nim", true));
            this.txtNama.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "nama_mahasiswa", true));
            this.txtAlamat.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "jenis_kel", true));
            this.dtTanggalLahir.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(new Binding("Text", this.CustomerBindingSource, "nama_prodi", true));
            koneksi.Close();*/
        }
        private void clearBinding()
        {
            this.txtNIM.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.cbxJenisKelamin.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }

        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
        }

        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select id_prodi, nama_prodi from dbo.Prodii";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = dt;
        }
    }
}
