using MongoDB.Bson.Serialization.Attributes;

namespace LibreTec.Models
{
    public class DevolverLivroModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Aluno { get; set; } = "";
        public string RA { get; set; } = "";
        public string Pesquisa { get; set; } = "";
        public string Data_Devolucao { get; set; } = "";
        public string Data_Retirada { get; set; } = "";

    }
}
