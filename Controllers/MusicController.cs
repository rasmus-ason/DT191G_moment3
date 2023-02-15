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
    public class MusicController : Controller
    {
        private readonly MusicContext _context;

        private readonly BorrowContext _borrowContext;

        public MusicController(MusicContext context, BorrowContext borrowContext)
        {
            _context = context;
            _borrowContext = borrowContext;
        }

        // GET: Music
        public IActionResult Index(string? SearchString)
        {
            //Declare the currentFilter viewData
            ViewData["CurrentFilter"] = SearchString;

            var result = from a in _context.Music select a;

            //Check if search string is empty
            if (!String.IsNullOrEmpty(SearchString))
            {
                //Make search case-insenitive
                var searchStringLower = SearchString.ToLower();
                result = result.Where(a => a.Artist.ToLower().Contains(searchStringLower) || a.Title.ToLower().Contains(searchStringLower));
            }

            //Sort by artist name
            result = result.OrderBy(a => a.Artist);

            return View(result);

        }

        // GET: Music
        public IActionResult AvailableAlbums(string SearchStringAvailableAlbums)
        {

            ViewData["CurrentFilterAvailableAlbums"] = SearchStringAvailableAlbums;

            var result = from a in _context.Music select a;

            if (!String.IsNullOrEmpty(SearchStringAvailableAlbums))
            {
                //Make search case-insenitive
                var searchStringLower = SearchStringAvailableAlbums.ToLower();
                result = result.Where(a => a.Artist.ToLower().Contains(searchStringLower) || a.Title.ToLower().Contains(searchStringLower));
            }

            //Sort by artist name
            result = result.OrderBy(a => a.Artist);

            return View(result);


        }


        // GET: Music/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FirstOrDefaultAsync(m => m.Id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // GET: Music/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Music/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,Tracks,isBorrowed")] Music music)
        {
            if (ModelState.IsValid)
            {
                music.IsBorrowed = false;
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        // GET: Music/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        // POST: Music/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,Tracks,IsBorrowed")] Music music)
        {
            if (id != music.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                    
                    _context.Update(music);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(music.Id))
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
            return View(music);
        }

        

        // GET: Music/Borrow/5
        public async Task<IActionResult> ReturnAlbum(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnAlbum(int id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            //Set value to false on album
            var updateMusicBool = await _context.Music.FindAsync(id);
            updateMusicBool.IsBorrowed = false;
            _context.Update(updateMusicBool);
            await _context.SaveChangesAsync();

            //Set value to return date
            var returnAlbum = await _borrowContext.Borrow.FirstOrDefaultAsync(m => m.AlbumId == id && m.ReturnDate == null);

            //Add return date if value is null
            if(returnAlbum.ReturnDate == null) {

                returnAlbum.ReturnDate = DateTime.Now;
                _borrowContext.Update(returnAlbum);

                await _borrowContext.SaveChangesAsync();
            }
           
            return RedirectToAction(nameof(Index));
        }
       

        // GET: Music/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Music == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .FirstOrDefaultAsync(m => m.Id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // POST: Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Music == null)
            {
                return Problem("Entity set 'MusicContext.Music'  is null.");
            }
            var music = await _context.Music.FindAsync(id);
            if (music != null)
            {
                _context.Music.Remove(music);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(int id)
        {
          return (_context.Music?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
