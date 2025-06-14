using GerenciamentoDePessoas.Models;
using GerenciamentoDePessoas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDePessoas.Controllers
{
    [Authorize]
    public class PessoaController : Controller
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        public async Task<ActionResult> Index()
        {
            var pessoas = await _pessoaService.BuscarTodasPessoasAsync();
            return View(pessoas);
        }

        /// <summary>
        /// Método para buscar o total de pessoas cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Total()
        {
            var totalPessoas = await _pessoaService.BuscarTotal();
            return Ok(totalPessoas);
        }

        /// <summary>
        /// Método para buscar pessoas por termo de pesquisa.
        /// </summary>
        /// <param name="termo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> BuscarPessoaTermo(string termo)
        {
            var pessoas = await _pessoaService.BuscarPessoaTermoAsync(termo);
            return Json(pessoas);
        }

        /// <summary>
        /// Método para exibir a página de adicionar uma nova pessoa - trás o formulário vazio.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        /// <summary>
        /// Método para adicionar uma nova pessoa ao sistema
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Criar(Pessoa pessoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _pessoaService.CriarPessoaAsync(pessoa);
                    TempData["MensagemSucesso"] = "Pessoa adicionada com sucesso!";
                    return RedirectToAction("Index", "Pessoa");
                }

                return RedirectToAction("Index", "Pessoa");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao adicionar pessoa: " + ex.Message;
                return View(pessoa);
            }
        }

        /// <summary>
        /// Método para exibir a página de edição de uma pessoa - trás o formulário vazio.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pessoa/Editar/{id}")]
        public async Task<ActionResult> Editar(int id)
        {
            try
            {
                if (id == 0)
                {
                    TempData["MensagemErroBusca"] = "Id não encontradoa!";
                    return RedirectToAction("Index", "Pessoa");
                }

                var pessoa = await _pessoaService.BuscarPessoaPorIdAsync(id);

                TempData["MensagemSucessoEditarBusca"] = "Pessoa encontrada com sucesso!";
                return View(pessoa);
            }
            catch (Exception)
            {
                TempData["MensagemErroBusca"] = "Pessoa não encontrada!";
                return RedirectToAction("Index", "Pessoa");
            }
        }

        /// <summary>
        /// Método post para editar uma pessoa existente no sistema.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Pessoa pessoa)
        {
            try
            {
                if (pessoa.Id == 0)
                {
                    throw new ArgumentException("ID inválido para edição.");
                }

                var pessoaParaEdicao = await _pessoaService.EditarPessoaAsync(pessoa);
                TempData["MensagemSucesso"] = "Pessoa atualizada com sucesso!";
                return RedirectToAction("Index", "Pessoa");

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao atualizar pessoa: " + ex.Message;
                return View();
            }

        }

        /// <summary>
        /// Método para exibir a página de exclusão de uma pessoa - trás o formulário vazio.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Excluir(int id)
        {
            var pessoa = await _pessoaService.BuscarPessoaPorIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        /// <summary>
        /// Método para excluir uma pessoa do sistema.
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(Pessoa pessoa)
        {
            try
            {
                var pessoaDb = await _pessoaService.BuscarPessoaPorIdAsync(pessoa.Id);
                if (pessoaDb == null)
                {
                    TempData["MensagemErro"] = "Pessoa não encontrada!";
                    return RedirectToAction("Index", "Pessoa");
                }
                await _pessoaService.ExcluirPessoaAsync(pessoaDb);
                TempData["MensagemSucesso"] = "Pessoa excluída com sucesso!";
                return RedirectToAction("Index", "Pessoa");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir pessoa: " + ex.Message;
                return View();
            }
        }
    }
}
