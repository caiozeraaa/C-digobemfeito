using Microsoft.AspNetCore.Mvc;
using Minhaapi.models;
using Minhaapi.Services;

[ApiController]
[Route("api/venda")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _service;

    public VendaController(IVendaService service)
        => _service = service;
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? cliente = null)
    => Ok(_service.GetAll(cliente));

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var venda = _service.GetById(id);
        if (venda == null) return NotFound();
        return Ok(venda);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CriarVendaRequest request)
    {
        try
        {
            var venda = _service.Create(request.ClienteId, request.ProdutoId, request.Quantidade);
            return CreatedAtAction(nameof(GetById), new { id = venda.Id }, venda);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
