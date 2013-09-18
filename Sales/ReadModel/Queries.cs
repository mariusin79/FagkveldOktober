using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Sales.ReadModel
{
    public class Queries : IQueries
    {
        private readonly IConnectionStrings _connectionStrings;

        public Queries(IConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public IEnumerable<RmAvailableBook> FindBooksAvailableForSale()
        {
            using (IDbConnection connection = new SqlConnection(_connectionStrings.SalesDb))
            {
                return connection.Query<RmAvailableBook>(@"SELECT * FROM Book");
            }
        }
    }
}