namespace infrastructure;
public static class Utilities
{
   
    public static readonly string
        MySqlConnectionString = Environment.GetEnvironmentVariable("currency-conn");
}