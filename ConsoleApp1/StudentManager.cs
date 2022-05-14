using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    internal class StudentManager
    {
        private readonly IStudentSerialazer Serialazer;
        private List<Student> students= new List<Student>();
        private int Count = 1;
        public  StudentManager(IStudentSerialazer studentSerialazer)// так мыб сможем добавлять конкретный сериализатор 
        {
            Serialazer = studentSerialazer;
        }
        public Student Add(string Name,string SecondName,string Patronomic, DateTime dateTime)
        {
            var student = new Student()
            {
                Id = Count++,
                Name = Name,
                SecondName = SecondName,
                Patromic= Patronomic,
                Birthday= dateTime
            };
            students.Add(student);
            return student;
        }

        public FileInfo SvaeTo(string FilePath)
        {
            var file=new FileInfo(FilePath);
           // var xmlSerialaized = new XmlSerializer(typeof(List<Student>));
            using (var xml = file.Create())
            {
                Serialazer.Serialaized(xml, students);  
            }
            return file;

        }
        public void LoadFrom(string FilePath)
        {
            using var xml = File.OpenRead(FilePath);
           students = Serialazer.Diserialaized(xml);   
            if(students.Count == 0)
            {
                return;
            }
            Count = students.Max(students => students.Id )+ 1;
        }
    }
}
