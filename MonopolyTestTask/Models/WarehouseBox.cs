using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Utils;

namespace MonopolyTestTask.Models
{
    /// <summary>
    /// Сущность склада - коробка
    /// </summary>
    public class WarehouseBox : IWarehouseItem
    {
        private const int _boxLifetimeDays = 100;
        private const string _toStringOffset = "        ";
        
        /// <summary>
        /// Конструктор класса коробки
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="depth">Глубина</param>
        /// <param name="weight">Вес</param>
        /// <param name="expirationDate">Срок годности</param>
        public WarehouseBox(
            double width,
            double height,
            double depth,
            double weight,
            DateTime expirationDate)
        {
            Id = Guid.NewGuid();
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            Volume = WarehouseItemUtils.GetVolume(width, height, depth);
            ExpiredDate = expirationDate;
        }

        /// <summary>
        /// Конструктор для указания даты производства коробки. 
        /// Срок годности вычисляется (дата производства +100 дней)
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="depth">Глубина</param>
        /// <param name="weight">Вес</param>
        /// <param name="manufacturedDate">Дата производства</param>
        /// <param name="isManufactureDate">Флаг, указывающий что передается дата производства</param>
        public WarehouseBox(
            double width,
            double height,
            double depth,
            double weight,
            DateTime manufacturedDate,
            bool isManufactureDate) : this (width,height,depth,weight,manufacturedDate.AddDays(_boxLifetimeDays))
        {
        }


        public Guid Id { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public double Volume { get; }
        public DateTime ExpiredDate { get; }

        public override string ToString()
        {
            return $"{_toStringOffset}Box params with id: {Id}\n" +
                   $"{_toStringOffset}--Width: {Width}, height: {Height}, depth: {Depth}\n" +
                   $"{_toStringOffset}--Expired date: {ExpiredDate.ToShortDateString()}\n" +
                   $"{_toStringOffset}--Volume: {Volume}\n";
        }
    }
}
