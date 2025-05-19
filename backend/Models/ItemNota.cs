namespace backend.Models
{
    public class ItemNota
    {
        public int Id { get; set; }
        public string CodigoItem { get; set; }
        public string DescricaoItem { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ImpostoItem { get; set; }
    }
}
