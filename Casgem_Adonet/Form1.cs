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

namespace Casgem_Adonet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-FCHRJ42\\SQLEXPRESS;Initial Catalog=CasgemDbMovie;Integrated Security=True");

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable da = new DataTable();
            adapter.Fill(da);
            dtgCategory.DataSource = da;

        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into TblCategory (CategoryName) values (@p1)", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCategory where CategoryID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
        }

        private void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblCategory Set CategoryName=@p1 where CategoryID=@p2", connection);
            command.Parameters.AddWithValue("@p1", txtCategoryName.Text);
            command.Parameters.AddWithValue("@p2", txtCategoryID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region
            connection.Open();
            SqlCommand command = new SqlCommand("Select Count(*) From TblCategory", connection);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblCategoryCount.Text = dr[0].ToString();
            }
            connection.Close();

            #endregion

            #region
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select MovieName From TblMovie Where MovieImdb=(Select Max(MovieImdb) From TblMovie)", connection);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                lblBestMovie.Text = dr2[0].ToString();
            }
            connection.Close();
            #endregion

            #region
            connection.Open();
            SqlCommand command3 = new SqlCommand("Select * From TblCategory", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command3);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DataSource = dt;
            connection.Close();
            #endregion


        }

        private void btnMovieList_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select MovieName , MovieImdb , MovieDuration , CategoryName From TblMovie Inner Join TblCategory On TblMovie.MovieCategory=TblCategory.CategoryID", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);
            dtgMovie.DataSource = dt;
            connection.Close();
        }

        private void btnMovieAdd_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("insert into TblMovie (MovieName,MovieImdb,MovieDuration,MovieCategory) values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMovieImdb.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.ExecuteNonQuery();
            MessageBox.Show("Film Başarılı Bir Şekilde Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            connection.Close() ;

        }

        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete From TblMovie Where MovieID=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtMovieID.Text);
            command.ExecuteNonQuery ();
            MessageBox.Show("Film Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update TblMovie Set MovieName=@p1,MovieImdb=@p2,MovieDuration=@p3,MovieCategory=@p4 where MovieID=@p5", connection);
            command.Parameters.AddWithValue("@p1", txtMovieName.Text);
            command.Parameters.AddWithValue("@p2", txtMovieImdb.Text);
            command.Parameters.AddWithValue("@p3", txtMovieTime.Text);
            command.Parameters.AddWithValue("@p4", cmbCategory.SelectedValue);
            command.Parameters.AddWithValue("@p5", txtMovieID.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Film Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            connection.Close();

        }
    }
}
