namespace backend.DTOs
{
    public class KpiRelatorioDTO
    {
        public int TotalEmpresas { get; set; }
        public int TotalNotasFiscais { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotalNotas { get; set; }
        public decimal TotalImpostos { get; set; }
    }
}
