using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using JsontTask3;

public class SqlToJson
{
    private string _connectionString;
    private Logger _logger;

    public SqlToJson(string connectionString, Logger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public void ExportSqlToJson(string jsonFilePath)
    {
        try
        {
            Console.WriteLine("Data reading starts.");// Printing In Console.
            _logger.LogMessage("Data reading starts.");// Giving Message To LogFile.
            using (var context = new JsonDbContext())
            {
                var data = context.JsonModels.ToList();

                if (data.Count > 0)
                {
                    // Serialize the data into JSON format
                    string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
                    {
                        WriteIndented = true // Optional: Makes the JSON more readable
                    });

                    // Write the JSON data to a file
                    File.WriteAllText(jsonFilePath, jsonData);

                    Console.WriteLine("Data Exported From SQL to JSON successfully.");// Printing In Console.
                }
                else
                {
                    Console.WriteLine("No data found to export.");
                }
                _logger.LogMessage("Data exported From SqlToJson successfully.");// Giving Message To LogFile.

                // Calling Email By Method
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail("From SqlToJson", " Export Successful");//First Is Subject And Second Is Email Message You Can GEt this Message In You Gmail.
            }
            Console.WriteLine("Data reading completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            _logger.LogMessage("Error: " + ex.Message);
        }
    }
}