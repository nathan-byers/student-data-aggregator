using CsvHelper.Configuration.Attributes;
namespace student_data_aggregator
{    public class Student
    {
        [Name("Student Name")]
        public string StudentName { get; set; }
        public decimal Grade { get; set; }
    }
}
