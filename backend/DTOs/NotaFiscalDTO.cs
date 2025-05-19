namespace backend.DTOs
{
    public class NotaFiscalDTO
    {
        public int Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public List<ItemNotaDTO> LstItensNota { get; set; } = new();

        //Calculados via regras de negócio
        public double ValorTotal => LstItensNota.Sum(i => i.Quantidade * i.ValorUnitario);
        public double ImpostoRecolhido => LstItensNota.Sum(i => i.ImpostoItem);
    }
}
