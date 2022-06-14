using AgainWebApp.Services;
using AgainWebApp.Services.Interfaces;
using Autofac;

namespace AgainWebApp.Infastructure.Autofac
{
    public class ServicesModule: Module
    {
        protected override void Load(ContainerBuilder builder)// нужен для того если у нас будет много серввисов чтобы было удобнее их регстрировать
        {
            base.Load(builder); 
            builder.RegisterType<SqlOrderService>().As<IOrderService>().InstancePerLifetimeScope();


            // таким образом при помощи этой строчки будут зарегистрированы сразу все наши сервисы из папки Services 
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)//таким образом мы будем регистрировать все модули из этой сборки 
                .Where(type=>type.Namespace.EndsWith("Services"))//из  сборки мы регистрируем только то где имя простраства заканчивается на Services
                .AsImplementedInterfaces();// а так же регистрируем все интерфейсы которые мы надем в в данной сборки 

        }


    }
}
