using backend.DTOs;
using backend.Models;
using backend.Repositories;
using backend.Mapping;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace backend.Business
{
    public class UploadArquivoBusiness
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaRepository;
        private readonly IItemNotaRepository _itemRepository;

        public UploadArquivoBusiness(IEmpresaRepository empresaRepository,
                                     INotaFiscalRepository notaRepository,
                                     IItemNotaRepository itemRepository)
        {
            _empresaRepository = empresaRepository;
            _notaRepository = notaRepository;
            _itemRepository = itemRepository;
        }

        public async Task ProcessarArquivoCsvAsync(Stream fileStream)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower().Trim(),
                Delimiter = ";",
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<UploadNotaCsvDtoMap>();
            var registros = csv.GetRecords<UploadNotaCsvDTO>().ToList();

            var empresasAgrupadas = registros
                .GroupBy(r => r.Cnpj)
                .Select(e => new Empresa
                {
                    Cnpj = e.Key,
                    RazaoSocial = e.First().RazaoSocial,
                    LstNotasFiscais = e.GroupBy(r => r.NumeroNota)
                        .Select(n => new NotaFiscal
                        {
                            NumeroNota = n.Key,
                            DataEmissao = n.First().DataEmissao,
                            LstItensNota = n.Select(i => new ItemNota
                            {
                                CodigoItem = i.CodigoItem,
                                DescricaoItem = i.DescricaoItem,
                                Quantidade = i.Quantidade,
                                ValorUnitario = i.ValorUnitario,
                                ImpostoItem = i.ImpostoItem
                            }).ToList()
                        }).ToList()
                }).ToList();

            try
            {
                foreach (var empresa in empresasAgrupadas)
                {
                    var empresaExistente = await _empresaRepository.BuscarEmpresaPorCnpjAsync(empresa.Cnpj);
                    int empresaId;

                    if (empresaExistente == null)
                    {
                        empresaId = await _empresaRepository.InserirEmpresaAsync(empresa);
                    }
                    else
                    {
                        empresaId = empresaExistente.Id;
                    }

                    foreach (var nota in empresa.LstNotasFiscais)
                    {
                        int notaId = await _notaRepository.InserirNotaFiscalAsync(empresaId, nota);

                        foreach (var item in nota.LstItensNota)
                        {
                            await _itemRepository.InserirItemNotaAsync(item, notaId);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
