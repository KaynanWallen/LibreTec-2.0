using MongoDB.Bson.Serialization.Attributes;

namespace LibreTec.Models
{
    public class LivroModel
    {
        //Precisa desse primeiro por conta do retorno
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Tombo_Atual")]
        public string Tombo_Atual { get; set; } = "";


        [BsonElement("Tombo_Antigo")]
        public string Tombo_Antigo { get; set; } = "";


        [BsonElement("Autor")]
        public string Autor { get; set; } = "";

        [BsonElement("Titulo")]
        public string Titulo { get; set; } = "";


        [BsonElement("Data")]
        public string Data { get; set; } = "";

        [BsonElement("Editora")]
        public string Editora { get; set; } = "";

        [BsonElement("Local")]
        public string Local { get; set; } = "";

        [BsonElement("Ano")]
        public string Ano { get; set; } = "";

        [BsonElement("Aquisicao")]
        public string Aquisicao { get; set; } = "";

        [BsonElement("Cod_Barra")]
        public string Cod_Barra { get; set; } = "";

        [BsonElement("Genero")]
        public string Genero { get; set; } = "";

        [BsonElement("Cod_Classe")]
        public string Cod_Classe { get; set; } = "";


        [BsonElement("Arquivo")]
        public string Arquivo { get; set; } = "";
        [BsonElement("Palavra_Chave")]
        public string Palavra_Chave { get; set; } = "";

        [BsonElement("Clutter")]
        public string Clutter { get; set; } = "";

        [BsonElement("Nome_Doador")]
        public string Nome_Doador { get; set; } = "";

        [BsonElement("Emprestado")]
        public InformacaoEmprestado Emprestado { get; set; } 

        public class InformacaoEmprestado
        {
            [BsonElement("Estado")]
            public bool Estado { get; set; } = false;

            [BsonElement("Data_Retirada")]
            public string Data_Retirada { get; set; } = "Data";

            [BsonElement("Data_Devolucao")]
            public string Data_Devolucao { get; set; } = "";

            [BsonElement("Nome")]
            public string Nome { get; set; } = "";

            [BsonElement("RA")]
            public string RA { get; set; } = "";

            [BsonElement("Periodo")]
            public string Periodo { get; set; } = "";

            [BsonElement("Multa")]
            public int Multa { get; set; } = 0;
        }
    }
}
