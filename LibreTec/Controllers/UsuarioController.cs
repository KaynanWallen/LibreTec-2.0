using LibreTec.Models;
using LibreTec.Repositorio;
using LibreTec.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibreTec.Controllers
{
    [PaginaParaUsuarioLogado]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult ApagarUsuario(string id)
        {
            try
            {
                _usuarioRepositorio.ApagarUsuarioDB(id);
                return RedirectToAction("paginaSettings", "Painel");
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPost]
        public IActionResult PostUsuario(UsuarioModel usuario)
        {
            try
            {
                _usuarioRepositorio.AdicionarUsuario(usuario);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index", "Painel");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, destalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Painel");
            }
        }
    }
}
