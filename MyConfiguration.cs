using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;
using System.IO;

public class MyConfiguration
{
    public string DirectoryPath { get; private set; }
    public string FileName { get; private set; }
    public string FilePath { get; private set; }
    public string LogPath { get; private set; }

    public MyConfiguration()
    {
        DirectoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
        FileName = ConfigurationManager.AppSettings["jsonFilePath"];
        LogPath = ConfigurationManager.AppSettings["LogFilePath"];

        if (string.IsNullOrEmpty(DirectoryPath) || string.IsNullOrEmpty(FileName))
        {
            throw new ConfigurationErrorsException("DirectoryPath and XmlFilePath configuration values are missing or empty.");
        }

        FilePath = Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"], ConfigurationManager.AppSettings["jsonFilePath"]);

    }

    public string GetFilePath()
    {
        return FilePath;
    }

    public string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
    public string GetLogPath()
    {
        return LogPath;
    }
}