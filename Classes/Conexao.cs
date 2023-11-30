using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Data;

public class Conexao
{
    public static string Host = "localhost";
    public static int Port = 3306;
    public static string Database = "bd_proj_alggenetico";
    public static string Username = "root";
    public static string Password = "123123";

    private static MySqlConnection connection;

    public static MySqlCommand command;

    public Conexao()
    {
        try
        {
            connection = new MySqlConnection($"server={Host};database={Database};port={Port};user={Username};password={Password}");
            connection.Open();

        } catch (MySqlException)
        {
            throw;
        }

    }

    public void Close()
    {
        connection.Close();
    }


    public MySqlCommand Query()
    {
        try
        {
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            return command;
        } catch (MySqlException e) {
            throw e;
        }
    }
}
