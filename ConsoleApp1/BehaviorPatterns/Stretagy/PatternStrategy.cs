using MathCore.MathParser;
using OxyPlot;
using OxyPlot.ImageSharp;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PatternStrategy
    {
        public static void Run()
        {
            const string function = "sin(x)+5*cos(2*x)-3";


            const string file_name = "function.png";
            //using (var file = File.Create(file_name))
            //{
            //    var png = new PngExporter(800, 600);
            //    png.Export(model, file);
            //};
            var plotToPng = new PlotToPNG(file_name);
            var plotTOJpg= new PlotToJPG(file_name);
            var calcilator = new FunctionCalculator(plotTOJpg);//сюда передаем нашу стратегию
            calcilator.PlotFunction(function);
        }
    }
    public class FunctionCalculator
    {
        private readonly IPlotStrategy PlotStrategy;
        private ExpressionParser parser = new ExpressionParser();
        public FunctionCalculator(IPlotStrategy plotStrategy)
        {
            PlotStrategy = plotStrategy;
        }
        public void PlotFunction(string Functiontext)
        {

            var xpression = parser.Parse(Functiontext);// тут мы парсим функцию 

            var func = xpression.Compile<Func<double, double>>();//потом компилируем ее в делегат 

            var model = new PlotModel// тут формируем плот модель 
            {
                Background = OxyColors.White,//задаем цвет 
                Series =                     //и указываем ее координаты 
                {
                    new FunctionSeries(func, -3, 3, 0.01)
                    {
                        Color=OxyColors.Red
                    }
                }
            };
            PlotStrategy.Plot(model);
        }
    }

    public interface IPlotStrategy
    {
        public void Plot(IPlotModel model);
    }
    public class PlotToPNG : IPlotStrategy//тут определяем нашу стратегию 
    {
        private readonly string filePath;
        private readonly int width;
        private readonly int height;
        private readonly double resolution;

        public PlotToPNG(string FilePath, int Width = 800, int Height = 600, double Resolution = 96)
        {
            filePath = FilePath;
            width = Width;
            height = Height;
            resolution = Resolution;
        }
        public void Plot(IPlotModel model)
        {
            var file = new FileInfo(filePath);

            using var stream = file.Create();

            var png = new PngExporter(width, height, resolution);
            png.Export(model, stream);
            file.Execute();

        }
    }
    public class PlotToJPG : IPlotStrategy//тут определяем нашу стратегию 
    {
        private readonly string filePath;
        private readonly int width;
        private readonly int height;
        private readonly double resolution;
        private readonly int quality;

        public PlotToJPG(string FilePath, int Width = 800, int Height = 600, double Resolution = 96,int quality=75)
        {
            filePath = FilePath;
            width = Width;
            height = Height;
            resolution = Resolution;
            this.quality = quality;
        }
        public void Plot(IPlotModel model)
        {
            var fileInfo = new FileInfo(filePath);

            using var file = fileInfo.Create();

            var png = new JpegExporter(width, height, resolution, quality);
            png.Export(model, file);
            fileInfo.Execute();

        }
    }
}
