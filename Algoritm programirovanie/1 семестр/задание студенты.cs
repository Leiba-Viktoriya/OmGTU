using System;
using System.Collections.Generic;
using System.Linq;

class Grade
{
    public string SubjectName { get; set; }
    public int Value { get; set; }

    public Grade(string subjectName, int value)
    {
        SubjectName = subjectName;
        Value = value;
    }
}

class Student
{
    public string FullName { get; set; }
    public Grade[] Grades { get; set; }

    public Student(string fullName, Grade[] grades)
    {
        FullName = fullName;
        Grades = grades;
    }

    public double AverageGrade()
    {
        return Grades.Average(g => g.Value);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Student> students = new List<Student>();
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.WriteLine("1. Add student");
            Console.WriteLine("2. Show students with average grade above 4");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(students);
                    break;
                case "2":
                    ShowHighAchievingStudents(students);
                    break;
                case "3":
                    continueProgram = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    static void AddStudent(List<Student> students)
    {
        Console.Write("Enter student's full name: ");
        string fullName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fullName))
        {
            Console.WriteLine("Full name cannot be empty.");
            return;
        }

        Console.Write("Enter number of grades: ");
        if (!int.TryParse(Console.ReadLine(), out int numberOfGrades) || numberOfGrades <= 0)
        {
            Console.WriteLine("Invalid number of grades.");
            return;
        }

        Grade[] grades = new Grade[numberOfGrades];

        for (int i = 0; i < numberOfGrades; i++)
        {
            Console.Write($"Enter subject name for grade {i + 1}: ");
            string subjectName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(subjectName))
            {
                Console.WriteLine("Subject name cannot be empty.");
                i--;
                continue;
            }
            int gradeValue;
            do
            {
                Console.Write($"Enter grade for {subjectName} (2-5): ");
                if (!int.TryParse(Console.ReadLine(), out gradeValue) || gradeValue < 2 || gradeValue > 5)
                {
                    Console.WriteLine("Invalid grade value. Please enter a value between 2 and 5.");
                }
                else break;
            } while (true);


            grades[i] = new Grade(subjectName, gradeValue);
        }

        students.Add(new Student(fullName, grades));
        Console.WriteLine("Student added.");
    }

    static void ShowHighAchievingStudents(List<Student> students)
    {
        var highAchievingStudents = students.Where(s => s.AverageGrade() > 4).ToList();

        if (highAchievingStudents.Count == 0)
        {
            Console.WriteLine("No students with an average grade above 4.");
            return;
        }

        Console.WriteLine("Students with an average grade above 4:");
        foreach (var student in highAchievingStudents)
        {
            Console.WriteLine($"{student.FullName} - Average grade: {student.AverageGrade():F2}");
            Console.WriteLine("Grades:");
            foreach (var grade in student.Grades)
            {
                Console.WriteLine($"   {grade.SubjectName}: {grade.Value}");
            }
        }
    }
}