using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;

namespace student_data_aggregator
{    public class Classroom
    {
        public string ClassName { get; set; }
        public IEnumerable<Student> Students { get; private set; }
        public decimal ClassAverage { get; private set; }

        public Classroom(string filePath)
        {
            CreateFromCsvFile(filePath);
        }

        private void CreateFromCsvFile(string filePath)
        {
            this.ClassName = Path.GetFileNameWithoutExtension(filePath);
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {                
                this.Students = csv.GetRecords<Student>().ToList();                
            }           

            this.ClassAverage = Util.GetAvg(this.Students.Select(s => s.Grade)); 
        }

    }
}

