using Desafio;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("Nome e email são obrigatórios.");
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User updatedUser)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        user.Email = updatedUser.Email;
        _context.SaveChanges();
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
        return NoContent();
    }
}
