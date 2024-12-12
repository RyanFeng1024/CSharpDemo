using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using RESTAPI.Services;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 此处标记整个控制器需要认证
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        // 通过依赖注入获得Service
        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有产品列表
        /// GET: api/products
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("call getall.");
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// 按ID获取单个产品
        /// GET: api/products/5
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("call GetById." + id);
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound($"Product with id {id} not found.");
            return Ok(product);
        }

        /// <summary>
        /// 按名称搜索产品
        /// GET: api/products/search?name=Apple
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            _logger.LogInformation("call SearchByName.");
            var products = await _productService.SearchByNameAsync(name);
            return Ok(products);
        }

        /// <summary>
        /// 新增产品
        /// POST: api/products
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            // 简单示例，未做详细验证
            var added = await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        /// <summary>
        /// 更新产品
        /// PUT: api/products/5
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID in URL and body do not match");
            }

            var updated = await _productService.UpdateAsync(product);
            if (updated == null) return NotFound($"Product with id {id} not found.");
            return Ok(updated);
        }

        /// <summary>
        /// 删除产品
        /// DELETE: api/products/5
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound($"Product with id {id} not found.");
            return NoContent();
        }
    }
}
