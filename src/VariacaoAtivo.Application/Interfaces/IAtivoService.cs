using VariacaoAtivo.Application.DTOs;

namespace VariacaoAtivo.Application.Interfaces
{
    public interface IAtivoService
    {
        Task<IEnumerable<AtivoDTO>> PesquisarAtivos(string ativo);
    }
}