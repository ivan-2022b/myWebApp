using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Visual.myProject.Pages;

public class PracticingModel(ILogger<PracticingModel> logger) : PageModel
{
    private readonly ILogger<PracticingModel> _logger = logger;

    public void OnGet()
    {

    }

    [BindProperty]
    public required string Name { get; set; }

    [BindProperty]
    public required string Email { get; set; }

    public void OnPost()
    {
        // Access the submitted data: Name and Email
        Console.WriteLine($"Name: {Name}, Email: {Email}");

        // Perform actions with the data (e.g., save to a database)
    }

    public void MyPost()
    {
        Console.WriteLine($"MyPost Name: {Name}, Email: {Email}");
    }
}
