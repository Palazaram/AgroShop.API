using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.QueryParameters;
using AgroShop.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    //public class ProductController : ControllerBase
    //{
    //    private readonly IProductService _productService;

    //    public ProductController(IProductService productService)
    //    {
    //        _productService = productService;        
    //    }

    //    [HttpGet("GetProducts")]
    //    [ProducesResponseType(typeof(IEnumerable<ClientProductDto>), StatusCodes.Status200OK)]
    //    public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters query, CancellationToken cancellationToken)
    //    {
    //        var result = await _productService.GetProductsForClientAsync(query, cancellationToken);

    //        if (!result.IsSuccess)
    //            return StatusCode(500, result.Error);

    //        return Ok(result.Data);
    //    }

    //    [HttpGet("GetProductById/{id}")]
    //    public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken, [FromQuery] bool asNoTracking = true)
    //    {
    //        var productId = new ProductId(id);

    //        var result = await _productService.GetClientProductByIdAsync(productId, cancellationToken, asNoTracking);

    //        if (!result.IsSuccess)
    //            return StatusCode(500, result.Error);

    //        return Ok(result.Data);
    //    }

    //    [HttpPost("AddProduct")]
    //    [Consumes("multipart/form-data")]
    //    [ProducesResponseType(typeof(CreateProductDto), StatusCodes.Status201Created)]
    //    public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDTO, CancellationToken cancellationToken)
    //    {
    //        var result = await _productService.AddProductAsync(createProductDTO, cancellationToken);

    //        if (!result.IsSuccess)
    //        {
    //            // Если ошибка валидации
    //            if (result.Error.Code.Contains("ValidationFailed"))
    //                return BadRequest(result.Error);

    //            // Иначе 500 Internal Server Error
    //            return StatusCode(500, result.Error);
    //        }

    //        return CreatedAtAction(nameof(GetProductById), new { id = result.Data.Id.Value }, result.Data!);
    //    }

    //    [HttpPost("UpdateProduct/{id}")]
    //    [Consumes("multipart/form-data")]
    //    [ProducesResponseType(typeof(EditProductDto), StatusCodes.Status204NoContent)]
    //    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] EditProductDto editProductDTO, CancellationToken cancellationToken)
    //    {
    //        if (id != editProductDTO.Id)
    //            return BadRequest("Id в URL и в теле запроса не совпадают.");

    //        var result = await _productService.UpdateProductAsync(editProductDTO, cancellationToken);

    //        if (!result.IsSuccess)
    //        {
    //            if (result.Error.Code.Contains("ValidationFailed"))
    //                return BadRequest(result.Error);

    //            if (result.Error.Code.Contains("NotFound"))
    //                return NotFound(result.Error);

    //            return StatusCode(500, result.Error);
    //        }

    //        return NoContent();
    //    }

    //    [HttpDelete("DeleteProduct/{id}")]
    //    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    //    {
    //        var result = await _productService.DeleteProductAsync(id, cancellationToken);

    //        if (!result.IsSuccess)
    //        {
    //            if (result.Error.Code.Contains("NotFound"))
    //                return NotFound(result.Error);

    //            return StatusCode(500, result.Error);
    //        }

    //        return NoContent();
    //    }
    //}
}
