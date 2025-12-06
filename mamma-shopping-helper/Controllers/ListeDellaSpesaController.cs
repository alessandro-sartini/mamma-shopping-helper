using mamma_shopping_helper.DTOs;
using mamma_shopping_helper.Model;
using mamma_shopping_helper.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mamma_shopping_helper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListeDellaSpesaController : ControllerBase
    {
        private readonly IListeDellaSpesaService _service;

        public ListeDellaSpesaController(IListeDellaSpesaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaDellaSpesa>>> GetListeDellaSpesa()
        {
            var liste = await _service.GetAllListeAsync();
            return Ok(liste);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListaDellaSpesa>> GetListaDellaSpesa(int id)
        {
            var lista = await _service.GetListaByIdAsync(id);

            if (lista == null)
                return NotFound(new { message = $"Lista con ID {id} non trovata" });

            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<ListaDellaSpesa>> CreateListaDellaSpesa(CreateListaDellaSpesaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

        
            var lista = new ListaDellaSpesa
            {
                Titolo = dto.Titolo,
                Descrizione = dto.Descrizione
            };

            try
            {
                var nuovaLista = await _service.CreateListaAsync(lista);

                return CreatedAtAction(nameof(GetListaDellaSpesa), new { id = nuovaLista.Id }, nuovaLista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore imprevisto durante la creazione della lista",
                    details = ex.Message
                });
            }
        }

        // PUT: api/ListeDellaSpesa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateListaDellaSpesa(int id, UpdateListaDellaSpesaDto dto)  // ⬅️ DTO
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lista = new ListaDellaSpesa
            {
                Id = id,  // ⬅️ Id dall'URL
                Titolo = dto.Titolo,
                Descrizione = dto.Descrizione,
                Conclusa = dto.Conclusa
            };

            try
            {
                var result = await _service.UpdateListaAsync(id, lista);

                if (!result)
                    return NotFound(new { message = $"Lista con ID {id} non trovata" });

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "La lista è stata modificata da un altro utente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante l'aggiornamento della lista",
                    details = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListaDellaSpesa(int id)
        {
            
            try
            {
                var result = await _service.DeleteListaAsync(id);

                if (!result)
                    return NotFound(new { message = $"Lista con ID {id} non trovata" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante l'eliminazione della lista",
                    details = ex.Message
                });
            }
        }

       
        // PUT: api/ListeDellaSpesa/5/conclusa
        [HttpPut("{id}/conclusa")]
        public async Task<IActionResult> ToggleConclusaLista(int id)
        {
            try
            {
                var result = await _service.ToggleConclusaAsync(id);

                if (!result)
                    return NotFound(new { message = $"Lista con ID {id} non trovata" });

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante il toggle dello stato conclusa",
                    details = ex.Message
                });
            }
        }
    }
}
