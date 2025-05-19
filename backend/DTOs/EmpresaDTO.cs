namespace backend.DTOs
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public List<NotaFiscalDTO> LstNotasFiscais { get; set; } = new();
    }
}
