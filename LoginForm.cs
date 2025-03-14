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

namespace COBAEM
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Properties.Resources.Bibli;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    MessageBox.Show("✅ Conexión exitosa.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error de conexión: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuarioCorrecto = "Admin";
            string contraseñaCorrecta = "COBAEM";

            string usuarioIngresado = txtUsuario.Text;
            string contraseñaIngresada = txtContraseña.Text;

            if (usuarioIngresado == usuarioCorrecto && contraseñaIngresada == contraseñaCorrecta)
            {
                MessageBox.Show("¡Bienvenido, " + usuarioIngresado + "!", "Acceso Correcto");
                this.Hide();

                MainForm ventanaPrincipal = new MainForm(usuarioIngresado);
                ventanaPrincipal.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
