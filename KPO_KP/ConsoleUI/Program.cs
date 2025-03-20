namespace ConsoleUI
{
    /// <summary>
    /// Точка входа в приложение. Создаёт меню и запускает его.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var menu = new MainMenu();
            menu.Run();
        }
    }
}