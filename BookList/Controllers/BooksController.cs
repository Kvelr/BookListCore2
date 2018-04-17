using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        public BooksController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        private readonly AppDBContext _dbContext;


        public IActionResult Index()
        {
            return View(_dbContext.Books.ToList());
        }

        //Get: book/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book bookToAdd)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(bookToAdd);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookToAdd);
        }

        //Details: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        //Edit: Books/Details/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        //Post: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                _dbContext.Update(book);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }



        //Delete: Books/Details/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        //Post: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryTokenAttribute]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.ID == id);

            if (book == null)
                return NotFound();

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}