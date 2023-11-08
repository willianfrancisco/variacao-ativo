using AutoMapper;
using VariacaoAtivo.Application.DTOs;
using VariacaoAtivo.Domain.Entities;

namespace VariacaoAtivo.Application.Mappings
{
    public class MapeamentoEntidadeParaDTO : Profile
    {
        public MapeamentoEntidadeParaDTO()
        {
            CreateMap<Ativo, AtivoDTO>().ReverseMap();
        }
    }
}