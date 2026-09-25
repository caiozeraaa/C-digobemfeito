using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Minhaapi.models;

namespace Minhaapi.Controllers;

[ApiController]
[Route("api/tipos")]
public class TipoController : ControllerBase
{
    private static readonly List<Tipo> _tipos = [];
    private static int _nextId = 1;

    // GET /api/tipos
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_tipos);
    }

    // GET /api/tipos/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tipo = _tipos.FirstOrDefault(t => t.IdProduto == id);
        if (tipo == null)
            return NotFound();

        return Ok(tipo);
    }

    // POST /api/tipos
    [HttpPost]
    public IActionResult Create([FromBody] Tipo tipo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        tipo.IdProduto = _nextId++;
        _tipos.Add(tipo);

        return CreatedAtAction(
            nameof(GetById),
            new { id = tipo.IdProduto },
            tipo);
    }

    // PUT /api/tipos/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Tipo tipo)
    {
        var existente = _tipos.FirstOrDefault(t => t.IdProduto == id);
        if (existente == null)
            return NotFound();

        existente.NomeProduto = tipo.NomeProduto;

        return Ok(existente);
    }

    // DELETE /api/tipos/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existente = _tipos.FirstOrDefault(t => t.IdProduto == id);
        if (existente == null)
            return NotFound();

        _tipos.Remove(existente);

        return NoContent();
    }
}
