namespace ReportsExamples.Models
{
    public record struct ProductsCatalog( string name,string Description,DateTime DateTime,IEnumerable<Product> Products);// так же его можно сделать структурой и тогда он будет храниться не в куче а в стеке 

    //public class ProductsCatalog//вот это все заменяет класс Record
    //{
    //    public string Description { get; init; }//init значит  что в это свойвства можно будет записать значения только один раз и потом оно станет только для чтения 
    //    public string Name { get; init; }
    //    public DateTime DateTime { get; init; }

    //    public override string ToString() => $"Name ={Name} Description={Description} CreationDate={DateTime}";

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(Name, Description,DateTime);
    //    }
    //    public override bool Equals(object? obj)
    //    {
    //        if(obj is not ProductsCatalog catalog) return false;
    //        if(GetType()!=catalog.GetType()) return false;

    //        return Name==catalog.Name&& Description==catalog.Description && DateTime==catalog.DateTime;
    //    }

    //}
}
