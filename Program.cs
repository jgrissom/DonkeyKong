﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

string file = "dk.csv";
// make sure movie file exists
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
    // create parallel lists of character details
    // lists are used since we do not know number of lines of data
    List<UInt64> Ids = [];
    List<string> Names = [];
    List<string> Descriptions = [];
    // to populate the lists with data, read from the data file
    try
    {
        StreamReader sr = new(file);
        // first line contains column headers
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                Ids.Add(UInt64.Parse(characterDetails[0]));
                // 2nd array element contains character name
                Names.Add(characterDetails[1]);
                // 3rd array element contains character description
                Descriptions.Add(characterDetails[2]);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
    string? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            // Add Character
        }
        else if (choice == "2")
        {
            // Display All Characters
        }
    } while (choice == "1" || choice == "2");
}

logger.Info("Program ended");
