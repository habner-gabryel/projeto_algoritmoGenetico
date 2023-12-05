using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Data;

public class Conexao
{
    private static readonly string Host = "localhost";
    private static readonly int Port = 3306;
    private static readonly string Database = "bd_proj_alggenetico";
    private static readonly string Username = "root";
    private static readonly string Password = "123123";

    private static MySqlConnection connection;

    public static MySqlCommand command;

    public Conexao()
    {
        try
        {
            connection = new($"server={Host};database={Database};port={Port};user={Username};password={Password}");
            connection.Open();

        } catch (MySqlException)
        {
            throw;
        }

    }

    public void AbreConexao()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
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
