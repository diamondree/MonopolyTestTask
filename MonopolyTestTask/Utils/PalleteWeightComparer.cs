using MonopolyTestTask.Models;

namespace MonopolyTestTask.Utils
{
    public class PalleteWeightComparer : IComparer<WarehousePallete>
    {
        public int Compare(WarehousePallete? x, WarehousePallete? y)
        {
            return x.Weight.CompareTo(y.Weight);
        }
    }
}
