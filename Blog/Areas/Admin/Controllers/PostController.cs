using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Blog.DAO;
using Blog.Infra;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    //dominio/Post
    //dominio/post/index => listagem post
    [Area("Admin")]
    public class PostController : Controller
    {
        private PostDAO dao;

        public PostController(PostDAO dao)
        {
            this.dao = dao;
        }

        public IActionResult Index()
        {
            IList<Post> lista = dao.Lista();

            return View(lista);
        }


        // Vai para a tela de Formulário
        public IActionResult Novo()
        {
            Post post = new Post();

            return View(post);
        }

        // Criar o Post - recebe os dados do formulário
        // Model Binding
        [HttpPost]
        public IActionResult Adiciona(Post post)
        {
            if (ModelState.IsValid)
            {
                dao.Adiciona(post);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Novo", post);
            }


        }

        // Post/categoria/filme (não é id)
        public IActionResult Categoria([Bind(Prefix = "id")] string categoria)
        {

            var lista = dao.FiltraPorCategoria(categoria);

            return View("index", lista);
        }

        public IActionResult RemovePost(int id)
        {
            dao.Remove(id);

            return RedirectToAction("Index");
        }

        public IActionResult Visualiza(int id)
        {

            Post post = dao.BucaPorId(id);

            return View(post);
        }

        [HttpPost]
        public IActionResult EditaPost(Post post)
        {
            if (ModelState.IsValid)
            {

                dao.Atualiza(post);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Visualiza", post);
            }
        }

        // Action que recebe o post a ser publicado
        public IActionResult PublicaPost(int id)
        {

            dao.Publica(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CategoriaAutocomplete(string termoDigitado)
        {

            var model = dao.ListaCategoriaQueContemTermo(termoDigitado);

            return Json(model);
        }
    }
}