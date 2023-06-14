using ReceiptScanner;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {

        string jsonFileName = "response.json";

        string solutionDirectory = FindSolutionDirectory();    // Find base directory

        if (string.IsNullOrEmpty(solutionDirectory))
        {
            throw new Exception("File path not found!");
        }

        string jsonFilePath = Path.Combine(solutionDirectory, jsonFileName);   //Create json file path

        string jsonString = File.ReadAllText(jsonFilePath);    // Read json file as string

        var options = new JsonSerializerOptions     // JsonSerializer Options 
        {
            PropertyNameCaseInsensitive = true
        };

        List<ReceiptItem> receiptItems = JsonSerializer.Deserialize<List<ReceiptItem>>(jsonString, options);  // Deserialize json to model( List<ReceiptItem>)

        receiptItems.RemoveAt(0);  // Removed first element due to its including whole receipt.

        var receiptItemsGroups = receiptItems.GroupBy(r => r, new InSameRowChecker()); //Receipt items are groupped according to whether they are in same row or not.

        Console.WriteLine("line\t|\ttext");                                                //Output Header
        Console.WriteLine("------------------------------------------------------");    
        int index = 1;                                                                  //index for output

        foreach (var group in receiptItemsGroups) 
        {
            var sortedRow = group.OrderBy(x => x.BoundingPoly.Vertices.Min(v => v.X)).ToList(); //Receipt items in same row are sorted according to their min X value
            var rowText = string.Join(" ", sortedRow.Select(x => x.Description));  // Receipt items descriptions are concatenated with seperator(' ').
            Console.WriteLine($"{index}\t|\t{rowText}");
            index++;
        }
    }
    private static string FindSolutionDirectory()
    {
        string assemblyLocation = Assembly.GetExecutingAssembly().Location;

        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

        while (assemblyDirectory != null)
        {
            string solutionFile = Directory.GetFiles(assemblyDirectory, "*.sln").FirstOrDefault();
            if (solutionFile != null)
            {
                return Path.GetDirectoryName(solutionFile);
            }
            assemblyDirectory = Directory.GetParent(assemblyDirectory)?.FullName;
        }
        return null;
    }
}








