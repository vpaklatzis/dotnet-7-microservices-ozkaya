using System.Net;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/products")]
public class CatalogController : ControllerBase {

    private readonly IProductRepository _repository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository repository, ILogger<CatalogController> logger) {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts() {
        var products = await _repository.GetProducts();

        return Ok(products);
    }

    [HttpGet("{id:length(24)}", Name = " Get Product By Id")]
    //[Route("/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetProductById(string id) {
        var product = await _repository.GetProduct(id);

        if (product == null) {
            _logger.LogError($"Product with id: {id} not found.");
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet("{category}", Name = "Get Products By Category")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category) {
        var product = await _repository.GetProductsByCategory(category);

        return Ok(product);
    }
}