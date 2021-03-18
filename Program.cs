// ===============================
// AUTHOR: Nathan Byers
// CREATED ON: Tuesday March 17 2021
// PURPOSE: Coding Exercise. Program.cs creates an output text file
// containing student average data.
// ==================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace student_data_aggregator
{
    public class Program
    {
        private static string defaultInFileDirectory = ".";
        private static string defaultOutFileName = "./out.txt";
        public static int Main(string[] args)
        {            
            var rootCommand = new RootCommand
            {
                new Option(
                    "--files",
                    description: "The location of the files to include in this aggregation."                    
                )
                {
                    Argument = new Argument<string>(),
                    IsRequired = false
                },
                new Option(
                    "--output",
                    description: "The path and filename of the output file."
                )
                
                {
                    Argument = new Argument<string>(),
                    IsRequired = false
                }
            };
            rootCommand.Description = "This application will parse student grades from csv flat files, " + 
            "calculate averages, and display the data in a new txt file.";

            rootCommand.Handler = CommandHandler.Create<string, string>((files, output) =>
            {
                if (string.IsNullOrWhiteSpace(files))
                {
                    files = defaultInFileDirectory;
                }
                if (string.IsNullOrWhiteSpace(output))
                {
                    output = defaultOutFileName;
                }

                var schoolDistrict = new SchoolDistrict(files);
                schoolDistrict.WriteReportToFile(output);
            });

            return rootCommand.Invoke(args);
        }

    }
}
