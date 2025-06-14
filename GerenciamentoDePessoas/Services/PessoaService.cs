
using GerenciamentoDePessoas.Models;
using GerenciamentoDePessoas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePessoas.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        /// <summary>
        /// Mtodo da Service para adicionar uma nova pessoa ao sistema.
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Pessoa> CriarPessoaAsync(Pessoa pessoa)
        {
            var pessoaExistente = await _pessoaRepository.PessoaExisteAsync(pessoa.CPF);
            
            if (pessoaExistente)
            {
                throw new Exception("Pessoa já existe.");
            }
           
            var usuarioCriado = await _pessoaRepository.CriarPessoaAsync(pessoa);

            return usuarioCriado;
        }

        /// <summary>
        /// Método da Service para atualizar uma pessoa no sistema.
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Pessoa> EditarPessoaAsync(Pessoa pessoa)
        {
            await _pessoaRepository.BuscarPessoaPorIdAsync(pessoa.Id);

            return await _pessoaRepository.EditarPessoaAsync(pessoa);
        }

        /// <summary>
        /// Método da Service para excluir uma pessoa do sistema.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pessoa> ExcluirPessoaAsync(Pessoa pessoa)
        {
            await _pessoaRepository.BuscarPessoaPorIdAsync(pessoa.Id);

            return await _pessoaRepository.ExcluirPessoaAsync(pessoa);
        }

        /// <summary>
        /// Método da Service para obter uma pessoa pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task<Pessoa> BuscarPessoaPorIdAsync(int id)
        {
            var pessoa = _pessoaRepository.BuscarPessoaPorIdAsync(id);
            if (pessoa == null)
            {
                throw new Exception("Pessoa não encontrada.");
            }
            return pessoa;
        }

        /// <summary>
        /// Método da Service para obter todas as pessoas do sistema.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task<IEnumerable<Pessoa>> BuscarTodasPessoasAsync()
        {
            var pessoas = _pessoaRepository.ObterTodasPessoasAsync();
            if (pessoas == null)
            {
                throw new Exception("Nenhuma pessoa encontrada.");
            }
            return pessoas;
        }

        /// <summary>
        /// Método da Service para buscar o total de pessoas cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<int> BuscarTotal()
        {            
            return await _pessoaRepository.BuscarTotal();
        }

        /// <summary>
        /// Método da Service para buscar pessoas por termo.
        /// </summary>
        /// <param name="termo"></param>
        /// <returns></returns>
        public async Task<List<string>> BuscarPessoaTermoAsync(string termo) 
        { 
            var resultadoBusca =  await _pessoaRepository.BuscarPessoaTermoAsync(termo);
            var nomeCompleto = new List<string>();

            foreach (var item in resultadoBusca)
            {
                nomeCompleto.Add($"{item.Nome} {item.Sobrenome}");
            }

            return nomeCompleto;
        }
    }
}
