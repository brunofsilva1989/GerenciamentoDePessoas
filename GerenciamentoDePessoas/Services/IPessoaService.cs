using GerenciamentoDePessoas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePessoas.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> BuscarTodasPessoasAsync();
        Task<int> BuscarTotal();
        Task<Pessoa> CriarPessoaAsync(Pessoa pessoa);
        Task<Pessoa> BuscarPessoaPorIdAsync(int id);
        Task<Pessoa> EditarPessoaAsync(Pessoa pessoa);
        Task<Pessoa> ExcluirPessoaAsync(Pessoa pessoa);
        Task<List<string>> BuscarPessoaTermoAsync(string termo);
    }
}
