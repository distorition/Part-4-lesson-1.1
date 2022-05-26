namespace AgainWebApp.Infastructure.MidleWare
{
    public class TestMidleWare//этот класс является промежуточным По и не откого не наследуется 
    {
        private readonly RequestDelegate next;

        public TestMidleWare(RequestDelegate next )//при помощи делегата (который должен быть обязательно в конструторе ) мы передаем наш запрос дальше 
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context )//сюда будет поступать контекст запроса который пришел нам от пользователя 
        {
            //обработка входящего хапроса 


            //это как реккурсия только асинхронная не запустит цикл и не сожрет много памяти 
            await next(context);//после того как мы обработали запрос мы передаем управление дальше

            // вконце бдет сформирован полностью весь заопрос в context который мы можем до обработать 
        }
    }
    
}
