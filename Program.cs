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
    // create list of characters
    // lists are used since we do not know number of lines of data
    // to populate the list with data, read from the data file
    List<Character> characters = [];
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
                Character character = new();
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                character.Id = UInt64.Parse(characterDetails[0]);
                // 2nd array element contains character name
                character.Name = characterDetails[1] ?? string.Empty;
                // 3rd array element contains character description
                character.Description = characterDetails[2] ?? string.Empty;
                characters.Add(character);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
/*
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
            Console.WriteLine("Enter new character name: ");
            string? Name = Console.ReadLine();
            if (!string.IsNullOrEmpty(Name)){
                // check for duplicate name
                List<string> LowerCaseNames = Names.ConvertAll(n => n.ToLower());
                if (LowerCaseNames.Contains(Name.ToLower()))
                {
                    logger.Info($"Duplicate name {Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    UInt64 Id = Ids.Max() + 1;
                    // input character description
                    Console.WriteLine("Enter description:");
                    string? Description = Console.ReadLine();
                    // Console.WriteLine($"{Id}, {Name}, {Description}");
                    // create file from data
                    StreamWriter sw = new(file, true);
                    sw.WriteLine($"{Id},{Name},{Description}");
                    sw.Close();
                    // add new character details to Lists
                    Ids.Add(Id);
                    Names.Add(Name);
                    Descriptions.Add(Description);
                    // log transaction
                    logger.Info($"Character id {Id} added");
                }
            } else {
                logger.Error("You must enter a name");
            }
        }
        else if (choice == "2")
        {
            // Display All Characters
            // loop thru Lists
            for (int i = 0; i < Ids.Count; i++)
            {
                // display character details
                Console.WriteLine($"Id: {Ids[i]}");
                Console.WriteLine($"Name: {Names[i]}");
                Console.WriteLine($"Description: {Descriptions[i]}");
                Console.WriteLine();
            }
        }
    } while (choice == "1" || choice == "2");
*/
}

logger.Info("Program ended");
