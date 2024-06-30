using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IMessageProducer messageProducer) : ControllerBase
{
    private readonly IMessageProducer _messageProducer = messageProducer;

    // In-Memory Database
    public static readonly List<Booking> Bookings = new();


    [HttpPost]
    public IActionResult Post(Booking booking)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        Bookings.Add(booking);
        _messageProducer.SendingMessage<Booking>(booking);
        return Ok();
    }
}