using System.Data.SqlTypes;
using Dapper;
using infrastructure.Models;
using MySqlConnector;

namespace infrastructure;

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
SELECT iso, value AS `RateToUsd` FROM Currency;";
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
    
    
    
    public int AddHistory(HistoryDto historyRecord)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string sql = @"
INSERT INTO History (Date, Source, Target, Value, Result)
VALUES (@Date, @Source, @Target, @Value, @Result);
SELECT LAST_INSERT_ID();";

            try
            {
                connection.Open();
                int insertedId = connection.QueryFirstOrDefault<int>(sql, historyRecord);
                return insertedId;
            }
            catch (Exception ex)
            {
                throw new SqlTypeException("Failed to add history record", ex);
            }
        }
    }
    public List<HistoryDto> GetAllHistory()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            const string sql = "SELECT Date, Source, Target, Value, Result FROM History";
            try
            {
                connection.Open();
                var history = connection.Query<HistoryDto>(sql).ToList();
                return history;
            }
            catch (Exception ex)
            {
                throw new SqlTypeException("Failed to fetch history", ex);
            }
        }
    }
    
}