﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeVendas.DAL;
using SistemaDeVendas.Entidades;
using SistemaDeVendas.Models;

namespace SistemaDeVendas.Controllers
{
    public class CategoriaController : Controller
    {
        //Injeção de dependência
        public readonly ApplicationDbContext _context;
        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _context.Categoria.ToList();
            //liberar memória
            _context.Dispose();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();
            if (id != null)
            {
                var entidade = _context.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
            }

            
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria objCategoria = new Categoria()
                {
                    Codigo = model.Codigo,
                    Descricao = model.Descricao
                };

                if(model.Codigo == null)
                {
                    _context.Categoria.Add(objCategoria);
                }
                else
                {
                    _context.Entry(objCategoria).State = EntityState.Modified;
                }
                _context.SaveChanges();

            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var cat = new Categoria() { Codigo = id };
            _context.Attach(cat);
            _context.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}
