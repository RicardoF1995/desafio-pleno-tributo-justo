using CsvHelper.Configuration;
using backend.DTOs;

namespace backend.Mapping
{
    public sealed class UploadNotaCsvDtoMap : ClassMap<UploadNotaCsvDTO>
    {
        public UploadNotaCsvDtoMap()
        {
            Map(m => m.Cnpj).Name("cnpj");
            Map(m => m.RazaoSocial).Name("razao_social");
            Map(m => m.NumeroNota).Name("numero_nota");
            Map(m => m.DataEmissao).Name("data_emissao");
            Map(m => m.CodigoItem).Name("codigo_item");
            Map(m => m.DescricaoItem).Name("descricao_item");
            Map(m => m.Quantidade).Name("quantidade");
            Map(m => m.ValorUnitario).Name("valor_unitario");
            Map(m => m.ImpostoItem).Name("imposto_item");
        }
    }
}
