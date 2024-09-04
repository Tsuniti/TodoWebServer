using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWebServer.Entities;

public class Todo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


}
