using MySql.Data.MySqlClient;

public class ConexionBD
{
    public static MySqlConnection ObtenerConexion()
    {
        string cadenaConexion = "server=localhost; database=appdb; uid=root; pwd=;";
        return new MySqlConnection(cadenaConexion); // ❌ NO llames a .Open() aquí
    }
}
