using Microsoft.AspNetCore.Mvc;

namespace ContosoWeb.Controllers;

public class WeddingGuest
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public int TableNumber { get; set; }
}

[ApiController]
[Route("api/seating")]
public class SeatingApiController : ControllerBase
{
    private static readonly List<WeddingGuest> Guests = new()
    {
        new WeddingGuest
        {
            Id = 1,
            Name = "Alice",
            TableNumber = 1
        }
    };

    [HttpPost]
    public IActionResult Create(WeddingGuest guest)
    {
        if (string.IsNullOrWhiteSpace(guest.Name))
        {
            return BadRequest();
        }

        guest.Id = Guests.Count == 0
            ? 1
            : Guests.Max(x => x.Id) + 1;

        Guests.Add(guest);

        return Created($"/api/seating/{guest.Id}", guest);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WeddingGuest guest)
    {
        var existing = Guests.FirstOrDefault(x => x.Id == id);

        if (existing == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(guest.Name))
        {
            return BadRequest();
        }

        existing.Name = guest.Name;
        existing.TableNumber = guest.TableNumber;

        return NoContent();
    }
}
