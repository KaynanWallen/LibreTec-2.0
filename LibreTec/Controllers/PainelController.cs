using LibreTec.Models;
using LibreTec.Repositorio;
using LibreTec.Filters;
using LibreTec.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibreTec.Controllers
{
    [PaginaParaUsuarioLogado]
    public class PainelController : Controller
    {

        private readonly ILivroRepositorio _livroRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public PainelController(ILivroRepositorio livroRepositorio, IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _livroRepositorio = livroRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaginaIncluir()
        {
            return View();
        }

        public IActionResult PaginaEditar(string id)
        {
            LivroModel livro = _livroRepositorio.BuscarUmLivro(id);
            return View(livro);
        }

        public IActionResult PaginaEmprestar()
        {
            return View();
        }

        public IActionResult PaginaDevolver()
        {
            return View();
        }

        public IActionResult PaginaRemover()
        {
            return View();
        }

        public IActionResult PaginaSettings()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.Buscartodos();
            return View(usuarios);
        }

        public IActionResult CriarUsuario()
        {
            return View();
        }

        public IActionResult PaginaBiblioteca()
        {
            List<LivroModel> livros = _livroRepositorio.BuscarTodosLivrosSemTask();
            return View(livros);
        }





    }
}