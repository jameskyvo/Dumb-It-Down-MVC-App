using DumbItDown.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System.Diagnostics;
using OpenAIModels = OpenAI.GPT3.ObjectModels;

namespace DumbItDown.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpenAIService openAiService;


        public HomeController(ILogger<HomeController> logger, IOpenAIService openAiService)
        {
            _logger = logger;
            this.openAiService = openAiService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string query = "dogs")
        {
            var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = $"Can you explain {query} in simple terms? 50 words or less.",
                MaxTokens = 90
            }, OpenAIModels.Models.TextDavinciV3);

            if (completionResult.Successful)
            {
                return View(null, completionResult.Choices?.FirstOrDefault()?.Text);
            }
            else
            {
                // log error
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}