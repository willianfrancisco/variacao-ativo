using AutoMapper;
using Newtonsoft.Json.Linq;
using VariacaoAtivo.Application.DTOs;
using VariacaoAtivo.Application.Interfaces;
using VariacaoAtivo.Domain.Entities;
using VariacaoAtivo.Domain.Interfaces;

namespace VariacaoAtivo.Application.Services
{
    public class AtivoService : IAtivoService
    {
        private readonly IAtivoRepository _ativoRepository;
        private readonly IMapper _mapper;

        public AtivoService(IAtivoRepository ativoRepository, IMapper mapper)
        {
            _ativoRepository = ativoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AtivoDTO>> PesquisarAtivos(string ativo = "PETR4.SA")
        {
            // Consultar Api yahoo para recuperar os ativos
            // Passar os valore para a entidade Ativo.Valor
            // Montar o dia e data da cota
            // Calcular os valores da variacao D - 1
            // Calcular os valores da variacao em relacao a primeira data
            // Gravar os dados via bulkinsert no banco
            IEnumerable<AtivoDTO> ativosMontados = await MontaDadosAtivo(ativo);
            IEnumerable<Ativo> ativos = _mapper.Map<IEnumerable<Ativo>>(ativosMontados);
            await _ativoRepository.AdicionarAtivos(ativos);
            return _mapper.Map<IEnumerable<AtivoDTO>>(_ativoRepository.PesquisarAtivos());
        }

        private async Task<IEnumerable<AtivoDTO>> MontaDadosAtivo(string ativo)
        {
            IEnumerable<decimal> valores = await ObterValorAtivos(ativo);
            List<AtivoDTO> ativos = new List<AtivoDTO>();

            for(int i = 0; i < valores.Count(); i++)
            {
                if(i == 0)
                {
                    AtivoDTO ativoDTO = new AtivoDTO
                    {
                        Dia = i + 1,
                        DataCota = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).ToString("dd/MM/yyyy"),
                        Valor = Decimal.Round(valores.ElementAt(i), MidpointRounding.AwayFromZero),
                        VariacaoDum = "-",
                        VariacaoPrimeiraData = "-"
                    };
                    ativos.Add(ativoDTO);
                }else
                {
                    decimal variacaDmaisUm = (Decimal.Round(valores.ElementAt(i), MidpointRounding.AwayFromZero) - Decimal.Round(valores.ElementAt(i -1), MidpointRounding.AwayFromZero)/Decimal.Round(valores.ElementAt(i -1), MidpointRounding.AwayFromZero)*100);
                    decimal variacaoPrimeiraData = (Decimal.Round(valores.ElementAt(i), MidpointRounding.AwayFromZero) - Decimal.Round(valores.ElementAt(0), MidpointRounding.AwayFromZero)/Decimal.Round(valores.ElementAt(0), MidpointRounding.AwayFromZero)*100);
                    AtivoDTO ativoDTO = new AtivoDTO
                    {
                        Dia = i + 1,
                        DataCota = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day - i).ToString("dd/MM/yyyy"),
                        Valor = Decimal.Round(valores.ElementAt(i), MidpointRounding.AwayFromZero),
                        VariacaoDum = variacaDmaisUm.ToString("F"),
                        VariacaoPrimeiraData = variacaoPrimeiraData.ToString("F")
                    };
                    ativos.Add(ativoDTO);
                }
            }

            return ativos;
        }

        private async Task<IEnumerable<decimal>> ObterValorAtivos(string ativo)
        {
            string url = $"https://query2.finance.yahoo.com/v8/finance/chart/{ativo}";
            using HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Analise os dados JSON
                    var data = JObject.Parse(json);

                    // Acesse os dados do gráfico
                    var chartData = data["chart"]["result"][0];

                    // Acesse as séries temporais
                    var timestamps = chartData["timestamp"];
                    var openPrices = chartData["indicators"]["quote"][0]["open"];

                    // Filtrar os dados para os últimos 30 dias
                    int totalDays = 30;
                    int dataCount = timestamps.Count();
                    int startIndex = Math.Max(dataCount - totalDays, 0);

                    List<decimal> values = new List<decimal>();
                    for (int i = 0; i < timestamps.Skip(startIndex).Count(); i++)
                    {
                        values.Add(openPrices[i].Value<decimal>());
                    }

                    return values;
                }
                else
                {
                    throw new Exception(message:$"Erro ao consultar a API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(message:$"Erro ao consultar a API: {ex.Message}");
            }

        }
    }
}