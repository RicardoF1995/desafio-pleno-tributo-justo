namespace backend.DTOs
{
    public class UploadNotaCsvDTO
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public string CodigoItem { get; set; }
        public string DescricaoItem { get; set; }
        public double Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ImpostoItem { get; set; }
    }
}