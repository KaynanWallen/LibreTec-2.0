namespace LibreTec.Data
{
    public class LivroDatabaseSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string LivroCollectionName { get; set; } = "Livro";
        public string UsuarioCollectionName { get; set; } = "Usuarios";
    }
}
