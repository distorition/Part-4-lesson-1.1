using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]// нам нужен этот атрибут для Binary сериализации ибо он старый  и требует наличие этого арибута 
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Patromic { get; set; }
        public DateTime  Birthday { get; set; }
    }
}
