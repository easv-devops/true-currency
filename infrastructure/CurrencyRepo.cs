using System.Data.SqlTypes;
using Dapper;
using infrastructure.Models;
using MySqlConnector;

namespace Infrastructure;

public class CurrencyRepo
{
    private readonly string _connectionString;
    
    public CurrencyRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Currency> GetAllCurrencies()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string sql = @"
SELECT Iso, Currency AS Valuta FROM Currency;";

            try
            {
                connection.Open();
                var currencies = connection.Query<Currency>(sql);
                return currencies.ToList();
            }
            catch (Exception ex)
            {
                throw new SqlTypeException("Failed to fetch currencies", ex);
            }
        }
    }
}