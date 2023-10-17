using System;
using System.IO;
using JsontTask3;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class JsonToSql
{
    private string _connectionString;
    private Logger _logger;

    public JsonToSql(string connectionString, Logger logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public void ConvertJsonToSql(string jsonFilePath)
    {
        MyConfiguration config = new MyConfiguration();
        string logFilePath = config.GetLogPath();
        Logger logger = new Logger(logFilePath);

        try
        {
            Console.WriteLine("Data reading starts.");// Printing In Console.
            _logger.LogMessage("Data reading starts.");// Giving Message To LogFile.
            string jsonContent = File.ReadAllText(jsonFilePath);
            var jsonData = JsonSerializer.Deserialize<JsonModel[]>(jsonContent); // Use JsonSerializer

            using (var context = new JsonDbContext())
            {
                foreach (var data in jsonData)
                {
                    int id = data.Id;
                    JsonModel existingRecord = context.JsonModels.FirstOrDefault(x => x.Id == id);

                    if (existingRecord != null)
                    {
                        // Update the existing record
                        existingRecord.Name = data.Name;
                        existingRecord.Age = data.Age;
                        existingRecord.Country = data.Country;
                    }
                    else
                    {
                        // Insert a new record
                        JsonModel newRecord = new JsonModel
                        {
                            Name = data.Name,
                            Age = data.Age,
                            Country = data.Country
                        };

                        context.JsonModels.Add(newRecord);
                    }
                }

                context.SaveChanges();
            }

            Console.WriteLine("Data Imported From JSONToSQL successfully.");// Printing In Console.
            _logger.LogMessage("Data Imported From JSONToSQL successfully.");//Giving Message To LogFile.
            Console.WriteLine("Reading Ends");
            // Log the XML content processed
            _logger.LogMessage("Json Content Processed:");//Giving Message To LogFile.

            // Calling Email By Method
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail("From JSONToSQL", " Import Successfull");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Database Update Error: " + ex.Message);
            _logger.LogMessage("Database Update Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            _logger.LogMessage("Error: " + ex.Message);
        }
    }
}
