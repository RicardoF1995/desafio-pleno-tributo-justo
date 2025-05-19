namespace backend.DTOs
{
    public class NotaFiscalDTO
    {
        public int Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public List<ItemNotaDTO> LstItensNota { get; set; } = new();

        public decimal ValorTotal { get; set; }
        public decimal ImpostoRecolhido { get; set; }
        public decimal Diferenca { get; set; }
        public decimal DiferencaPercentual { get; set; }
    }
}
