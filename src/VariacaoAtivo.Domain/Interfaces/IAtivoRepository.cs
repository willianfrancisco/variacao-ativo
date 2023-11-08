using VariacaoAtivo.Domain.Entities;

namespace VariacaoAtivo.Domain.Interfaces
{
    public interface IAtivoRepository
    {
        Task AdicionarAtivos(Ativo ativo);
        Task<IEnumerable<Ativo>> PesquisarAtivos();
    }
}