using Oracle.ManagedDataAccess.Client;
using ProductAPI.Domain.Entities;
using System.Data;
using Dapper;
using ProductAPI.Domain.Interfaces;

namespace ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new OracleConnection(_connectionString);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM TNIRPT.TEST_PRODUCTS";
            using var conn = Connection;
            return await conn.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM TNIRPT.TEST_PRODUCTS WHERE ID = :Id";
            using var conn = Connection;
            return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Product product)
        {
            var sql = @"
                    INSERT INTO TNIRPT.TEST_PRODUCTS (NAME, DESCRIPTION, PRICE, CREATED_AT) 
                    VALUES (:Name, :Description, :Price, SYSDATE) 
                    RETURNING ID INTO :Id";

            using var conn = Connection;
            var parameters = new DynamicParameters();
            parameters.Add("Name", product.Name);
            parameters.Add("Description", product.Description);
            parameters.Add("Price", product.Price);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conn.ExecuteAsync(sql, parameters);
            return parameters.Get<int>("Id");
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var sql = @"UPDATE TNIRPT.TEST_PRODUCTS SET NAME = :Name, DESCRIPTION = :Description, PRICE = :Price 
                    WHERE ID = :Id";
            using var conn = Connection;
            var rows = await conn.ExecuteAsync(sql, product);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM TNIRPT.TEST_PRODUCTS WHERE ID = :Id";
            using var conn = Connection;
            var rows = await conn.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }
    }
}
