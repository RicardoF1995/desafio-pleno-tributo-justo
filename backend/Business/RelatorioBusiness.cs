using backend.DTOs;
using backend.Repositories;

namespace backend.Business
{
    public class RelatoriosBusiness
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IItemNotaRepository _itemNotaRepository;

        public RelatoriosBusiness(
            IEmpresaRepository empresaRepository,
            INotaFiscalRepository notaFiscalRepository,
            IItemNotaRepository itemNotaRepository)
        {
            _empresaRepository = empresaRepository;
            _notaFiscalRepository = notaFiscalRepository;
            _itemNotaRepository = itemNotaRepository;
        }

        public async Task<List<EmpresaDTO>> RetornarRelatorioAsync()
        {
            var empresas = await _empresaRepository.BuscarTodasEmpresasAsync();
            var empresasDto = new List<EmpresaDTO>();

            foreach (var empresa in empresas)
            {
                var notas = await _notaFiscalRepository.BuscarNotasPorEmpresaAsync(empresa.Id);
                var notasDto = new List<NotaFiscalDTO>();

                foreach (var nota in notas)
                {
                    var itens = await _itemNotaRepository.BuscarItensPorNotaFiscalAsync(nota.Id);
                    var itensDto = itens.Select(i => new ItemNotaDTO
                    {
                        Id = i.Id,
                        CodigoItem = i.CodigoItem,
                        DescricaoItem = i.DescricaoItem,
                        Quantidade = i.Quantidade,
                        ValorUnitario = i.ValorUnitario,
                        ImpostoItem = i.ImpostoItem
                    }).ToList();

                    decimal totalNota = (decimal)itens.Sum(i => i.Quantidade * i.ValorUnitario);
                    decimal impostoRecolhido = (decimal)itens.Sum(i => i.ImpostoItem);
                    decimal diferenca = totalNota - impostoRecolhido;

                    notasDto.Add(new NotaFiscalDTO
                    {
                        Id = nota.Id,
                        NumeroNota = nota.NumeroNota,
                        DataEmissao = nota.DataEmissao,
                        ValorTotal = totalNota,
                        ImpostoRecolhido = impostoRecolhido,
                        Diferenca = diferenca,
                        LstItensNota = itensDto
                    });
                }

                empresasDto.Add(new EmpresaDTO
                {
                    Id = empresa.Id,
                    Cnpj = empresa.Cnpj,
                    RazaoSocial = empresa.RazaoSocial,
                    LstNotasFiscais = notasDto
                });
            }

            return empresasDto;
        }
    }
}
