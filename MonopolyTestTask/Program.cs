using MonopolyTestTask.Models;
using MonopolyTestTask.Utils;

namespace MonopolyTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Разделитель для вывода информации
            var separator = string.Concat(Enumerable.Repeat("-", 100));

            // Генератор случайных предметов
            var generator = new ItemsGenerator();

            // Список паллет
            var palletes = new List<WarehousePallete>();

            // Последовательность для генерации паллет
            // Количество элеметнов в массиве == количеству сгенерированных паллет
            // Каждый элемент массива == количеству вложенных предметов в паллете
            int[] palletesEnclosedItemsCountArray = Enumerable.Range(1, 30).ToArray();

            try
            {
                palletes = generator.GeneratePalletes(palletesEnclosedItemsCountArray);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(-1);
            }

            // Группировка паллет по сроку годности, сортировка и вывод на экран
            SortedDictionary<DateTime, List<WarehousePallete>> grouppedPalletes = PalleteUtils.GroupPalletesByDate(palletes);
            PalleteUtils.SortPalleteInGroupsByWeight(grouppedPalletes);
            PalleteUtils.PrintSortedPalletes(grouppedPalletes, separator);

            // Печать разделителя
            for (int i = 0; i < 3; i++)
                Console.WriteLine(separator);

            // Получение трех паллет с наибольшим сроком годности из вложенных предметов, сортировка по объему и вывод на экран
            List<WarehousePallete> threePalletes = PalleteUtils.GetThreePalletesWithLongLiveBoxes(palletes);
            PalleteUtils.SortPalletesByVolume(threePalletes);
            PalleteUtils.PrintPalletesList(threePalletes);
        }
    }
}
