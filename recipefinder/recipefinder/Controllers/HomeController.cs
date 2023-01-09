using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using recipeFinder.Models;
using Azure.Storage.Blobs;

namespace recipeFinder.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration config;

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
        this.config = config;
    }
    [HttpPost("getrecipe")]
    public async Task<IActionResult> DoSomething([FromForm] IFormFile image)
    {
        /* Tutaj dodać łączenie z mlem i jego odpowiedź sparsować na recipeName */





        string recipeName = "apple_pie"; // to co wypluje ML
        var connectionString = config.GetValue<string>("blobConnectionString");
        var blobContainerName = config.GetValue<string>("blobContainerName");
        BlobClient blobClient = new BlobClient(connectionString, blobContainerName, recipeName);
        using (var stream = new MemoryStream())
        {
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;
            var contentType = (await blobClient.GetPropertiesAsync()).Value.ContentType;
            //Console.WriteLine("JESTEM TU HURA");
            return File(stream.ToArray(), contentType, blobClient.Name);
        }
    }
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
