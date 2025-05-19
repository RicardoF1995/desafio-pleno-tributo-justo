using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Business
{
    public class EstatisticasBusiness
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IItemNotaRepository _itemNotaRepository;
        
        public EstatisticasBusiness(
            IEmpresaRepository empresaRepository,
            INotaFiscalRepository notaFiscalRepository,
            IItemNotaRepository itemNotaRepository)
        {
            _empresaRepository = empresaRepository;
            _notaFiscalRepository = notaFiscalRepository;
            _itemNotaRepository = itemNotaRepository;
        }

        public async Task<KpiRelatorioDTO> RetornarEstatisticasAsync()
        {
            var empresas = await _empresaRepository.BuscarTodasEmpresasAsync();
            int totalEmpresas = empresas.Count;
            int totalNotas = 0;
            int totalItens = 0;
            double valorTotalNotas = 0;
            double totalImpostos = 0;

            foreach (var empresa in empresas)
            {
                var notas = await _notaFiscalRepository.BuscarNotasPorEmpresaAsync(empresa.Id);
                totalNotas += notas.Count;

                foreach (var nota in notas)
                {
                    var itens = await _itemNotaRepository.BuscarItensPorNotaFiscalAsync(nota.Id);
                    totalItens += itens.Count;
                    valorTotalNotas += itens.Sum(i => i.Quantidade * i.ValorUnitario);
                    totalImpostos += itens.Sum(i => i.ImpostoItem);
                }
            }

            return new KpiRelatorioDTO
            {
                TotalEmpresas = totalEmpresas,
                TotalNotasFiscais = totalNotas,
                TotalItens = totalItens,
                ValorTotalNotas = (decimal)valorTotalNotas,
                TotalImpostos = (decimal)totalImpostos
            };
        }
    }
}