using GerenciamentoDePessoas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePessoas.Repository
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> ObterTodasPessoasAsync();
        Task<int> BuscarTotal();
        Task<Pessoa> BuscarPessoaPorIdAsync(int id);
        Task<Pessoa> CriarPessoaAsync(Pessoa pessoa);
        Task<Pessoa> EditarPessoaAsync(Pessoa pessoa);
        Task<bool> PessoaExisteAsync(string cpf);
        Task<Pessoa> ExcluirPessoaAsync(Pessoa pessoa);
        Task<List<Pessoa>> BuscarPessoaTermoAsync(string termo);
    }
}
