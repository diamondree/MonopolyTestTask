namespace MonopolyTestTask.Interfaces
{
    public interface IWarehouseItem
    {
        public Guid Id { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public double Volume { get; }
        public DateTime ExpiredDate { get; }
    }
}
