using mamma_shopping_helper.DTOs;
using mamma_shopping_helper.Model;
using mamma_shopping_helper.Service;
using Microsoft.AspNetCore.Mvc;

namespace mamma_shopping_helper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdottiController : ControllerBase
    {
        private readonly IProdottoService _service;

        public ProdottiController(IProdottoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdotti()
        {
            var prodotti = await _service.GetAllProdottiAsync();
            return Ok(prodotti);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotto>> GetProdotto(int id)
        {
            var prodotto = await _service.GetProdottoByIdAsync(id);

            if (prodotto == null)
                return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

            return Ok(prodotto);
        }

        [HttpGet("lista/{listId}")]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdottiByLista(int listId)
        {
            var prodotti = await _service.GetProdottiByListaIdAsync(listId);
            return Ok(prodotti);
        }

        [HttpGet("utente/{userName}")]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdottiByUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { message = "Il nome utente non può essere vuoto" });

            var prodotti = await _service.GetProdottiByUserNameAsync(userName);
            return Ok(prodotti);
        }

        [HttpPost]
        public async Task<ActionResult<CreateProdottoDto>> CreateProdotto(CreateProdottoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Quantita <= 0)
                return BadRequest(new { message = "La quantità deve essere maggiore di 0" });

            var prodotto = new Prodotto
            {
               UserName = dto.UserName,
                Nome = dto.Nome,
                Quantita = dto.Quantita,
                ListaDellaSpesaId = dto.ListaDellaSpesaId

            };

            try
            {
                var nuovoProdotto = await _service.CreateProdottoAsync(prodotto);
                return CreatedAtAction(nameof(GetProdotto), new { id = nuovoProdotto.Id }, nuovoProdotto);
            }
            catch (ArgumentException ex)
            {              
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Altri errori generici
                return StatusCode(500, new
                {
                    message = "Errore imprevisto durante la creazione del prodotto",
                    details = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdotto(int id, UpdateProdottoDto dto)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var prodotto = new Prodotto
            {
                Id = id, 
                Nome = dto.Nome,
                Quantita = dto.Quantita,
                UserName = dto.UserName,
                Acquistato = dto.Acquistato
              
            };

            try
            {
                var result = await _service.UpdateProdottoAsync(id, prodotto);

                if (!result)
                    return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante l'aggiornamento del prodotto",
                    details = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdotto(int id)
        {
            try
            {
                var result = await _service.DeleteProdottoAsync(id);

                if (!result)
                    return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante l'eliminazione del prodotto",
                    details = ex.Message
                });
            }
        }

        [HttpPut("{id}/acquistato")]
        public async Task<IActionResult> ToggleAcquistato(int id)
        {
            try
            {
                var result = await _service.ToggleAcquistatoAsync(id);

                if (!result)
                    return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante il toggle acquistato",
                    details = ex.Message
                });
            }
        }

        [HttpPut("{id}/incrementa-quantita")]
        public async Task<IActionResult> IncrementaQuantita(int id, [FromQuery] int quantita = 1)
        {
            if (quantita <= 0)
                return BadRequest(new { message = "La quantità deve essere maggiore di 0" });

            if (quantita > 100)
                return BadRequest(new { message = "La quantità massima incrementabile è 100" });

            try
            {
                var result = await _service.IncrementaQuantitaAsync(id, quantita);

                if (!result)
                    return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante l'incremento della quantità",
                    details = ex.Message
                });
            }
        }

        [HttpPut("{id}/decrementa-quantita")]
        public async Task<IActionResult> DecrementaQuantita(int id, [FromQuery] int quantita = 1)
        {
            if (quantita <= 0)
                return BadRequest(new { message = "La quantità deve essere maggiore di 0" });

            if (quantita > 100)
                return BadRequest(new { message = "La quantità massima decrementabile è 100" });

            try
            {
                var result = await _service.DecrementaQuantitaAsync(id, quantita);

                if (!result)
                    return NotFound(new { message = $"Prodotto con ID {id} non trovato" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Errore durante il decremento della quantità",
                    details = ex.Message
                });
            }
        }
    }
}
