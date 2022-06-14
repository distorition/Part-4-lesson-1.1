using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.HomeWork.Lesson_5;

namespace ConsoleApp1.HomeWork.Lesson_6
{
    internal class ModuleVisitor:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ScanVisitorAsync>().As<IVisitAsync>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => t.Namespace.EndsWith("Lesson 6")).AsImplementedInterfaces();
        }
    }
}
