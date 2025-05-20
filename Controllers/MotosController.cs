using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Models;

namespace MottuApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/motos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll() =>
            Ok(await _context.Motos.ToListAsync());

        // GET: api/motos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            return moto == null ? NotFound() : Ok(moto);
        }

        // GET: api/motos/search?placa=XYZ1234
        [HttpGet("search")]
        public async Task<ActionResult<Moto>> SearchByPlaca([FromQuery] string placa)
        {
            var moto = await _context.Motos.FirstOrDefaultAsync(m => m.Placa == placa);
            return moto == null ? NotFound() : Ok(moto);
        }

        // POST: api/motos
        [HttpPost]
        public async Task<ActionResult<Moto>> Create(Moto moto)
        {
            if (string.IsNullOrWhiteSpace(moto.Placa)) return BadRequest("Placa obrigatória.");

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        // PUT: api/motos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Moto moto)
        {
            if (id != moto.Id) return BadRequest("IDs diferentes.");

            var existente = await _context.Motos.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Placa = moto.Placa;
            existente.Status = moto.Status;
            existente.Patio = moto.Patio;
            existente.DataEntrada = moto.DataEntrada;
            existente.DataSaida = moto.DataSaida;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/motos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
