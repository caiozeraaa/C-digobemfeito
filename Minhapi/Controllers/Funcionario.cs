using Microsoft.AspNetCore.Mvc;
using Minhaapi.models;
using Minhaapi.Services;

[ApiController]
[Route("api/funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _service;

    public FuncionarioController(IFuncionarioService service)
        => _service = service;

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var funcionario = _service.GetById(id);
        if (funcionario == null) return NotFound();
        return Ok(funcionario);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Funcionario funcionario)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var criado = _service.Create(funcionario);
            return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Funcionario funcionario)
    {
        try
        {
            var atualizado = _service.Update(id, funcionario);
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