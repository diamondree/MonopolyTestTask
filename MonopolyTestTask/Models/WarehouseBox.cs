using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Utils;

namespace MonopolyTestTask.Models
{
    public class WarehouseBox : IWarehouseItem
    {
        private const int _boxLifetimeDays = 100;

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
    }
}
