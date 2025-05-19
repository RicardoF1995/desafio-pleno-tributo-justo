namespace backend.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public ICollection<ItemNota> LstItensNota { get; set; } = new List<ItemNota>();
    }
}
