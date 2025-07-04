﻿using Microsoft.AspNetCore.Mvc;
using ModelContextProtocol.API.Application.DTO;
using ModelContextProtocol.API.Data.Entities;
using ModelContextProtocol.API.Data.Repositories;

namespace ModelContextProtocol.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/products
    /// <summary>
    /// Obtêm os produtos
    /// </summary>
    /// <returns>Coleção de objetos da classe Produto</returns>                
    /// <response code="200">Lista dos produtos</response>        
    /// <response code="400">Falha na requisição</response>         
    /// <response code="404">Nenhum produto foi localizado</response>         
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var products = await _productRepository.GetAllAsync();

        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }

    // GET: api/products/{id}
    /// <summary>
    /// Obtêm as informações do produto pelo seu Id
    /// </summary>
    /// <param name="id">Código do produto</param>
    /// <returns>Objetos da classe Produto</returns>                
    /// <response code="200">Informações do Producto</response>
    /// <response code="400">Falha na requisição</response>         
    /// <response code="404">O produto não foi localizado</response>         
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound("Nenhum produto encontrado.");
        }

        return Ok(product);
    }

    // POST api/products/
    /// <summary>
    /// Grava o produto
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST / Produto
    ///     {
    ///         "title": "Sandalia",
    ///         "description": "Sandália Preta Couro Salto Fino",
    ///         "price": 249.50,
    ///         "quantity": 100       
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Produto</returns>                
    /// <response code="201">O produto foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewProduct")]
    public async Task<IActionResult> PostAsync([FromBody] ProductDTO productDTO)
    {

        var colors = await _productRepository.GetColorsByIdsAsync(productDTO.ColorIds);

        var product = new ProductModel()
        {
            Title = productDTO.Title,
            Description = productDTO.Description,
            Price = productDTO.Price,
            Quantity = productDTO.Quantity,
            Colors = colors
        };

        await _productRepository.AddAsync(product);

        await _productRepository.SaveAsync();

        return CreatedAtAction("NewProduct", new { id = product.Id }, product);
    }

    // GET: api/products/search?query={text}
    /// <summary>
    /// Pesquisa produtos pelo título, descrição ou cor
    /// </summary>
    /// <param name="query">Texto a ser pesquisado</param>
    /// <returns>Lista de produtos</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchAsync([FromQuery] string query)
    {
        var results = await _productRepository.SearchByTermAsync(query);

        if (results == null || !results.Any())
        {
            return NotFound("Nenhum produto encontrado.");
        }

        return Ok(results);
    }

}
