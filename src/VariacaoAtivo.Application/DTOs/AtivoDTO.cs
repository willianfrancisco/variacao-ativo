namespace VariacaoAtivo.Application.DTOs
{
    public class AtivoDTO
    {
        public int Dia { get; set; }
        public string? DataCota { get; set; }
        public decimal Valor { get; set; }
        public string? VariacaoDum { get; set; }
        public string? VariacaoPrimeiraData { get; set; }
    }
}