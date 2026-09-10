using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDataProcessor
{
    // ============= OOP: Base Class =============
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected Person(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    // ============= OOP: Inheritance =============
    public class Student : Person
    {
        public string Department { get; set; }
        public List<double> Grades { get; set; } = new List<double>();

        public Student(int id, string name, string department, List<double> grades)
            : base(id, name)
        {
            Department = department;
            Grades = grades;
        }

        public double AverageGrade => Grades.Any() ? Grades.Average() : 0;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# OOP & LINQ Data Processing Demo ===\n");

            // ============= Mock Data Collection 
            List<Student> students = new List<Student>
            {
                new Student(1, "Alex", "Computer Science", new List<double> { 90, 85, 88 }),
                new Student(2, "Anna", "Computer Science", new List<double> { 95, 92, 98 }),
                new Student(3, "David", "Economics", new List<double> { 75, 80, 72 }),
                new Student(4, "Maria", "Economics", new List<double> { 88, 91, 85 }),
                new Student(5, "John", "Engineering", new List<double> { 60, 65, 70 })
            };

            // 1. LINQ Filtering & Projection (High Performers) =============
            Console.WriteLine("--- Top Performing Students (Avg > 85) ---");
            var topStudents = students
                .Where(s => s.AverageGrade > 85)
                .OrderByDescending(s => s.AverageGrade)
                .Select(s => new { s.Name, s.Department, Avg = s.AverageGrade });

            foreach (var student in topStudents)
            {
                Console.WriteLine($"Student: {student.Name} | Department: {student.Department} | Avg Grade: {student.Avg:F2}");
            }

            // 2. LINQ Grouping (Average per Department) =============
            Console.WriteLine("\n--- Department Performance Summary ---");
            var departmentStats = students
                .GroupBy(s => s.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    StudentCount = g.Count(),
                    OverallAvg = g.Average(s => s.AverageGrade)
                });

            foreach (var stat in departmentStats)
            {
                Console.WriteLine($"Department: {stat.Department} | Count: {stat.StudentCount} | Dept Avg: {stat.OverallAvg:F2}");
            }

            Console.WriteLine("\nProcessing completed successfully.");
            Console.ReadLine();
        }
    }
}
