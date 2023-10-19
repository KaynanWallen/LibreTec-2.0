using LibreTec.Models;

namespace LibreTec.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);

        void RemoverSessaoDoUsuario();

        UsuarioModel? BuscarSessaoDoUsuario();
    }
}
