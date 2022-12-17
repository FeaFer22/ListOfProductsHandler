using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListOfProductsHandler.Database.Context;
using ListOfProductsHandler.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ListOfProductsHandler.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private IQueryable<Product> products;

        public ProductsController(AppDbContext context)
        {
            _context = context;
            products = context.Products.AsQueryable();
        }

        #region CRUD
        // GET: Products
        public IActionResult Index(SortState sortOrder)
        {
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case SortState.PriceDesc:
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case SortState.PriceAsc:
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return View(products.ToList());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var product = await _context.Products.FindAsync(id);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
