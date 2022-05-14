using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static/* async Task */void Main(string[] args)
        {

            //var currentThread = Thread.CurrentThread;
            //currentThread.Priority = ThreadPriority.Highest;

            //while (true)
            //{
            //    Console.Title = DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss:ffff");
            //    Thread.Sleep(100);
            //}

            //ThreadTest.Run();
            //CriticalSection.Run();
            //  SynchornizationTest.Run();
            //  ThreadPoolTest.Run();
            // ParalelTest.Run();
            //ThreadSafeDictinary.Run();
            //var ttask= AsyncAwaitTest.RunAsync();// таким образом если выскочит ошибка то она будет хранится в переменной и не положит нашу программу 
            // await AsyncAwaitTest.Run2Async();
            //LogingExamples.Run();
            //BuilderPattern.Run();

            //var student = new Student()
            //{
            //    Name = "Ivanov",
            //    SecondName = "Ivan",
            //    Id = 1,
            //    Patromic = "Ivanovich",
            //    Birthday =  DateTime.Now.AddYears(-18)
            //};
            //var xmlSer = new XmlSerializer(typeof(Student));//сериализуем нащего студента
            //const string xmlFileName = "student.xml";
            //using (var xmlFile = File.Create(xmlFileName))
            //    xmlSer.Serialize(xmlFile, student);

            //Student? student1 = null;
            //using (var xmlFile = File.OpenText(xmlFileName))
            //{
            //    student1=(Student?) xmlSer.Deserialize(xmlFile);
            //}

            //var xmlSerialezd = new XmlStudentSerialazer();
            //var binarySerialaized = new BinarySerialazedStudent();

            //таким образом мы сможем указывать тип сериализации прямо в конструкторе 
            //var student_Manager= new StudentManager(StudentSerialazer.Xml());

            //либо же указать тип сериализации
            var serialized = StudentSerializerType.Binary;
           var student_Manager = new StudentManager(StudentSerialazer.Create(serialized));

            student_Manager.Add("Ivanov","ivan","Ivanovic",DateTime.Now.AddYears(-18));
            student_Manager.Add("Andrey", "Evdakimovich", "Ivanovic", DateTime.Now.AddYears(-18));
            student_Manager.Add("Anton", "Gandon", "Ivanovic", DateTime.Now.AddYears(-18));

            var file_name = $"student.{serialized}";//и тогда мы сможем автоматически указыыать тип сериализации в нашем файле 
            student_Manager.SvaeTo(file_name);
            //XmlSerializer serializer = new XmlSerializer(typeof(Student));
            //BinaryFormatter binary= new BinaryFormatter();
        }
    }
}
