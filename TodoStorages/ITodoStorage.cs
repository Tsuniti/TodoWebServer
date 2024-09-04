using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebServer.Entities;

namespace TodoWebServer.TodoStorages;

public interface ITodoStorage
{
    IEnumerable<Todo> GetAll();
    void Create(string title);
    void Delete(Guid todoId);
    void Update(Guid todoId, string title, bool isCompleted);
}
