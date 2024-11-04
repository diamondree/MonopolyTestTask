using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Models;

namespace MonopolyTestTask.Utils
{
    /// <summary>
    /// Вспомогательный класс для работы с паллетами
    /// </summary>
    public static class PalleteUtils
    {
        /// <summary>
        /// Проверка возможности размещения коллекции предметов на паллете с заданными шириной и глубиной
        /// </summary>
        /// <param name="items">Коллекция предметов</param>
        /// <param name="palleteWidth">Ширина паллета</param>
        /// <param name="palleteDepth">Глубина паллета</param>
        /// <exception cref="ArgumentOutOfRangeException">Исключение по аргументу, если не коробка не проходит по параметрам</exception>
        public static void CheckItemsFitPallete(
            IEnumerable<IWarehouseItem> items,
            double palleteWidth,
            double palleteDepth)
        {
            if (items.Any())
            {
                foreach (var item in items)
                {
                    if (item.Width > palleteWidth)
                        throw new ArgumentOutOfRangeException(nameof(item.Width), $"Item with id {item.Id} has width more pallete");

                    if (item.Depth > palleteDepth)
                        throw new ArgumentOutOfRangeException(nameof(item.Depth), $"Item with id {item.Id} has depth more pallete");
                }
            }
        }

        /// <summary>
        /// Получение срока годности для паллета.
        /// Рассчитывается из минимального срока годности у вложенных предметов
        /// </summary>
        /// <param name="items">Коллекция предметов</param>
        /// <returns>Минимальный срок годности из коллекции</returns>
        public static DateTime GetPalleteExpirationDate(IEnumerable<IWarehouseItem> items)
        {
            var expirationDate = new DateTime();
            if (items.Any())
            {
                expirationDate = items.First().ExpiredDate;
                foreach (var item in items)
                    if (item.ExpiredDate < expirationDate)
                        expirationDate = item.ExpiredDate;
            }

            return expirationDate;
        }

        /// <summary>
        /// Группировка паллет по сроку годности
        /// </summary>
        /// <param name="palletes">Коллекция паллет</param>
        /// <returns>Отсортированный по возрастанию словарь,
        /// в котором ключом является срок годности группы,
        /// значением - список паллет с таким сроком годности</returns>
        public static SortedDictionary<DateTime, List<WarehousePallete>> GroupPalletesByDate(IEnumerable<WarehousePallete> palletes)
        {
            var groupedPalletes = new SortedDictionary<DateTime, List<WarehousePallete>>();

            foreach (var pallete in palletes)
            {
                if (!groupedPalletes.ContainsKey(pallete.ExpiredDate))
                    groupedPalletes.Add(pallete.ExpiredDate, new List<WarehousePallete>());

                groupedPalletes[pallete.ExpiredDate].Add(pallete);
            }

            return groupedPalletes;
        }

        /// <summary>
        /// Сортировка элементов внутри сгруппированных паллетов по весу
        /// </summary>
        /// <param name="groups">Словарь с сгруппированными паллетами</param>
        public static void SortPalleteInGroupsByWeight(IDictionary<DateTime, List<WarehousePallete>> groups)
        {
            foreach (var group in groups.Values)
                group.Sort(new PalleteWeightComparer());
        }

        /// <summary>
        /// Получение трех паллет, у которых максимальный
        /// срок годности предмета из вложенных предметов
        /// </summary>
        /// <param name="palletes">Коллекция паллет</param>
        /// <returns>Список их трех паллет с максимальным сроком годности коробки</returns>
        public static List<WarehousePallete> GetThreePalletesWithLongLiveBoxes(List<WarehousePallete> palletes)
        {
            var result = new List<WarehousePallete>();
            if (palletes.Count > 0)
            {
                result.Add(palletes[0]);
                var minExpDate = new KeyValuePair<DateTime, WarehousePallete>(GetMaxExpiredDateInPallete(palletes[0]), palletes[0]);

                for (int i = 1; i < palletes.Count; i++)
                {
                    if (i < 3)
                    {
                        result.Add(palletes[i]);
                        DateTime maxDateInPallete = GetMaxExpiredDateInPallete(palletes[i]);
                        if (minExpDate.Key > maxDateInPallete)
                        {
                            minExpDate = new KeyValuePair<DateTime, WarehousePallete>(maxDateInPallete, palletes[i]);
                        }
                    }
                    else
                    {
                        DateTime maxDateInPallete = GetMaxExpiredDateInPallete(palletes[i]);
                        if (minExpDate.Key < maxDateInPallete)
                        {
                            result.Remove(minExpDate.Value);
                            result.Add(palletes[i]);

                            minExpDate = GetMinExpDateInList(result);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Сортировка массива паллет по объему
        /// </summary>
        /// <param name="palletes">Массив паллет</param>
        public static void SortPalletesByVolume(List<WarehousePallete> palletes)
            => palletes.Sort(new PalleteVolumeComparer());

        /// <summary>
        /// Получение максимального срока годности из вложенных предметов в паллету
        /// </summary>
        /// <param name="pallete">Паллета для получения максимального срока годности</param>
        /// <returns>Максимальный срок годности в паллете</returns>
        public static DateTime GetMaxExpiredDateInPallete(WarehousePallete pallete)
            => pallete.EnclosedItems.Max(item => item.ExpiredDate);

        /// <summary>
        /// Получение минимального срока годности в списке
        /// </summary>
        /// <param name="list">Список для поиска</param>
        /// <returns>Пара ключ-значение, где ключом является срок годности, а значением паллета</returns>
        public static KeyValuePair<DateTime, WarehousePallete> GetMinExpDateInList(List<WarehousePallete> list)
        {
            var min = new KeyValuePair<DateTime, WarehousePallete>(GetMaxExpiredDateInPallete(list[0]), list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                DateTime maxDateInPallete = GetMaxExpiredDateInPallete(list[i]);

                if (min.Key > maxDateInPallete)
                    min = new KeyValuePair<DateTime, WarehousePallete>(maxDateInPallete, list[i]);
            }

            return min;
        }

        /// <summary>
        /// Вывод информации о сгруппированных и отсортированных паллет на экран.
        /// </summary>
        /// <param name="grouppedPalletes">Сгруппированные паллеты</param>
        /// <param name="separator">Разделитель используется для разделения групп</param>
        public static void PrintSortedPalletes(SortedDictionary<DateTime, List<WarehousePallete>> grouppedPalletes, string separator)
        {
            for (int i = 0; i < grouppedPalletes.Count; i++)
            {
                Console.WriteLine($"Group number: {i + 1}. " +
                                  $"Expired date for group: {grouppedPalletes.ElementAt(i).Key.ToShortDateString()}. " +
                                  $"Group palletes count: {grouppedPalletes.ElementAt(i).Value.Count()}");
                foreach (var pallete in grouppedPalletes.ElementAt(i).Value)
                    Console.WriteLine(pallete.ToString());

                Console.WriteLine(separator);
            }
        }

        /// <summary>
        /// Вывод информации о списке паллет
        /// </summary>
        /// <param name="palletes">Список паллет</param>
        public static void PrintPalletesList(List<WarehousePallete> palletes)
        {
            foreach (var pallete in palletes)
                Console.WriteLine(pallete.ToString());
        }
    }
}
