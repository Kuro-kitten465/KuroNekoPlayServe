using System.Net;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Cores
{
    internal class Server
    {
        private readonly HttpListener _listener;
        private readonly string _gameDirectory;
        private readonly ushort _port;
        private bool _isRunning;
        private readonly Dictionary<string, string> _mimeTypes;

        public Server(ushort port, string gameDirectory)
        {
            _port = port;
            _gameDirectory = gameDirectory;
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/");
            
            _mimeTypes = new Dictionary<string, string>
            {
                { ".html", "text/html" },
                { ".js", "application/javascript" },
                { ".css", "text/css" },
                { ".json", "application/json" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".woff", "application/font-woff" },
                { ".woff2", "application/font-woff2" },
                { ".ttf", "application/font-ttf" },
                { ".data", "application/octet-stream" },
                { ".wasm", "application/wasm" },
                { ".unityweb", "application/octet-stream" }
            };
        }

        public void Start()
        {
            _isRunning = true;
            _listener.Start();
            WriteLine($"{NL}{COMPLETE}Server started at http://localhost:{_port}/");
            
            Task.Run(HandleRequests);
        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
        }

        private async Task HandleRequests()
        {
            while (_isRunning)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    ProcessRequest(context);
                }
                catch (HttpListenerException)
                {
                    if (_isRunning)
                    {
                        WriteLine($"{EXCEPTION}Error handling request");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"{EXCEPTION}Unexpected error: {ex.Message}");
                }
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            string requestedFile = request.Url?.LocalPath ?? "/";
            if (requestedFile == "/")
                requestedFile = "/index.html";

            string filePath = Path.Combine(_gameDirectory, requestedFile.TrimStart('/'));
            
            WriteLine($"{PROGRESS}Request: {request.Url?.LocalPath} -> {filePath}");

            try
            {
                if (!File.Exists(filePath))
                {
                    response.StatusCode = 404;
                    WriteLine($"{WARNING}File not found: {filePath}");
                    return;
                }

                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (_mimeTypes.TryGetValue(extension, out string? mimeType))
                {
                    response.ContentType = mimeType;
                }

                using var fileStream = File.OpenRead(filePath);
                response.ContentLength64 = fileStream.Length;
                fileStream.CopyTo(response.OutputStream);
                
                WriteLine($"{COMPLETE}Served: {filePath}");
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                WriteLine($"{EXCEPTION}Error serving {filePath}: {ex.Message}");
            }
            finally
            {
                response.Close();
            }
        }
    }
}
