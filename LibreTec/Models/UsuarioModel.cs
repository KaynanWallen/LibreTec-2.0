using LibreTec.Helper;
using MongoDB.Bson.Serialization.Attributes;

namespace LibreTec.Models
{
    public class UsuarioModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("Nome")]
        public string Nome { get; set; } = "";


        [BsonElement("Login")]
        public string Login { get; set; } = "";


        [BsonElement("Email")]
        public string Email { get; set; } = "";


        [BsonElement("Senha")]
        public string Senha { get; set; } = "";



        public void SetarHashSenha()
        {
            if (Senha != null)
            {
                Senha = Senha.GeradorDeHash();
            }
        }

    }
}
