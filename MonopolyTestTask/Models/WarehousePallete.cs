using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Utils;
using System.Text;

namespace MonopolyTestTask.Models
{
    /// <summary>
    /// Класс склада - паллета
    /// </summary>
    public class WarehousePallete : IWarehouseItem
    {
        private const int _palleteWeight = 30;
        private const string _toStringOffset = "    ";

        /// <summary>
        /// Создание нового экземпляра паллеты.
        /// Может быть создано исключение ArgumentOutOfRangeException,
        /// если ширина/глубина вложенного предмета больше соответствующего
        /// параметра у паллеты
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="depth">Глубина</param>
        /// <param name="enclosedItems">Вложенные предметы</param>
        public WarehousePallete(
            double width,
            double height,
            double depth,
            IEnumerable<IWarehouseItem> enclosedItems) 
        {
            PalleteUtils.CheckItemsFitPallete(enclosedItems, width, depth);

            Id = Guid.NewGuid();
            Width = width;
            Height = height;
            Depth = depth;
            Weight = WarehouseItemUtils.GetItemsTotalWeight(enclosedItems) + _palleteWeight;
            Volume = WarehouseItemUtils.GetItemsTotalVolume(enclosedItems) + WarehouseItemUtils.GetVolume(width,height,depth);
            ExpiredDate = PalleteUtils.GetPalleteExpirationDate(enclosedItems);
            EnclosedItems = enclosedItems;
        }

        public Guid Id { get;}
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public double Volume { get; }
        public DateTime ExpiredDate { get; }
        public IEnumerable<IWarehouseItem> EnclosedItems { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{_toStringOffset}Pallete params with id: {Id}\n" +
                      $"{_toStringOffset}--Width: {Width}, height: {Height}, depth: {Depth}\n" +
                      $"{_toStringOffset}--Expired date: {ExpiredDate.ToShortDateString()}\n" +
                      $"{_toStringOffset}--Volume: {Volume}; Enclosed items count: {EnclosedItems.Count()}\n");

            foreach (var item in EnclosedItems)
                sb.Append(item.ToString());

            return sb.ToString();
        }
    }
}
