using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BehaviorPatterns.ChainsOfResponsability
{
    public static class ChainsOfResponsabilityExample
    {
        /// <summary>
        /// пример последовательной обработки информации (цепочка обязанностей )
        /// </summary>
        public static void Run()
        {
            const string Filename = "ChainsOfResponsabilityExample.png";
            var processor= new FunctionProcessor()
                .Adds(new FunctionParser())
                .Adds(new ExpressionCompiler())
                .Adds(new PlotModelGenerator())
                .Adds(new Exporter(new PlotToPNG(Filename)));
            const string function = "sin(x)+5*cos(2*x)-3";
            processor.Process(function);

        }
    }
}
