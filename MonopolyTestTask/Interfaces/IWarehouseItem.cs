namespace MonopolyTestTask.Interfaces
{
    /// <summary>
    /// Интерфейс, представляющий сущность склада
    /// </summary>
    public interface IWarehouseItem
    {
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Глубина
        /// </summary>
        public double Depth { get; }

        /// <summary>
        /// Вес
        /// </summary>
        public double Weight { get; }

        /// <summary>
        /// Объем
        /// </summary>
        public double Volume { get; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime ExpiredDate { get; }

        /// <summary>
        /// Вывод информации в строку о сущности
        /// </summary>
        /// <returns></returns>
        public string ToString();
    }
}
