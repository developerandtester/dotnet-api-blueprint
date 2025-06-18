using Microsoft.AspNetCore.Mvc;
using MyBlueprintApi.Models;

namespace MyBlueprintApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private static readonly List<ToDoItem> _todos = new();

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItem>> GetAll() => Ok(_todos);

    [HttpPost]
    public ActionResult<ToDoItem> Create(ToDoItem item)
    {
        item.Id = Guid.NewGuid();
        _todos.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpGet("{id}")]
    public ActionResult<ToDoItem> GetById(Guid id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, ToDoItem updated)
    {
        var index = _todos.FindIndex(t => t.Id == id);
        if (index == -1) return NotFound();

        updated.Id = id;
        _todos[index] = updated;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _todos.FirstOrDefault(t => t.Id == id);
        if (item is null) return NotFound();

        _todos.Remove(item);
        return NoContent();
    }
}
