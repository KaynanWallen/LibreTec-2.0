using LibreTec.Models;

namespace LibreTec.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task AdicionarUsuario(UsuarioModel usuario);
        UsuarioModel BuscarPorlogin(string login);
        List<UsuarioModel> Buscartodos();
        bool ApagarUsuarioDB(string id);
    }
}
