using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Students
{
    public class StydentsExample
    {
        public static void Run()
        {
            var serialized = StudentSerializerType.Binary;
            var student_Manager = new StudentManager(StudentSerialazer.Create(serialized));

            student_Manager.Add("Ivanov", "ivan", "Ivanovic", DateTime.Now.AddYears(-18));
            student_Manager.Add("Andrey", "Evdakimovich", "Ivanovic", DateTime.Now.AddYears(-18));
            student_Manager.Add("Anton", "Gandon", "Ivanovic", DateTime.Now.AddYears(-18));

            var file_name = $"student.{serialized}";//и тогда мы сможем автоматически указыыать тип сериализации в нашем файле 
            student_Manager.SvaeTo(file_name);
        }
    }
}
