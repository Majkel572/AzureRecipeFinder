using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using recipeFinder.Models;
using Azure.Storage.Blobs;
using System.Net;
using recipefinder.Controllers;
using System.Text.Json;

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
    public async Task<IActionResult> DoSomething()
    {
        //if(image is null) {
        //    return BadRequest("No file provided.");
        // }
        var uploadedFile = Request.Form.Files;
        foreach (var file in uploadedFile)
        {
            string Filename = file.FileName;
            Console.WriteLine(Filename);
        }
        var image = uploadedFile[0];

        WebRequest request = HttpWebRequest.Create("http://5613cafb-fb2a-4132-9a3e-0d32dc030956.northeurope.azurecontainer.io/score");
        request.Method = "POST";
        request.Headers.Add("Content-Type", "application/octet-stream");
        var f = image.OpenReadStream();
        using (var ms = new MemoryStream())
        {
            f.CopyTo(ms);
            var fileBytes = ms.ToArray();
            request.ContentLength = fileBytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(fileBytes, 0, fileBytes.Length);
            stream.Close();
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

        string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

        ResponseJson? jsonResponse = JsonSerializer.Deserialize<ResponseJson>(json);
        int maxIndex = 0;
        for (int i = 0; i < jsonResponse.probs.Length; i++)
        {
            if (jsonResponse.probs[i] > jsonResponse.probs[maxIndex])
            {
                maxIndex = i;
            }
        }

        string recipeName = jsonResponse.labels[maxIndex]; // to co wypluje ML
        Console.WriteLine(recipeName);
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
