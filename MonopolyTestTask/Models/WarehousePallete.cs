using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Utils;
using System.Text;

namespace MonopolyTestTask.Models
{
    public class WarehousePallete : IWarehouseItem
    {
        private const int _palleteWeight = 30;

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
            Weight = PalleteUtils.GetItemsTotalWeight(enclosedItems) + _palleteWeight;
            Volume = PalleteUtils.GetItemsTotalVolume(enclosedItems) + WarehouseItemUtils.GetVolume(width,height,depth);
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
            sb.Append($"Pallete params with id: {Id}\n" +
                      $"--Width: {Width}, height: {Height}, depth: {Depth}\n" +
                      $"--Expired date: {ExpiredDate.ToShortDateString()}\n" +
                      $"--Volume: {Volume}; Enclosed items count: {EnclosedItems.Count()}\n");

            foreach (var item in EnclosedItems)
                sb.Append(item.ToString());

            return sb.ToString();
        }
    }
}
