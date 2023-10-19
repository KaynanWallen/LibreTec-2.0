using DnsClient;
using LibreTec.Data;
using LibreTec.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibreTec.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IMongoCollection<UsuarioModel> _usuarioCollection;

        public UsuarioRepositorio(IOptions<LivroDatabaseSettings> usuarioRepositorio)
        {
            var mongoClient = new MongoClient(usuarioRepositorio.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(usuarioRepositorio.Value.DatabaseName);

            _usuarioCollection = mongoDatabase.GetCollection<UsuarioModel>(usuarioRepositorio.Value.UsuarioCollectionName);
        }

        public async Task AdicionarUsuario(UsuarioModel usuario)
        {
            usuario.SetarHashSenha();
            await _usuarioCollection.InsertOneAsync(usuario);
        }

        public bool ApagarUsuarioDB(string id)
        {
            UsuarioModel usuarioDB = _usuarioCollection.Find(x => x.Id == id).FirstOrDefault();
            if(usuarioDB != null)
            {
                try
                {
                    _usuarioCollection.DeleteOneAsync(x => x.Id == id);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }

        public  UsuarioModel BuscarPorlogin(string login)
        {
            UsuarioModel usuarioDB =  _usuarioCollection.Find(x => x.Login == login).FirstOrDefault();
            return usuarioDB;
        }

        public List<UsuarioModel> Buscartodos()
        {
            List<UsuarioModel> usuarios = _usuarioCollection.Find(x => true).ToList();
            return usuarios;
        }
    }
}
