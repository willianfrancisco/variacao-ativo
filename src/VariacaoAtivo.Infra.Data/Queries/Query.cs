namespace VariacaoAtivo.Infra.Data.Queries
{
    public static class Query
    {
        public static string QueryCota()
        {
            return "SELECT Dia, DataCota, Valor, VariacaoDum, VariacaoPrimeiraData FROM Cota";
        }
    }
}