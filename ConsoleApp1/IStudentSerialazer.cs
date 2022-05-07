using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public interface IStudentSerialazer//штука для одоптации всех трех лкасс Сериализации 
    {
        void Serialaized(Stream textWriter,List<Student> students);
       List<Student> Diserialaized(Stream textReader);

    }
    public abstract class StudentSerialazer : IStudentSerialazer//мы реализовали абстрактный класс за тем чтобы реализовать в нем какие то общие методы
    {
        public abstract List<Student> Diserialaized(Stream textReader);
        public abstract void Serialaized(Stream textWriter, List<Student> students);
    }

    internal class XmlStudentSerialazer : StudentSerialazer//класс для сериализации в формате Xml
    {
        private static readonly XmlSerializer xmlSerilazer= new XmlSerializer(typeof(List<Student>));// XmlSerialazer всегда очень долго создается 
        public override List<Student> Diserialaized(Stream textReader)
        {
            return (List<Student>?)xmlSerilazer.Deserialize(textReader) ?? throw new InvalidOperationException("Не удалось получить список студентов ");//если в наш список будет равен Null то мы получим ошибку 
        }

        public override void Serialaized(Stream textWriter, List<Student> students)
        {
            xmlSerilazer.Serialize(textWriter, students);
        }
    }

    internal class BinarySerialazedStudent : StudentSerialazer// класс для сериализации в формате  Binary
    {
        private static readonly BinaryFormatter xmlSerilazer = new BinaryFormatter();
        public override List<Student> Diserialaized(Stream textReader)
        {
            return (List<Student>?)xmlSerilazer.Deserialize(textReader) ?? throw new InvalidOperationException("Не удалось получить список студентов ");//если в наш список будет равен Null то мы получим ошибку 
        }

        public override void Serialaized(Stream textWriter, List<Student> students)
        {
            xmlSerilazer.Serialize(textWriter, students);
        }
    }

    /// <summary>
    /// тут есть проблема ибо джейсон почему то не принимает лист со студентами
    /// </summary>
    //internal class JsonSerialazedStudent : StudentSerialazer// класс для сериализации в формате  Json
    //{
    
    //    public override List<Student> Diserialaized(Stream textReader)
    //    {
    //        return JsonSerializer.Deserialize<List<Student>>(textReader) ?? throw new InvalidOperationException("Не удалось получить список студентов ");//если в наш список будет равен Null то мы получим ошибку 
    //    }
    

    //    public override void Serialaized(Stream textWriter, List<Student>students)
    //    {
           
    //        JsonSerializer.Serialize(textWriter, students);
    //    }
    //}

}
