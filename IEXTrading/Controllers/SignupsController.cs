using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IEXTrading.DataAccess;
using IEXTrading.Models;

namespace IEXTrading.Controllers
{
    public class SignupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SignupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Signups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: Signups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signup = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (signup == null)
            {
                return NotFound();
            }

            return View(signup);
        }

        // GET: Signups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Signups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,customer_id,first_name,last_name,age,Email,activity,recreation_center,days_of_week,times")] Signup signup)
        {
            if (ModelState.IsValid)
            {
                signup.Id = Guid.NewGuid();
                _context.Add(signup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signup);
        }

        // GET: Signups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signup = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            if (signup == null)
            {
                return NotFound();
            }
            return View(signup);
        }

        // POST: Signups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,customer_id,first_name,last_name,age,Email,activity,recreation_center,days_of_week,times")] Signup signup)
        {
            if (id != signup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignupExists(signup.Id))
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
            return View(signup);
        }

        // GET: Signups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signup = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (signup == null)
            {
                return NotFound();
            }

            return View(signup);
        }

        // POST: Signups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var signup = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customer.Remove(signup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignupExists(Guid id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
