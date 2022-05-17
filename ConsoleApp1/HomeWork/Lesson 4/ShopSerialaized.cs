using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1.HomeWork.Lesson_4
{
    /// <summary>
    /// надеюсь я правильно понял задание 
    /// </summary>
   public interface IShopSerializeed
    {
        void Serialize(Stream textWriter, List<ShopStructure> shopStructures);
        List<ShopStructure> Deserialize(Stream textWriter);
    }
    public abstract class ShopSerialaized : IShopSerializeed
    {
        public abstract List<ShopStructure> Deserialize(Stream textWriter);
        public abstract void Serialize(Stream textWriter, List<ShopStructure> shopStructures);
    }
    internal abstract class XmlSerilaized : ShopSerialaized
    {
        private static readonly XmlSerializer xmlSerializer;
        public override void Serialize(Stream textWriter, List<ShopStructure> shopStructures)
        {
            xmlSerializer.Serialize(textWriter, shopStructures);
        }
        public override List<ShopStructure> Deserialize(Stream stream)
        {
            return (List<ShopStructure>?)xmlSerializer.Deserialize(stream) ?? throw new InvalidOperationException("не смогли получить список товаров ");
        }
    }
    internal abstract class BinarySerialaizer : ShopSerialaized
    {
        private static readonly BinaryFormatter binaryFormatter;
        public override void Serialize(Stream textWriter, List<ShopStructure> shopStructures)
        {
            binaryFormatter.Serialize(textWriter, shopStructures);
        }
        public override List<ShopStructure> Deserialize(Stream textWriter)
        {
            return (List<ShopStructure>?)binaryFormatter.Deserialize(textWriter) ?? throw new InvalidOperationException("не смогли получить товары");
        }
    }   

    internal abstract class JsonSerialazerShop : ShopSerialaized
    {
        
        public override void Serialize(Stream textWriter, List<ShopStructure> shopStructures)
        {
            JsonSerializer.Serialize(textWriter, shopStructures);
        }   
        public override List<ShopStructure> Deserialize(Stream textWriter)
        {
            return JsonSerializer.Deserialize<List<ShopStructure>>(textWriter) ?? throw new InvalidOperationException("не смогли получить список товаров ");
        }
    }
}
