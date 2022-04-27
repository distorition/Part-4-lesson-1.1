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
            var plotter=new PlotBuilde()//флуент интерфейс
                .SetBackground(OxyColors.Peru)
                .SetAsixleftMinorGridLineColor(OxyColors.Pink)
                .Plot(x => Sinc(Math.PI * 2 * x), -5, 5, 0.01, OxyColors.Red, 2)
                .Plot(x => Math.Sin(2*Math.PI  * x), -5, 5,0.01, OxyColors.Green, 1)
                .Plot(x=>Math.Cos(2 * Math.PI * x), -5, 5, 0.01, OxyColors.LimeGreen, 1)
                .CreateModel();

            //теперь у нас есть модель графика и её надо экспортировать
            var exports = new PngExporter(800,600);//в начале указываем ширину , 
            var pngFile = new FileInfo("sinc.png");
            using(var png = pngFile.Create())
            {
                exports.Export(plotter, png);
            }
        }
    }

    public class PlotBuilde
    {
        private readonly List<(Func<double, double> f, double MinX, double MaxX, double stepX, OxyColor color, double StrokeThickness)> Plots
            = new List<(Func<double, double> f, double MinX, double MaxX, double stepX, OxyColor color, double StrokeThickness)>();
        public OxyColor Background { get; set; } = OxyColors.White;
        public OxyColor AsixleftMajorGridLineColor { get; set; } = OxyColors.Gray;
        public OxyColor AsixBottontMajorGridLineColor { get; set; } = OxyColors.Gray;
        public LineStyle AsixleftMajorGridLineStyle { get; set; }=LineStyle.Solid;
        public LineStyle AsixBottontMajorGridLineStyle { get; set; }=LineStyle.Solid;
        public OxyColor AsixleftMinorGridLineColor { get; set; } = OxyColors.LightBlue;
        public OxyColor AsixBottonMinorfGridLineStyle { get; set; }= OxyColors.LightBlue;
        public LineStyle AsixleftMinorrGridLineStyle { get; set; }=LineStyle.Solid;
        public LineStyle AsixBottonMinorGridLineStyle { get; set; }= LineStyle.Solid;

        public PlotBuilde Plot(Func<double,double>f,double MinX, double MaxX,double stepX,OxyColor color, double StrokeThickness)
        {
            Plots.Add((f, MinX, MaxX, stepX, color, StrokeThickness));
            return this;
        }

        public PlotBuilde SetAsixleftMajorGridLineColor(OxyColor color)
        {
            AsixleftMajorGridLineColor = color;
            return this;

        }

        public PlotBuilde SetAsixBottontMajorGridLineColor(OxyColor color)
        {
            AsixBottontMajorGridLineColor = color;
            return this;

        }
        public PlotBuilde SetAsixleftMajorGridLineStyle(LineStyle style)
        {
            AsixleftMajorGridLineStyle = style;
            return this;

        }
        public PlotBuilde SetAsixBottontMajorGridLineStyle(LineStyle style)
        {
            AsixBottontMajorGridLineStyle = style;
            return this;

        }
        public PlotBuilde SetAsixleftMinorGridLineColor(OxyColor color)
        {
            AsixleftMinorGridLineColor = color;
            return this;

        }
        public PlotBuilde SetAsixBottonMinorfGridLineStyle(OxyColor color)
        {
            AsixBottonMinorfGridLineStyle = color;
            return this;

        }
        public PlotBuilde SetAsixleftMinorrGridLineStyle(LineStyle style)
        {
            AsixleftMinorrGridLineStyle = style;
            return this;

        }
        public PlotBuilde SetAsixBottonMinorGridLineStyle(LineStyle style)
        {
            AsixBottonMinorGridLineStyle = style;
            return this;

        }

        public PlotBuilde SetBackground(OxyColor color)
        {
            Background = color;
            return this;
        }
        public PlotModel CreateModel()
        {
             var model = new PlotModel
            {
                //добавляем пару осей(необязательно)
                Axes =
                {
                    new LinearAxis//так же мы можем в осях  устанавливать свои настройки 
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
