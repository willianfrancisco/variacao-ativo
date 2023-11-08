using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using VariacaoAtivo.Domain.Entities;
using VariacaoAtivo.Domain.Interfaces;
using VariacaoAtivo.Infra.Data.Queries;
using Z.Dapper.Plus;

namespace VariacaoAtivo.Infra.Data.Repositories
{
    public class AtivoRepository : IAtivoRepository
    {
        private readonly string _connectionString;

        public AtivoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AtivoConnectionString");
        }

        public async Task AdicionarAtivos(IEnumerable<Ativo> ativo)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.BulkActionAsync(x => x.BulkInsert(ativo));
        }

        public async Task<IEnumerable<Ativo>> PesquisarAtivos()
        {
            string query = Query.QueryCota();
            using SqlConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Ativo>(query);
        }
    }
}