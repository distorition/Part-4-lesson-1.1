using OxyPlot;
using OxyPlot.Axes;
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
    internal class BuilderPattern
    {
        public static double Sinc(double x) => Math.Sin(x) / x;  
        public static void Run()
        {
            var model = new PlotModel
            {
                //добавляем пару осей(необязательно)
                Axes =
                {
                    new LinearAxis//так же мы можем в осинах устанавливать свои настройки 
                    {
                    Position = AxisPosition.Left,
                    MajorGridlineColor=OxyColors.Gray,
                    MajorGridlineStyle=LineStyle.Solid,
                    MinorGridlineColor=OxyColors.LightGray,
                    MinorGridlineStyle=LineStyle.Dash
                    },
                    new LinearAxis 
                    {

                    Position = AxisPosition.Right,
                    MajorGridlineColor=OxyColors.Gray,
                    MajorGridlineStyle=LineStyle.Solid,
                    MinorGridlineColor=OxyColors.LightGray,
                    MinorGridlineStyle=LineStyle.Dash
                    },
                },
                Background = OxyColors.White,

                //добавляемм графики (можно обойтись чисто этим без всего того что сверху)
                Series =
                {
                    new FunctionSeries(x => Sinc(2 * Math.PI * x), -5, 5, 0.1, "Sinc(x)")//в начале указываем функцию, интервал , шаг 
                    {
                        //тут настройки самого графика 
                        Color= OxyColors.Red,
                        StrokeThickness=2,
                    }

                }
            };

            //теперь у нас есть модель графика и её надо экспортировать
            var exports = new PngExporter(800,600);//в начале указываем ширину , 
            var pngFile = new FileInfo("sinc.png");
            using(var png = pngFile.Create())
            {
                exports.Export(model, png);
            }
        }
    }

    public class PlotBuilde
    {
        private readonly List<(Func<double, double> f, double MinX, double MaxX, double stepX, OxyColor color, double StrokeThickness)> Plots
            = new List<(Func<double, double> f, double MinX, double MaxX, double stepX, OxyColor color, double StrokeThickness)>(); 
        public OxyColor Background { get; set; } 
        public OxyColor AsixleftMajorGridLineColor { get; set; }
        public OxyColor AsixBottontMajorGridLineColor { get; set; }
        public LineStyle AsixleftMajorGridLineStyle { get; set; }
        public LineStyle AsixBottontMajorGridLineStyle { get; set; }
        public OxyColor AsixleftMinorGridLineColor { get; set; }
        public OxyColor AsixBottonMinorfGridLineStyle { get; set; }
        public LineStyle AsixleftMinorrGridLineStyle { get; set; }
        public LineStyle AsixBottonMinorGridLineStyle { get; set; }

        public void Plot(Func<double,double>f,double MinX, double MaxX,double stepX,OxyColor color, double StrokeThickness)
        {
            Plots.Add((f, MinX, MaxX, stepX, color, StrokeThickness));
        }

        public PlotModel CreateModel()
        {
             var model = new PlotModel
            {
                //добавляем пару осей(необязательно)
                Axes =
                {
                    new LinearAxis//так же мы можем в осинах устанавливать свои настройки 
                    {
                    Position = AxisPosition.Left,
                    MajorGridlineColor=AsixleftMajorGridLineColor,
                    MajorGridlineStyle=AsixleftMajorGridLineStyle,
                    MinorGridlineColor=AsixleftMinorGridLineColor,
                    MinorGridlineStyle=AsixleftMinorrGridLineStyle
                    },
                    new LinearAxis 
                    {

                    Position = AxisPosition.Bottom,
                    MajorGridlineColor=AsixBottontMajorGridLineColor,
                    MajorGridlineStyle=AsixBottontMajorGridLineStyle,
                    MinorGridlineColor=AsixBottonMinorfGridLineStyle,
                    MinorGridlineStyle=AsixBottonMinorGridLineStyle
                    },
                },
                Background = Background,

               
            };
            foreach(var (f,min,max,dx,color,thickes) in Plots)
            {
                model.Series.Add(new FunctionSeries(f, min, max, dx) { Color=color,StrokeThickness=thickes});
            }
            return model;
        }
    }
}
