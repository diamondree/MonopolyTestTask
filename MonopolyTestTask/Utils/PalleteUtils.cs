using MonopolyTestTask.Interfaces;
using MonopolyTestTask.Models;

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

        public static void SortPalleteInGroupsByWeight(IDictionary<DateTime, List<WarehousePallete>> groups)
        {
            foreach (var group in groups.Values)
                group.Sort(new PalleteWeightComparer());
        }

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

        public static void SortPalletesByVolume(List<WarehousePallete> palletes)
            => palletes.Sort(new PalleteVolumeComparer());

        public static DateTime GetMaxExpiredDateInPallete(WarehousePallete pallete)
            => pallete.EnclosedItems.Max(item => item.ExpiredDate);

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
    }
}
