using Microsoft.AspNetCore.Mvc;
using testAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClosedXML.Excel;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace testAPI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        // GET: Category Index (Hiển thị danh sách với tìm kiếm và phân trang)
        public async Task<IActionResult> Index(string keyword = null, int page = 1, int pageSize = 2)
        {
            var response = await _httpClient.GetAsync($"categories/paged/search?keyword={keyword}&page={page}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonContent);

                // Lấy tổng số mục từ header của API
                var totalItems = int.Parse(response.Headers.GetValues("X-Total-Count").FirstOrDefault() ?? "0");

                // Tính tổng số trang
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                // Truyền dữ liệu đến View
                ViewBag.Keyword = keyword;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(categories);
            }
            else
            {
                // Trường hợp API không trả về thành công
                return View(new List<Category>());
            }
        }

        // GET: Category/Create (Hiển thị form tạo mới Category)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create (Xử lý tạo mới Category)
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("categories", category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        // GET: Category/Details/{id} (Trả về dữ liệu chi tiết của Category)
        public async Task<IActionResult> Details(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Category>($"categories/{id}");

            if (category == null)
            {
                return NotFound();
            }

            // Trả về dữ liệu chi tiết dưới dạng JSON
            return Json(new { name = category.Name, description = category.Description });
        }


        // GET: Category/Edit/{id} (Hiển thị form chỉnh sửa Category)
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _httpClient.GetFromJsonAsync<Category>($"categories/{id}");
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/{id} (Xử lý chỉnh sửa Category)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"categories/{id}", category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(category);
        }

        // GET: Category/Delete/{id} (Xóa Category)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        // Chức năng xuất dữ liệu ra Excel
        public async Task<IActionResult> ExportExcel(string keyword = null)
        {
            // Lấy danh sách Category từ API với keyword (nếu có)
            var categories = await GetCategoriesFromApi(keyword);

            // Sử dụng EPPlus để tạo file Excel
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Categories");

                // Đặt tiêu đề cột
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Description";

                // Điền dữ liệu vào các ô
                for (int i = 0; i < categories.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = categories[i].CategoryId;
                    worksheet.Cells[i + 2, 2].Value = categories[i].Name;
                    worksheet.Cells[i + 2, 3].Value = categories[i].Description;
                }

                // Thiết lập kiểu định dạng cho các ô
                worksheet.Cells.AutoFitColumns();

                // Lưu file Excel vào bộ nhớ và trả về dưới dạng một file tải xuống
                var fileContents = package.GetAsByteArray();
                var fileName = "Categories.xlsx";

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }


        private async Task<List<Category>> GetCategoriesFromApi(string keyword)
        {
            var response = await _httpClient.GetAsync($"categories/search?keyword={keyword}");

            // Kiểm tra nếu response thành công và có dữ liệu
            if (response.IsSuccessStatusCode)
            {
                // Deserialize JSON thành List<Category>
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return categories;
            }

            // Nếu không thành công, trả về một danh sách rỗng
            return new List<Category>();
        }

    }
}
