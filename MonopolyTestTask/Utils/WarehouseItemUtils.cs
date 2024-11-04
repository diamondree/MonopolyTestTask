using MonopolyTestTask.Interfaces;

namespace MonopolyTestTask.Utils
{
    /// <summary>
    /// Utils`ы для предмета со склада
    /// </summary>
    public static class WarehouseItemUtils
    {
        /// <summary>
        /// Вычисление объема
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="depth">Глубина</param>
        /// <returns>Объем</returns>
        public static double GetVolume(double width, double height, double depth)
            => width * height * depth;

        /// <summary>
        /// Получение общего веса предметов
        /// </summary>
        /// <param name="items">Массив предметов</param>
        /// <returns>Общий вес предметов для коллекции</returns>
        public static double GetItemsTotalWeight(IEnumerable<IWarehouseItem> items)
        {
            double totalWeight = 0;

            foreach (var item in items)
                totalWeight += item.Weight;

            return totalWeight;
        }

        /// <summary>
        /// Получение общего объема предметов
        /// </summary>
        /// <param name="items">Массив предметов</param>
        /// <returns>Общий объем для коллекции</returns>
        public static double GetItemsTotalVolume(IEnumerable<IWarehouseItem> items)
        {
            double totalVolume = 0;

            foreach (var item in items)
                totalVolume += item.Volume;

            return totalVolume;
        }

    }
}
