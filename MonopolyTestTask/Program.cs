using MonopolyTestTask.Models;
using MonopolyTestTask.Utils;

namespace MonopolyTestTask
{
    internal class Program
    {
        private const string _separator = "------------------------------------------------";

        static void Main(string[] args)
        {
            
            var boxesCombo1 = new[] 
            {
                new WarehouseBox(10, 20, 30, 40, new DateTime(2024, 11, 20)),
                new WarehouseBox(5, 30, 15, 100, new DateTime(2024, 10,10), true),
                new WarehouseBox(5, 100, 10, 70, new DateTime(2024, 11, 10))
            };

            var boxesCombo2 = new[]
            {
                new WarehouseBox(10, 50, 50, 40, new DateTime(2024,11,10)),
                new WarehouseBox(10, 50, 50, 40, new DateTime(2025,11,10)),
            };

            var boxesCombo3 = new[]
            {
                new WarehouseBox(10,10,10,10, new DateTime(2024, 11, 20))
            };

            var boxesCombo4 = new[]
            {
                new WarehouseBox(5,5,5,5, new DateTime(2025,01,01))
            };

            var boxesCombo5 = new[]
{
                new WarehouseBox(25,25,45,55, new DateTime(2026,01,01))
            };

            var palletes = new List<WarehousePallete>();

            try
            {
                palletes.Add(new WarehousePallete(10, 10, 10, boxesCombo4));
                palletes.Add(new WarehousePallete(10, 20, 30, boxesCombo1));
                palletes.Add(new WarehousePallete(20, 5, 50, boxesCombo2));
                palletes.Add(new WarehousePallete(30, 30, 30, boxesCombo3));
                palletes.Add(new WarehousePallete(50, 50, 50, boxesCombo5));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(-1);
            }

            SortedDictionary<DateTime, List<WarehousePallete>> grouppedPalletes = PalleteUtils.GroupPalletesByDate(palletes);
            PalleteUtils.SortPalleteInGroupsByWeight(grouppedPalletes);
            PrintSortedPalletes(grouppedPalletes);

            for (int i = 0; i < 3; i++)
                Console.WriteLine(_separator);

            List<WarehousePallete> threePalletes = PalleteUtils.GetThreePalletesWithLongLiveBoxes(palletes);
            PalleteUtils.SortPalletesByVolume(threePalletes);
            PrintPalletesList(threePalletes);
        }

        private static void PrintSortedPalletes(SortedDictionary<DateTime,List<WarehousePallete>> grouppedPalletes)
        {
            for (int i = 0; i < grouppedPalletes.Count; i++)
            {
                Console.WriteLine($"Group number: {i + 1}. Expired date for group: {grouppedPalletes.ElementAt(i).Key.ToShortDateString()}");
                foreach (var pallete in grouppedPalletes.ElementAt(i).Value)
                    Console.WriteLine(pallete.ToString());

                Console.WriteLine(_separator);
            }
        }

        private static void PrintPalletesList(List<WarehousePallete> palletes) 
        {
            foreach (var pallete in palletes)
                Console.WriteLine(pallete.ToString());
        }
    }
}
