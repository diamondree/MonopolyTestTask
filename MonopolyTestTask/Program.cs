using MonopolyTestTask.Models;

namespace MonopolyTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var boxes = new[] { new WarehouseBox(10, 20, 30, 40, new DateTime(2024, 10, 9)), };

            try
            {
                var pallete = new WarehousePallete(10, 20, 30, boxes);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press anything");
            Console.ReadKey();*/
            var aboba = new DateTime(2010, 10, 30);
            var abeba = new DateTime(2010, 10, 19);
            Console.WriteLine(aboba > abeba);
            Console.WriteLine(aboba < abeba);
        }
    }
}
