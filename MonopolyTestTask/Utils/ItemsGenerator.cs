using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Models;

namespace MonopolyTestTask.Utils
{
    /// <summary>
    /// Класс для генерации предметов
    /// </summary>
    public class ItemsGenerator
    {
        // Максимальное значение в рандоме
        private const int _maxGeneratedValue = 100;
        private readonly Random _random;

        public ItemsGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Генерация списка паллетов
        /// </summary>
        /// <param name="itemsCountInsideEveryPallete">
        /// Массив, который содержит количество вложенных элементов
        /// для каждого из паллетов. Количество элементов в массиве ==
        /// количеству паллетов в результирующем списке
        /// </param>
        /// <returns>Список, содержащий случайно сгенерированные паллеты</returns>
        public List<WarehousePallete> GeneratePalletes(int[] itemsCountInsideEveryPallete)
        {
            var result = new List<WarehousePallete>();

            foreach (var boxesInsideCount in itemsCountInsideEveryPallete)
                result.Add(GeneratePallete(boxesInsideCount));

            return result;
        }
        
        /// <summary>
        /// Генерация паллеты
        /// </summary>
        /// <param name="boxesInsideCount">Количество вложенных элементов</param>
        /// <returns>Случайно сгенерированная паллета</returns>
        private WarehousePallete GeneratePallete(int boxesInsideCount)
        {
            var palleteEnclosedItems = new List<IWarehouseItem>();
            var randWidth = _random.Next(_maxGeneratedValue);
            var randDepth = _random.Next(_maxGeneratedValue);
            var randHeight = _random.Next(_maxGeneratedValue);

            for (int i = 0; i < boxesInsideCount; i++)
                palleteEnclosedItems.Add(GenerateBoxFitsSize(randWidth, randDepth));

            return new WarehousePallete(randWidth, randHeight, randDepth, palleteEnclosedItems);
        }

        /// <summary>
        /// Генерация коробки с максимальной шириной и глубиной
        /// </summary>
        /// <param name="width">Максимальная ширина</param>
        /// <param name="depth">Максимальная высота</param>
        /// <returns>Коробка с ограниченными параметрами</returns>
        private IWarehouseItem GenerateBoxFitsSize(double width, double depth)
        {
            var randomWidth = _random.Next((int)width);
            var randDepth = _random.Next((int)depth);
            var randHeight = _random.Next(_maxGeneratedValue);
            var randWeight = _random.Next(_maxGeneratedValue);
            var randDate = RandomDate();

            return new WarehouseBox(randomWidth, randHeight, randDepth, randWeight, randDate);
        }
        
        /// <summary>
        /// Генерация случайной даты
        /// </summary>
        /// <returns>Случайная дата</returns>
        private DateTime RandomDate()
        {
            DateTime start = new DateTime(2024, 10, 25);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
