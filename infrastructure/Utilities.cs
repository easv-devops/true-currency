﻿namespace infrastructure;
public class Utilities
{
    private static readonly Uri Uri = new Uri(Environment.GetEnvironmentVariable("pgconn")!);
    
    public static readonly string
        ProperlyFormattedConnectionString = string.Format(
            "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3",
            Uri.Host,
            Uri.AbsolutePath.Trim('/'),
            Uri.UserInfo.Split(':')[0],
            Uri.UserInfo.Split(':')[1],
            Uri.Port > 0 ? Uri.Port : 5432);
    
    public static readonly string
        MySqlConnectionString = "Server=4.231.252.47;Database=currencies;Uid=myuser;Pwd=mypassword;";
}