using MonopolyTestTask.Interfaces;

namespace MonopolyTestTask.Utils
{
    public static class PalleteUtils
    {
        public static double GetItemsTotalWeight(IEnumerable<IWarehouseItem> items)
        {
            double totalWeight = 0;

            foreach (var item in items)
                totalWeight += item.Weight;

            return totalWeight;
        }

        public static double GetItemsTotalVolume(IEnumerable<IWarehouseItem> items)
        {
            double totalVolume = 0;

            foreach (var item in items)
                totalVolume += item.Volume;

            return totalVolume;
        }

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

    }
}
