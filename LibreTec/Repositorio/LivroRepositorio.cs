using DnsClient;
using LibreTec.Data;
using LibreTec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibreTec.Repositorio
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly IMongoCollection<LivroModel> _livroCollection;

        public LivroRepositorio(IOptions<LivroDatabaseSettings> livroRepositorio)
        {
            var mongoClient = new MongoClient(livroRepositorio.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(livroRepositorio.Value.DatabaseName);
            _livroCollection = mongoDatabase.GetCollection<LivroModel>(livroRepositorio.Value.LivroCollectionName);
        }


        public async Task AdicionarLivro(LivroModel livro)
        {
            await _livroCollection.InsertOneAsync(livro);
        }

        //EmprestarLivro
        public bool EmprestarLivro(LivroModel livro)
        {
            LivroModel livroDB = _livroCollection.Find(x => x.Tombo_Atual == livro.Tombo_Atual).FirstOrDefault();
            if (livroDB != null)
            {
                livro.Id = livroDB.Id;
                livro.Tombo_Atual = livroDB.Tombo_Atual;
                livro.Tombo_Antigo = livroDB.Tombo_Antigo;
                livro.Autor = livroDB.Autor;
                livro.Titulo = livroDB.Titulo;
                livro.Data = livroDB.Data;
                livro.Editora = livroDB.Editora;
                livro.Local = livroDB.Local;
                livro.Ano = livroDB.Ano;
                livro.Aquisicao = livroDB.Aquisicao;
                livro.Cod_Barra = livroDB.Cod_Barra;
                livro.Genero = livroDB.Genero;
                livro.Cod_Classe = livroDB.Cod_Classe;
                livro.Arquivo = livroDB.Arquivo;
                livro.Palavra_Chave = livroDB.Palavra_Chave;
                livro.Clutter = livroDB.Clutter;
                livro.Nome_Doador = livroDB.Nome_Doador;
                livro.Emprestado.Estado = true;
                _livroCollection.ReplaceOne(x => x.Tombo_Atual == livro.Tombo_Atual, livro);
                return true;
            }

            return false;
        }

        public async Task<List<LivroModel>> BuscarTodosLivros()
        {
            return await _livroCollection.Find(x => true).ToListAsync();
        }

        public async Task<List<LivroModel>> BuscarParaDevolver(string Pesquisa, string Campo)
        {
            switch(Campo)
            {
                case "Titulo":
                    return await _livroCollection.Find(x => x.Titulo.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
                case "RA":
                    return await _livroCollection.Find(x => x.Emprestado.RA.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
                case "Nome":
                    return await _livroCollection.Find(x => x.Emprestado.Nome.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
                case "Tombo_Atual":
                    return await _livroCollection.Find(x => x.Tombo_Atual.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
                case "Tombo_Antigo":
                    return await _livroCollection.Find(x => x.Tombo_Antigo.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
                default:
                    return await _livroCollection.Find(x => x.Tombo_Atual.ToLower().Contains(Pesquisa.ToLower())).ToListAsync();
            }
        }

        public async Task<bool> DevolverLivro(string id)
        {
            LivroModel livroDB = await _livroCollection.Find(x => x.Id == id).FirstAsync();
            if (livroDB != null)
            {
                try
                {
                    livroDB.Emprestado.Estado = false;
                    livroDB.Emprestado.Data_Retirada = "";
                    livroDB.Emprestado.Data_Devolucao = "";
                    livroDB.Emprestado.Nome = "";
                    livroDB.Emprestado.RA = "";
                    livroDB.Emprestado.Periodo = "";
                    livroDB.Emprestado.Multa = 0;

                    await _livroCollection.ReplaceOneAsync(x => x.Id == id, livroDB);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;

        }

        public List<LivroModel> BuscarTodosLivrosSemTask()
        {
            return _livroCollection.Find(x => true).ToList();
        }

        public async Task<bool> ExluirLivro(string id)
        {
            LivroModel livroDB = await _livroCollection.Find(x => x.Id == id).FirstAsync();
            if(livroDB != null)
            {
                try
                {
                    await _livroCollection.DeleteOneAsync(x => x.Id == id);
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
           
        }


        public async Task AtualizarLivro(LivroModel livro)
        {
            await _livroCollection.ReplaceOneAsync(x => x.Id == livro.Id, livro);

        }

        public LivroModel BuscarUmLivro(string id)
        {
            LivroModel livroDB = _livroCollection.Find(x => x.Id == id).FirstOrDefault();
            return livroDB;
        }
    }
}
