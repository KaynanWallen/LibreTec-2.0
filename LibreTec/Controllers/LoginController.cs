using LibreTec.Models;
using LibreTec.Repositorio;
using LibreTec.Helper;
using Microsoft.AspNetCore.Mvc;

namespace LibreTec.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }


        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Painel");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public  IActionResult Logar(UsuarioModel usuario)
        {
            try
            {
                UsuarioModel usuarioDB =  _usuarioRepositorio.BuscarPorlogin(usuario.Login);
                if(usuarioDB != null)
                {
                    if(usuarioDB.Senha == usuario.Senha.GeradorDeHash())
                    {
                        _sessao.CriarSessaoDoUsuario(usuario);
                        return RedirectToAction("Index", "Painel");

                    }
                    //Return caso não ache o senha do usuario
                    return RedirectToAction("PaginaBiblioteca", "Painel");
                }
                //Return caso não ache o login do usuario
                return RedirectToAction("PaginaIncluir", "Painel");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, destalhe do erro: {erro.Message}";
                return RedirectToAction("PaginaRemover", "Painel");
            }
        }
    }
}
