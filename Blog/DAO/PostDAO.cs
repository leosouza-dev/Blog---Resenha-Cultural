using Blog.Infra;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.DAO
{
    public class PostDAO
    {
        private BlogContext contexto;

        public PostDAO(BlogContext contexto)
        {
            this.contexto = contexto;
        }

        // método que devolve uma lista de posts
        public IList<Post> Lista()
        {
            var lista = contexto.Posts.ToList();
            return lista;
        }

        // metodo para persistir posts
        public void Adiciona(Post post)
        {
            contexto.Posts.Add(post);
            contexto.SaveChanges();
        }

        public IList<Post> FiltraPorCategoria(string categoria)
        {
            var lista = contexto.Posts.Where(p => p.Categoria.Contains(categoria)).ToList();
            return lista;
        }

        public void Remove(int id)
        {
            // encontra o post com o id definido
            var post = contexto.Posts.Find(id);

            contexto.Remove(post);

            contexto.SaveChanges();
        }

        public Post BucaPorId(int id)
        {
            Post post = contexto.Posts.Find(id);
            return post;
        }

        public void Atualiza(Post post)
        {
            contexto.Update(post);
            contexto.SaveChanges();
        }

        public void Publica(int id)
        {
            Post post = contexto.Posts.Find(id);

            post.Publicado = true;
            post.DataPublicacao = DateTime.Now;

            contexto.SaveChanges();
        }

        public IList<string> ListaCategoriaQueContemTermo(string termo)
        {
            return contexto.Posts.Where(p => p.Categoria.Contains(termo))
                                 .Select(p => p.Categoria)
                                 .Distinct()
                                 .ToList();
        }

        public IList<Post> ListaPublicados()
        {
            return contexto.Posts.Where(p => p.Publicado)
                   .OrderByDescending(p => p.DataPublicacao).ToList();
        }

        public IList<Post> BuscaPeloTermo(string termo)
        {
            return contexto.Posts
                .Where(p => (p.Publicado) && (p.Titulo.Contains(termo) || p.Resumo.Contains(termo)))
                .ToList();
        }
    }
}
