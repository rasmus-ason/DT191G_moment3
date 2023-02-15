using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dt191G_moment3.Data;
using Dt191G_moment3.Models;

namespace Dt191G_moment3.Controllers
{
    public class BorrowController : Controller
    {
        private readonly BorrowContext _context;
        private readonly MusicContext _musicContext;

        public BorrowController(BorrowContext context, MusicContext musicContext)
        {
            _context = context;
            _musicContext = musicContext;
        }

       

        // GET: Borrow
        public async Task<IActionResult> Index()
        {
              return _context.Borrow != null ? 
                          View(await _context.Borrow.ToListAsync()) :
                          Problem("Entity set 'BorrowContext.Borrow'  is null.");
        }

         // GET: Borrow
        public async Task<IActionResult> ReturnedAlbums()
        {
            var result = _context.Borrow != null ? _context.Borrow.OrderByDescending(b => b.ReturnDate) : null;

            return result != null ? View(await result.ToListAsync()) : Problem("Entity set 'BorrowContext.Borrow' is null.");
        }


        // GET: Borrow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Borrow == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        

         // GET: Borrow/Create
        public IActionResult Create(string artist, string title, int AlbumId)
        {
            if(artist != null){

                    ViewData["ArtistBorrowAlbum"] = artist;
                    ViewData["TitleBorrowAlbum"] = title;
            }
           

            return View();
        }

        // POST: Borrow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BorrowedTitle,BorrowedArtist,BorrowedTo,AlbumId,BorrowedDate,ReturnDate")] Borrow borrow)
        {

            if (ModelState.IsValid)
            {
                //Add date of borrow
                borrow.BorrowedDate = DateTime.Now;
                borrow.ReturnDate = null;

                //Update bool in music db
                var updateBoolMusic = await _musicContext.Music.FirstOrDefaultAsync(m => m.Id == borrow.AlbumId);
                updateBoolMusic.IsBorrowed = true;
                _musicContext.Update(updateBoolMusic);
                await _musicContext.SaveChangesAsync();
            
                _context.Add(borrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrow);
        }

        // GET: Borrow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Borrow == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }
            return View(borrow);
        }

        // POST: Borrow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BorrowedTitle,BorrowedArtist,AlbumId,BorrowedTo,BorrowedDate")] Borrow borrow)
        {
            if (id != borrow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowExists(borrow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(borrow);
        }

        // GET: Borrow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Borrow == null)
            {
                return NotFound();
            }

            var borrow = await _context.Borrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrow == null)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // POST: Borrow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Borrow == null)
            {
                return Problem("Entity set 'BorrowContext.Borrow'  is null.");
            }
            var borrow = await _context.Borrow.FindAsync(id);
            if (borrow != null)
            {
                _context.Borrow.Remove(borrow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ReturnedAlbums));
        }

        private bool BorrowExists(int id)
        {
          return (_context.Borrow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
