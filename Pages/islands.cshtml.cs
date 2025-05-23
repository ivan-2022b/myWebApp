using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;

namespace Visual.myProject.Pages;

public class IslandsModel(ILogger<IslandsModel> logger) : PageModel
{
    private readonly ILogger<IslandsModel> _logger = logger;
    public string _bitColumns = "";
    static int bitColumns, bitRows, countIslands = 0;
    static BitArray bitArray = new(0);
    [BindProperty]
    public required string inputColumns { get; set; }
    [BindProperty]
    public required string inputRows { get; set; }
    [BindProperty]
    public required string POST_Route { get; set; }

    static readonly HttpClient client = new();

    static async Task MyMain()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            using HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    public void OnGet()
    {
        ViewData["workingDirectory"] = Environment.CurrentDirectory;
        Process process = new();
        ProcessStartInfo startInfo = new() { // WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardInput = false,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            FileName = "cmd.exe",
            Arguments = "/c xmysql --version " // & xmysql -h 192.168.254.67 -u newuser -p nopassword -d mydb"
        };
        process.StartInfo = startInfo;
        process.Start();
        string cmdOutput = process.StandardOutput.ReadToEnd();
        ViewData["cmdOutput"] = cmdOutput;
    }

    public void OnPost()
    {
        switch (POST_Route) {
            case "main": DynamicGrid(); break;
            case "xmysql": SETUP_XMySQL(); break;
            default: break;
        }
    }

    public void DynamicGrid()
    {
        Console.WriteLine("(✓) parsing: columns & rows . .");
        if (int.TryParse(inputColumns, out bitColumns) && int.TryParse(inputRows, out bitRows)) {
            if (bitColumns > 0 && bitRows > 0) {
                Console.WriteLine($"(✓) received: {bitColumns} columns & {bitRows} rows . .");
                for (int i = 0; i < bitColumns; i++) {
                    _bitColumns += "auto ";
                }
                _bitColumns.Trim();
                Console.WriteLine(_bitColumns);
                createGrid();
            }
            else Console.WriteLine("(X) enter positive integers only . . ");
        }
        else Console.WriteLine("(X) enter integers only . . ");
    }

    public void SETUP_XMySQL() {
        Process process = new();
        ProcessStartInfo startInfo = new() { // WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardInput = false,
            RedirectStandardOutput = false,
            UseShellExecute = false,
            FileName = "cmd.exe",
            Arguments = "/c xmysql --version & xmysql -h 192.168.254.67 -u newuser -p nopassword -d mydb"
        };
        process.StartInfo = startInfo;
        process.Start();
        // string cmdPa = process.StandardOutput.ReadToEnd();
        // ViewData["cmdPa"] = cmdPa;
    }

    public void createGrid()
    {
        Console.WriteLine("(✓) parameters set for BitArray map initialization . .");

        // allocate 3D BitArray & declare new Random
        bitArray = new(bitColumns * bitRows * 2);
        Random rNum = new();

        // do something for each column entry in each row
        for (int i = 0; i < bitRows; i++) {
            Console.WriteLine();
                for (int j = 0; j < bitColumns; j++)
                    if(rNum.Next(7) > 3) {
                        bitArray[(i * bitColumns) + j] = true;
                        Console.Write("🟩");
                    }
                    else
                        Console.Write("⬛");
        }
        Console.WriteLine("\n");

        // map islands
        for (int i = 0; i < bitColumns * bitRows; i++)
            if (!bitArray[i + (bitArray.Length / 2)]) { // Verify it's unchecked
                bitArray[i + (bitArray.Length / 2)] = true; // Mark it as checked
                if (bitArray[i]) { // Check if it's an island
                    Console.WriteLine("Island Set {0:G} . .", ++countIslands);
                    RecursiveIsland(i); // Mark continuously adjacent island bits
                    Thread.Sleep(250);
                    Console.WriteLine();
                }
            }

        Console.WriteLine("Islands: {0:G}", countIslands);
    }

    static void RecursiveIsland(int index)
    {
        bitArray[index + (bitArray.Length / 2)] = true; // Mark it as checked

        if (index % bitColumns != bitColumns - 1) // Verify LEFT bit isn't out of bounds
            if (!bitArray[index + 1 + (bitArray.Length / 2)] && bitArray[index + 1]) { // Verify it's unchecked & island
                Console.Write("0b{0:G} went LEFT, ", index);
                RecursiveIsland(index + 1); // RecursiveIsland it
            }
        if (index + bitColumns < bitArray.Length / 2) // Verify DOWN bit isn't out of bounds
            if (!bitArray[index + bitColumns + (bitArray.Length / 2)] && bitArray[index + bitColumns]) { // Verify it's unchecked & island
                Console.Write("0b{0:G} went DOWN, ", index);
                RecursiveIsland(index + bitColumns); // RecursiveIsland it
            }
        if (index % bitColumns != 0) // Verify RIGHT bit isn't out of bounds
            if (!bitArray[index - 1 + (bitArray.Length / 2)] && bitArray[index - 1]) { // Verify it's unchecked & island
                Console.Write("0b{0:G} went RIGHT, ", index);
                RecursiveIsland(index - 1); // RecursiveIsland it
            }
        if (index - bitColumns > 0) // Verify UP bit isn't out of bounds
            if (!bitArray[index - bitColumns + (bitArray.Length / 2)] && bitArray[index - bitColumns]) { // Verify it's unchecked & island
                Console.Write("0b{0:G} went UP, ", index);
                RecursiveIsland(index - bitColumns); // Run RecursiveIsland
            }
         Console.WriteLine("0b{0:G} goes BACK", index);
    }
}
