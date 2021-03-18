using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace student_data_aggregator
{
    public class SchoolDistrict 
    {
        public List<Classroom> Classrooms { get; set; }
        public decimal OverallDistrictAverage { get; set; }

        public SchoolDistrict(string inputFilesPath)
        {
            CreateFromCsvFiles(inputFilesPath);
        }

        private void CreateFromCsvFiles(string inputFilesPath)
        {
            this.Classrooms = new List<Classroom>();

            foreach(var filePath in Directory.EnumerateFiles(inputFilesPath))
            {
                this.Classrooms.Add(new Classroom(filePath));
            }

            this.OverallDistrictAverage = Util.GetAvg(this.Classrooms.SelectMany(c => c.Students.Select(s => s.Grade)));
        }

        public void WriteReportToFile(string outputFilePath)
        {
            using (StreamWriter outputFile = new StreamWriter(outputFilePath))
            {
                outputFile.WriteLine("=======================================================");
                outputFile.WriteLine("HIGHEST PERFORMING CLASS");                
                outputFile.WriteLine($"Class Name: {this.Classrooms.OrderByDescending(c => c.ClassAverage).First().ClassName}");
                outputFile.WriteLine($"Class Average: {this.Classrooms.OrderByDescending(c => c.ClassAverage).First().ClassAverage.ToString("0.0")}");
                outputFile.WriteLine("=======================================================");
                outputFile.WriteLine();
                outputFile.WriteLine($"Overall student average: {this.OverallDistrictAverage}");               

                foreach(var classroom in this.Classrooms)
                {
                    outputFile.WriteLine();
                    outputFile.WriteLine("----------------------------------------------------");
                    outputFile.WriteLine($"Class Name: {classroom.ClassName}");
                    outputFile.WriteLine($"Class Average: {classroom.ClassAverage}");
                    outputFile.WriteLine($"Total number of students: {classroom.Students.Count()}");
                    outputFile.WriteLine($"Number of students used to calculate average: {classroom.Students.Where(s => s.Grade >= 1).Count()}");
                    outputFile.WriteLine("Students discarded from consideration:");
                    foreach(var student in classroom.Students.Where(s => s.Grade < 1))
                    {
                        outputFile.WriteLine(student.StudentName);
                    }
                }
            }
        }
    }
}