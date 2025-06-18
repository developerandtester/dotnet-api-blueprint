using Xunit;
using MyBlueprintApi.Controllers;
using MyBlueprintApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Tests;

public class TodoControllerTests
{
    [Fact]
    public void Post_Creates_Todo()
    {
        var controller = new ToDoController();
        var newTodo = new ToDoItem { Title = "Write unit tests", IsCompleted = false };

        var result = controller.Create(newTodo);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdTodo = Assert.IsType<ToDoItem>(createdResult.Value);
        Assert.Equal("Write unit tests", createdTodo.Title);
        Assert.False(createdTodo.IsCompleted);
        Assert.NotEqual(Guid.Empty, createdTodo.Id);
    }

    [Fact]
    public void GetAll_Returns_All_Todos()
    {
        var controller = new ToDoController();
        controller.Create(new ToDoItem { Title = "Task 1", IsCompleted = false });
        controller.Create(new ToDoItem { Title = "Task 2", IsCompleted = true });

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var todos = Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(okResult.Value);
        Assert.Equal(2, todos.Count());
    }
}
