using LibreTec.Models;

namespace LibreTec.Repositorio
{
    public interface ILivroRepositorio
    {
        Task<List<LivroModel>> BuscarTodosLivros();
        List<LivroModel> BuscarTodosLivrosSemTask();
        LivroModel BuscarUmLivro(string id);
        Task<List<LivroModel>> BuscarParaDevolver(string pesquisa, string valor);
        Task AdicionarLivro(LivroModel livro);
        bool EmprestarLivro(LivroModel livro);
        Task AtualizarLivro(LivroModel livro);
        Task<bool> ExluirLivro(string id);
        Task<bool> DevolverLivro(string id);
    }
}
