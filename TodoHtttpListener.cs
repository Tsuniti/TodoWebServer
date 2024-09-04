using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TodoWebServer;

public class TodoHtttpListener
{
    private readonly HttpListener _httpListener;
    private readonly int _port;

    public event TodoDelegate? OnGet;
    public event TodoDelegate? OnPost;
    public event TodoDelegate? OnDelete;
    public event TodoDelegate? OnPut;

    public TodoHtttpListener(int port = 80)
    {
        _port = port;
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add($"http://localhost:{port}/");
    }

    public async Task ListenAsync()
    {
        await Task.Run(async () =>
        {
            _httpListener.Start();
            Console.WriteLine($"Server is listening {_port} port...");
            while (true)
            {
                var httpContext = await _httpListener.GetContextAsync();

                switch (httpContext.Request.HttpMethod)
                {
                    case "GET":
                        OnGet?.Invoke(httpContext.Request, httpContext.Response);
                        break;

                    case "POST":
                        OnPost?.Invoke(httpContext.Request, httpContext.Response);
                        break;

                    case "PUT":
                        OnPut?.Invoke(httpContext.Request, httpContext.Response);
                        break;

                    case "DELETE":
                        OnDelete?.Invoke(httpContext.Request, httpContext.Response);
                        break;

                }
            }
        });
    }
    public delegate void TodoDelegate(HttpListenerRequest request, HttpListenerResponse response);

}
