using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebServer.Entities;

namespace TodoWebServer.TodoStorages;

public class ListTodoStorage : ITodoStorage
{
    private List<Todo> _todos = new()
    {
        new Todo
        {
            Title = "First todo"
        },
        new Todo
        {
            Title = "Second todo"
        }
    };

    public void Create(string title) => _todos.Add(new Todo { Title = title });

    public void Delete(Guid todoId)
    {
        _todos.Remove(_todos.FirstOrDefault(t => t.Id == todoId));
    }

    public IEnumerable<Todo> GetAll() => _todos;

    public void Update(Guid todoId, string title, bool isCompleted)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == todoId);
        if (todo == null) throw new Exception("Todo not found");
        todo.Title = title;
        todo.IsCompleted = isCompleted;
        todo.UpdatedAt = DateTime.Now;
    }
}
