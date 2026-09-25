using Microsoft.AspNetCore.Mvc;
using Minhaapi.models;
using Minhaapi.Services;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
        => _service = service;

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var cliente = _service.GetById(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var criado = _service.Create(cliente);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Cliente cliente)
    {
        try
        {
            var atualizado = _service.Update(id, cliente);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deletado = _service.Delete(id);
        if (!deletado) return NotFound();
        return NoContent();
    }
}