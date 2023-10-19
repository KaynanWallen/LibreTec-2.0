using LibreTec.Models;
using LibreTec.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;


namespace LibreTec.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroRepositorio _livroRepositorio;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LivroController(ILivroRepositorio livroRepositorio, IWebHostEnvironment hostingEnvironment)
        {
            _livroRepositorio = livroRepositorio;
            _hostingEnvironment = hostingEnvironment;
        }






        //Código para pegar os livros do excel
        [HttpGet]
        public async Task<ActionResult> AdicionarTodosLivrosExcel()
        {
            try
            {
                string pathToExcelFile = Path.Combine(_hostingEnvironment.ContentRootPath, "Data", "excel.xlsx");
                Workbook wb = new Workbook(pathToExcelFile);


                // Obter todas as planilhas
                WorksheetCollection collection = wb.Worksheets;

                // Crie uma lista para armazenar os objetos
                List<LivroModel> livros = new List<LivroModel>();
                List<Dictionary<string, object>> dados = new List<Dictionary<string, object>>();

                // Percorra todas as planilhas
                for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
                {
                    // Obter planilha usando seu índice
                    Worksheet worksheet = collection[worksheetIndex];

                    // Imprimir nome da planilha
                    Console.WriteLine("Worksheet: " + worksheet.Name);

                    // Obter número de linhas e colunas
                    int rows = worksheet.Cells.MaxDataRow;
                    int cols = worksheet.Cells.MaxDataColumn;

                    // Encontre os índices das colunas desejadas

                    // Percorrer as linhas
                    for (int i = 1; i <= rows; i++) // Comece a partir da segunda linha (índice 1)
                    {
                        // Crie um objeto para armazenar os valores desta linha
                        LivroModel livro = new LivroModel();

                        livro.Tombo_Atual = worksheet.Cells[i, 0].Value != null ? worksheet.Cells[i, 0].Value.ToString() : "";
                        livro.Tombo_Antigo = worksheet.Cells[i, 01].Value != null ? worksheet.Cells[i, 1].Value.ToString() : "";
                        livro.Autor = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : "";
                        livro.Titulo = worksheet.Cells[i, 4].Value != null ? worksheet.Cells[i, 4].Value.ToString() : "";
                        livro.Data = worksheet.Cells[i, 5].Value != null ? worksheet.Cells[i, 5].Value.ToString() : "";
                        livro.Editora = worksheet.Cells[i, 6].Value != null ? worksheet.Cells[i, 6].Value.ToString() : "";
                        livro.Local = worksheet.Cells[i, 7].Value != null ? worksheet.Cells[i, 7].Value.ToString() : "";
                        livro.Ano = worksheet.Cells[i, 8].Value != null ? worksheet.Cells[i, 8].Value.ToString() : "";
                        livro.Aquisicao = worksheet.Cells[i, 9].Value != null ? worksheet.Cells[i, 9].Value.ToString() : "";
                        livro.Cod_Barra = worksheet.Cells[i, 10].Value != null ? worksheet.Cells[i, 10].Value.ToString() : "";
                        livro.Genero = worksheet.Cells[i, 11].Value != null ? worksheet.Cells[i, 11].Value.ToString() : "";
                        livro.Cod_Classe = worksheet.Cells[i, 12].Value != null ? worksheet.Cells[i, 12].Value.ToString() : "";
                        livro.Arquivo = worksheet.Cells[i, 13].Value != null ? worksheet.Cells[i, 13].Value.ToString() : "";
                        livro.Clutter = worksheet.Cells[i, 14].Value != null ? worksheet.Cells[i, 14].Value.ToString() : "";
                        livro.Palavra_Chave = worksheet.Cells[i, 15].Value != null ? worksheet.Cells[i, 15].Value.ToString() : "";

                        if (livro.Emprestado == null)
                        {
                            livro.Emprestado = new LivroModel.InformacaoEmprestado();
                        }

                        var VerificarEmprestimo = worksheet.Cells[i, 16].Value != null ? worksheet.Cells[i, 16].Value.ToString() : "";
                        if (VerificarEmprestimo == "EMPRESTADO")
                        {
                            livro.Emprestado.Estado = true;
                        }

                        livro.Emprestado.Data_Retirada = worksheet.Cells[i, 17].Value != null ? worksheet.Cells[i, 17].Value.ToString() : "";
                        livro.Emprestado.Data_Devolucao = worksheet.Cells[i, 18].Value != null ? worksheet.Cells[i, 18].Value.ToString() : "";
                        livro.Emprestado.Nome = worksheet.Cells[i, 19].Value != null ? worksheet.Cells[i, 19].Value.ToString() : "";
                        livro.Emprestado.Periodo = worksheet.Cells[i, 20].Value != null ? worksheet.Cells[i, 20].Value.ToString() : "";
                        livro.Nome_Doador = worksheet.Cells[i, 25].Value != null ? worksheet.Cells[i, 25].Value.ToString() : "";

                        await _livroRepositorio.AdicionarLivro(livro);
                        livros.Add(livro);
                    }
                }

                return Json(new { livros });
            }

            catch (Exception erro)
            {
                return Json(erro);
            }
        }


        //Código para deletar livros
        [HttpPost]
        public ActionResult DeleteLivro(string id)
        {
            try
            {
                _livroRepositorio.ExluirLivro(id);
                TempData["MensagemSucesso"] = "Dados atualizados com sucesso!";
                return Json("Deu Certo");
            }
            catch (Exception)
            {

                return Json("Deu errado");
            }

        }


        //Código para devolver Livros

        [HttpPost]
        public ActionResult DevolverLivro(string id)
        {
            try
            {
                _livroRepositorio.DevolverLivro(id);
                TempData["MensagemSucesso"] = "Dados atualizados com sucesso!";
                return Json("Deu certo");
            }
            catch (Exception)
            {

                return Json("Deu errado");
            }

        }
        //Código para pesquisar livros

        [HttpPost]
        public ActionResult PesquisarLivros(string Pesquisa, string Campo)
        {
            Task<List<LivroModel>> livros;

            livros = _livroRepositorio.BuscarParaDevolver(Pesquisa, Campo);

            return Json(new { livros  });
           // return View("PaginaDevolver", livros);

        }


        //Código para adicionar um livro

        [HttpPost]
        public IActionResult PostLivro(LivroModel livro)
        {
            try
            {
                _livroRepositorio.AdicionarLivro(livro);
                TempData["MensagemSucesso"] = "Livro Cadastrado com sucesso";
                return RedirectToAction("PaginaIncluir", "Painel");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, destalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Painel");
            }
        }



        //Código para emprestar um livro
        [HttpPost]
        public IActionResult PutLivro(LivroModel livro)
        {
            try
            {
                bool AdicionarLivroDB = _livroRepositorio.EmprestarLivro(livro);
                if (AdicionarLivroDB)
                {
                    TempData["MensagemSucesso"] = "Livro emprestado com sucesso";
                } else
                {
                    TempData["MensagemErro"] = "Tombo não registrado";
                }
                return RedirectToAction("PaginaEmprestar", "Painel");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, destalhe do erro: {erro.Message}";
                return RedirectToAction("PaginaEmprestar", "Painel");
            }
        }


        //Còdigo para Atualizar livro
        [HttpPost]
        public IActionResult AtualizarLivro(LivroModel livro)
        {
            try
            {
                _livroRepositorio.AtualizarLivro(livro);
                return RedirectToAction("PaginaBiblioteca", "Painel");
            }
            catch (Exception)
            {
                return RedirectToAction("PaginaBiblioteca", "Painel");
            }
        }
    }
}
