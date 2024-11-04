using MonopolyTestTask.Models;

namespace MonopolyTestTask.Utils
{
    /// <summary>
    /// Класс для сравнения паллет по весу
    /// </summary>
    public class PalleteWeightComparer : IComparer<WarehousePallete>
    {
        public int Compare(WarehousePallete? x, WarehousePallete? y)
        {
            return x.Weight.CompareTo(y.Weight);
        }
    }

    /// <summary>
    /// Класс для сравнения паллет по объему
    /// </summary>
    public class PalleteVolumeComparer : IComparer<WarehousePallete>
    {
        public int Compare(WarehousePallete? x, WarehousePallete? y)
        {
            return x.Volume.CompareTo(y.Volume);
        }
    }
}
