using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WebDtContext _context;

        public CategoriesController(WebDtContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
        //// GET: api/Categories/search?keyword=
        //[HttpGet("search")]
        //public async Task<ActionResult<IEnumerable<Category>>> SearchCategories(string keyword)
        //{
        //    if (string.IsNullOrEmpty(keyword)) // Nếu không nhập keyword
        //    {
        //        return await _context.Categories.ToListAsync(); // Trả về toàn bộ danh sách
        //    }

        //    var categories = await _context.Categories
        //        .Where(c => c.Name.Contains(keyword) || c.Description.Contains(keyword))
        //        .ToListAsync();

        //    return categories;
        //}

        ////phân trang
        //[HttpGet("paged")]
        //public async Task<ActionResult<IEnumerable<Category>>> GetPagedCategories(int page = 1, int pageSize = 10)
        //{
        //    var totalItems = await _context.Categories.CountAsync();
        //    var categories = await _context.Categories
        //        .Skip((page - 1) * pageSize) // Bỏ qua các mục đã hiển thị
        //        .Take(pageSize) // Lấy số lượng mục theo `pageSize`
        //        .ToListAsync();

        //    // Trả kèm tổng số mục để hỗ trợ hiển thị số trang
        //    Response.Headers.Add("X-Total-Count", totalItems.ToString());
        //    return categories;
        //}

        //phân trang seach
        [HttpGet("paged/search")]
        public async Task<ActionResult<IEnumerable<Category>>> GetPagedCategoriesWithSearch(string keyword = null, int page = 1, int pageSize = 10)
        {
            // Lọc theo keyword nếu có
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Description.Contains(keyword));
            }

            // Lấy tổng số mục
            var totalItems = await query.CountAsync();

            // Lấy danh sách phân trang
            var categories = await query
                .Skip((page - 1) * pageSize) // Bỏ qua các mục đã hiển thị
                .Take(pageSize) // Lấy số lượng mục theo pageSize
                .ToListAsync();

            // Trả kèm tổng số mục để hỗ trợ phân trang
            Response.Headers.Add("X-Total-Count", totalItems.ToString());

            return categories;
        }


    }
}
