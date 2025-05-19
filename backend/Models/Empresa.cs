namespace backend.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public ICollection<NotaFiscal> LstNotasFiscais { get; set; } = new List<NotaFiscal>();
    }
}
