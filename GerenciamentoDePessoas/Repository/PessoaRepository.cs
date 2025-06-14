using GerenciamentoDePessoas.Data;
using GerenciamentoDePessoas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDePessoas.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        //Na repository, o contexto é injetado via construtor, permitindo acesso ao banco de dados.
        private readonly GerenciamentoDePessoasContext _context;

        public PessoaRepository(GerenciamentoDePessoasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodasPessoasAsync()
        {
            var pessoas = await _context.Pessoas.ToListAsync();

            return pessoas;                
        }

        public async Task<Pessoa> BuscarPessoaPorIdAsync(int id)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pessoa == null)
                {
                    throw new Exception("Pessoa não encontrada.");
                }
                return pessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter pessoa por ID: " + ex.Message, ex);
            }            
        }

        public async Task<Pessoa> CriarPessoaAsync(Pessoa pessoa)
        {
            try
            {
                await _context.Pessoas.AddAsync(pessoa);
                await _context.SaveChangesAsync();

                return pessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar pessoa ao banco de dados: " + ex.Message, ex);
            }           
        }

        public async Task<Pessoa> EditarPessoaAsync(Pessoa pessoa)
        {
            try
            {                
                _context.Pessoas.Update(pessoa);
                await _context.SaveChangesAsync();

                return pessoa;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar pessoa no banco de dados: " + ex.Message, ex);
            }            
        }

        public async Task<Pessoa> ExcluirPessoaAsync(Pessoa pessoa)
        {
            var pessoaDb = await BuscarPessoaPorIdAsync(pessoa.Id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }

            return pessoaDb;
        }

        public async Task<bool> PessoaExisteAsync(string cpf)
        {
             var usuarioExistente = await _context.Pessoas.AnyAsync(p => p.CPF == cpf);
             return usuarioExistente;
        }

        /// <summary>
        /// Método para buscar o total de pessoas no banco de dados.
        /// </summary>
        /// <returns></returns>
        public Task<int> BuscarTotal()
        {
            return _context.Pessoas.CountAsync();
        }

        /// <summary>
        /// Método para buscar pessoas por termo de pesquisa (nome ou CPF).
        /// </summary>
        /// <param name="termo"></param>
        /// <returns></returns>
        public async Task<List<Pessoa>> BuscarPessoaTermoAsync(string termo)
        {
            var pessoaDb =  _context.Pessoas
                .Where(p => 
                    EF.Functions.Like(p.Nome, $"%{termo}%") || 
                    EF.Functions.Like(p.Sobrenome, $"%{termo}%") || 
                    EF.Functions.Like(p.CPF, $"%{termo}%"))
                .ToListAsync();  
            
            return await pessoaDb;
        }
    }
}
