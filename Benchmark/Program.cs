using System.Data.SqlClient;

public class Program
{
    static void Main()
    {
        string connectionStringWithPooling = "Server=37.27.179.21\\SQLEXPRESS22, 1433;Database=erp;User Id=sa;Password=itsteatime-123;Pooling=true;";
        string connectionStringWithDefault = "Server=37.27.179.21\\SQLEXPRESS22, 1433;Database=erp;User Id=sa;Password=itsteatime-123;";

        string query = "SELECT TOP 50 * FROM shoes";

        Console.WriteLine("\nStart timing the non-pooling SqlConnection version...");
        var startTime = DateTime.Now;

        for (int i = 0; i < 1000; i++)
        {
            using (var connection = new SqlConnection(connectionStringWithDefault))
            {
                connection.Open();
            }
        }

        var endTime = DateTime.Now;
        Console.WriteLine("Time spent (without pooling): " + (endTime - startTime).TotalMilliseconds + " ms");

        Console.WriteLine("\nStart timing the pooling SqlConnection version...");
        startTime = DateTime.Now;

        for (int i = 0; i < 1000; i++)
        {
            using (var connection = new SqlConnection(connectionStringWithPooling))
            {
                connection.Open();
            }
        }

        endTime = DateTime.Now;
        Console.WriteLine("Time spent (with pooling): " + (endTime - startTime).TotalMilliseconds + " ms");
    }
}

