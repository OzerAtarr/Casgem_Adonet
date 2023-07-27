using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Casgem_Adonet
{
    public partial class FrmLogin : Form
    {
        private Random random = new Random();

        public FrmLogin()
        {
            InitializeComponent();
            ChangeBackgroundColor(); // İlk başlangıçta arka plan rengini değiştir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-FCHRJ42\\SQLEXPRESS;Initial Catalog=CasgemDbMovie;Integrated Security=True");

            connection.Open();
            SqlCommand command = new SqlCommand("Select * From TblAdmin Where UserName=@p1 and Password=@p2", connection);
            command.Parameters.AddWithValue("@p1", textBox1.Text);
            command.Parameters.AddWithValue("@p2", textBox2.Text);
            SqlDataReader dr = command.ExecuteReader(); 
            if (dr.Read())
            {
                Form1 frm = new Form1();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre girdiniz!");
            }
            connection.Close();
            // Burada giriş işlemleri yapılabilir
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            #region
            // Timer ayarları
            Timer timer = new Timer();
            timer.Interval = 10000; // Değiştirmek istediğiniz saniye aralığı (1 saniye)
            timer.Tick += Timer_Tick;
            timer.Start();
            #endregion
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Timer tetiklendiğinde arka plan rengini değiştir
            ChangeBackgroundColor();
        }

        private void ChangeBackgroundColor()
        {
            // Rastgele renk oluştur ve formun arka planına ata
            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            this.BackColor = randomColor;
        }
    }
}
