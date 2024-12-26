using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNowDemo.Repositories
{
    public class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString => _connectionString;
    }

    public static class RepositoryExtensions
    {
        public static object ValueOrDbNull(this decimal? d)
        {
            return !d.HasValue ? DBNull.Value : d.Value;
        }

        public static object ValueOrDbNull(this int? d)
        {
            return !d.HasValue ? DBNull.Value : d.Value;
        }

        public static decimal? GetDecimalOrNull(this MySqlDataReader reader, int i)
        {
            return reader[i] == DBNull.Value ? null : (decimal)reader[i];
        }

        public static int? GetInt32OrNull(this MySqlDataReader reader, int i)
        {
            return reader[i] == DBNull.Value ? null : (int)reader[i];
        }
    }
}
