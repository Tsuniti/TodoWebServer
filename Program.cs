using System.Text;
using TodoWebServer;
using TodoWebServer.Entities;
using TodoWebServer.Prowiders;
using TodoWebServer.TodoStorages;

TodoHtttpListener server = new TodoHtttpListener(5000);
ITodoStorage todoStorage = new ListTodoStorage();

//////////////////////////////////////////////////////////////////////////////////////////////////////////

server.OnGet += async (request, response) =>
{
    string responseBody = JsonProvider.Serialize(todoStorage.GetAll());

    byte[] bytes = Encoding.UTF8.GetBytes(responseBody);

    await response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
    response.Close();
};

//////////////////////////////////////////////////////////////////////////////////////////////////////////

server.OnPost += async (request, response) =>
{
    byte[] buffer = new byte[request.ContentLength64];
    await request.InputStream.ReadAsync(buffer, 0, buffer.Length);

    string todoTitle = Encoding.UTF8.GetString(buffer);

    todoStorage.Create(todoTitle);

    response.OutputStream.Close();
};

//////////////////////////////////////////////////////////////////////////////////////////////////////////

// curl -d "0441692d-feb6-4d6c-8fdf-ba94adc91206" -X DELETE http://localhost:5000

server.OnDelete += async (request, response) =>
{
    byte[] buffer = new byte[request.ContentLength64];
    await request.InputStream.ReadAsync(buffer, 0, buffer.Length);

    Guid todoId = Guid.Parse(Encoding.UTF8.GetString(buffer));

    todoStorage.Delete(todoId);

    response.OutputStream.Close();
};

//////////////////////////////////////////////////////////////////////////////////////////////////////////

/*
 * powershell
 
 Invoke-RestMethod -Uri "http://localhost:5000" -Method PUT -Body '{"id": "0635e60f-46d7-44a2-bb29-3bc78ddce3fb", "title": "Updated", "isCompleted": true}' -ContentType "application/json"

 * or with return of information

 Invoke-WebRequest -Uri "http://localhost:5000" -Method PUT -Body '{"id": "0635e60f-46d7-44a2-bb29-3bc78ddce3fb", "title": "Updated", "isCompleted": true}' -ContentType "application/json"

 * cmd
 curl -d "{\"id\": \"4a4803b0-386c-4591-8108-39a43b4e735c\", \"title\": \"Updated\", \"isCompleted\": true}" -X PUT http://localhost:5000

*/

server.OnPut += async (request, response) =>
{
    byte[] buffer = new byte[request.ContentLength64];
    await request.InputStream.ReadAsync(buffer, 0, buffer.Length);
    var todo = JsonProvider.Deserialize<Todo>(Encoding.UTF8.GetString(buffer));
    todoStorage.Update(todo.Id, todo.Title, todo.IsCompleted);
    response.OutputStream.Close();
};

//////////////////////////////////////////////////////////////////////////////////////////////////////////

server.ListenAsync();

Console.ReadLine();