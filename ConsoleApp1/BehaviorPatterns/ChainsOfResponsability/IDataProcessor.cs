using MathCore.MathParser;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BehaviorPatterns.ChainsOfResponsability
{
    public interface IDataProcessor
    {
        object Process(object data);// на вход поступает обьект а на выходе тоже обьект 
    }
    public class FunctionProcessor
    {
        private List<IDataProcessor> _Blocks = new List<IDataProcessor>();
        public FunctionProcessor Adds(IDataProcessor data)
        {
            _Blocks.Add(data);
            return this;//будет возвращать сам класс
        }

        public object  Process(string function)//сюда передаем нашу функцию
        {
            object value=function;
            foreach(var block in _Blocks)
            {
                value= block.Process(value);//а тут мы вызываем каждый наш блок и складываем ее в анше значения 
            }
            return value;
        }
    }
    class FunctionParser : IDataProcessor
    {
        private readonly ExpressionParser _Parser = new ExpressionParser();

        public object Process(object data)
        {
            var str = (string)data;
            var expression=_Parser.Parse(str);//это итерация (ей можно заменить рекурсию чтобы секономить места на стеки )
            return expression;
        }
    }
    class ExpressionCompiler : IDataProcessor
    {
        public object Process(object data)
        {
            var expression = (MathExpression)data;
            var func= expression.Compile<Func<double, double>>();
            return func;    
        }
    }
    class PlotModelGenerator : IDataProcessor
    {
        public object Process(object data)
        {
            var func= (Func<double, double>)data;
            var model = new PlotModel
            {
                Background = OxyColors.White,
                Series =
                {
                    new FunctionSeries(func, -3, 3, 0.01)
                    {
                        Color=OxyColors.Red
                    }
                }

            };
            return model;
        }
    }
    class Exporter : IDataProcessor
    {
        private IPlotStrategy plotterStretagy;
        public Exporter(IPlotStrategy plotterStretagy)
        {
            this.plotterStretagy = plotterStretagy;
        }
        public object Process(object data)
        {
            var model=(PlotModel)data;
            plotterStretagy.Plot(model);
            return null;
        }
    }
}
