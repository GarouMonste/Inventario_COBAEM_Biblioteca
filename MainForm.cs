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
    public partial class MainForm: Form
    {
        string usuarioLogueado;
        public MainForm(string nombre)
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.COBA;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.StartPosition = FormStartPosition.CenterScreen;
            usuarioLogueado = nombre;
            this.Text = "Panel Principal - Bienvenido " + nombre;
            CargarDatos();
        }

        private void CargarDatos()
        {
    try
    {
        using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
        {
                    string query = "SELECT * FROM tu_tabla ORDER BY Materia, Num_Inventario";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            dgvDatos.DataSource = tabla;
         }
         }
         catch (Exception ex)
         {
        MessageBox.Show("Error al cargar los datos: " + ex.Message);
         }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvDatos.Columns[e.ColumnIndex].Name == "Materia")
            {
                string materia = dgvDatos.Rows[e.RowIndex].Cells["Materia"].Value.ToString();

                if (materia == "Matemáticas")
                    dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                else if (materia == "Ciencias")
                    dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (materia == "Historia")
                    dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                else
                    dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }

            void CargarDatos()
            {
                try
                {
                    using (MySqlConnection conexion = ConexionBD.ObtenerConexion())
                    {
                        if (conexion.State == ConnectionState.Closed)
                        {
                            conexion.Open();
                        }

                        string query = "SELECT * FROM tu_tabla";

                        MySqlCommand cmd = new MySqlCommand(query, conexion);

                        MySqlDataAdapter adaptador = new MySqlDataAdapter(cmd);

                        DataTable tabla = new DataTable();

                        adaptador.Fill(tabla);

                        dgvDatos.DataSource = tabla;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error de MySQL: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error general: " + ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    if (conexion.State == System.Data.ConnectionState.Closed)
                    {
                        conexion.Open();
                    }

                    DataGridViewRow fila = dgvDatos.CurrentRow;

                    if (fila != null)
                    {
                        string query = "INSERT INTO tu_tabla (Num_Inventario, Titulo, Autor, Editorial, Edicion, Cantidad, Lugar, Fecha_Edicion, Num_Paginas, Clave_Campo, Clave_Autor, Materia, Observaciones, Ejemplar) " +
                                       "VALUES (@inventario, @titulo, @autor, @editorial, @edicion, @cantidad, @lugar, @fecha, @paginas, @campo, @claveAutor, @materia, @observaciones, @ejemplar)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@inventario", fila.Cells["Num_Inventario"].Value.ToString());
                            cmd.Parameters.AddWithValue("@titulo", fila.Cells["Titulo"].Value.ToString());
                            cmd.Parameters.AddWithValue("@autor", fila.Cells["Autor"].Value.ToString());
                            cmd.Parameters.AddWithValue("@editorial", fila.Cells["Editorial"].Value.ToString());
                            cmd.Parameters.AddWithValue("@edicion", fila.Cells["Edicion"].Value.ToString());
                            cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(fila.Cells["Cantidad"].Value));
                            cmd.Parameters.AddWithValue("@lugar", fila.Cells["Lugar"].Value.ToString());
                            cmd.Parameters.AddWithValue("@fecha", Convert.ToInt32(fila.Cells["Fecha_Edicion"].Value));
                            cmd.Parameters.AddWithValue("@paginas", Convert.ToInt32(fila.Cells["Num_Paginas"].Value));
                            cmd.Parameters.AddWithValue("@campo", fila.Cells["Clave_Campo"].Value.ToString());
                            cmd.Parameters.AddWithValue("@claveAutor", fila.Cells["Clave_Autor"].Value.ToString());
                            cmd.Parameters.AddWithValue("@materia", fila.Cells["Materia"].Value.ToString());
                            cmd.Parameters.AddWithValue("@observaciones", fila.Cells["Observaciones"].Value.ToString());
                            cmd.Parameters.AddWithValue("@ejemplar", fila.Cells["Ejemplar"].Value.ToString());

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Datos agregados correctamente.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos en la fila seleccionada.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar los datos: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    if (conexion.State == System.Data.ConnectionState.Closed)
                    {
                        conexion.Open();
                    }

                    DataGridViewRow fila = dgvDatos.CurrentRow;

                    if (fila != null)
                    {
                        string query = "UPDATE tu_tabla SET " +
                                       "Num_Inventario = @inventario, " +
                                       "Titulo = @titulo, " +
                                       "Autor = @autor, " +
                                       "Editorial = @editorial, " +
                                       "Edicion = @edicion, " +
                                       "Cantidad = @cantidad, " +
                                       "Lugar = @lugar, " +
                                       "Fecha_Edicion = @fecha, " +
                                       "Num_Paginas = @paginas, " +
                                       "Clave_Campo = @campo, " +
                                       "Clave_Autor = @claveAutor, " +
                                       "Materia = @materia, " +
                                       "Observaciones = @observaciones, " +
                                       "Ejemplar = @ejemplar " +
                                       "WHERE id = @id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(fila.Cells["id"].Value ?? 0));
                            cmd.Parameters.AddWithValue("@inventario", fila.Cells["Num_Inventario"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@titulo", fila.Cells["Titulo"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@autor", fila.Cells["Autor"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@editorial", fila.Cells["Editorial"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@edicion", fila.Cells["Edicion"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(fila.Cells["Cantidad"].Value ?? 0));
                            cmd.Parameters.AddWithValue("@lugar", fila.Cells["Lugar"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@fecha", Convert.ToInt32(fila.Cells["Fecha_Edicion"].Value ?? 0));
                            cmd.Parameters.AddWithValue("@paginas", Convert.ToInt32(fila.Cells["Num_Paginas"].Value ?? 0));
                            cmd.Parameters.AddWithValue("@campo", fila.Cells["Clave_Campo"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@claveAutor", fila.Cells["Clave_Autor"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@materia", fila.Cells["Materia"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@observaciones", fila.Cells["Observaciones"].Value?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@ejemplar", fila.Cells["Ejemplar"].Value?.ToString() ?? "");

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Datos modificados correctamente.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona una fila para modificar.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar los datos: " + ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0) // Verifica que haya una fila seleccionada
            {
                object valorID = dgvDatos.SelectedRows[0].Cells["id"].Value;

                // Verifica que el ID no sea nulo o DBNull
                if (valorID != null && valorID != DBNull.Value)
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de que quieres eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(valorID); // Convierte el ID

                            using (var conexion = ConexionBD.ObtenerConexion())
                            {
                                if (conexion.State == System.Data.ConnectionState.Closed)
                                {
                                    conexion.Open();
                                }

                                string query = "DELETE FROM tu_tabla WHERE id = @id";

                                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                                {
                                    cmd.Parameters.AddWithValue("@id", id);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Registro eliminado correctamente.");
                            CargarDatos(); // Recargar los datos en la tabla
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar los datos: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error: El ID seleccionado es inválido o está vacío.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro para eliminar.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    if (conexion.State == System.Data.ConnectionState.Closed)
                    {
                        conexion.Open();
                    }

                    string filtro = txtBuscar.Text.Trim();

                    if (string.IsNullOrEmpty(filtro))
                    {
                        MessageBox.Show("Por favor, ingresa un valor para buscar.");
                        return;
                    }

                    string query = "SELECT * FROM tu_tabla WHERE Num_Inventario LIKE @filtro OR Materia LIKE @filtro";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                        DataTable dt = new DataTable();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);

                        dgvDatos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los datos: " + ex.Message);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarDatos();
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
