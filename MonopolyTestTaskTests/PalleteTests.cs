using MonopolyTestTask.Models;
using MonopolyTestTask.Utils;

namespace MonopolyTestTaskTests
{
    public class PalleteTests
    {
        [TestCase(15,10)]
        [TestCase(10,15)]
        [TestCase(15,15)]
        public void PalleteCreationTestFails(double width, double depth)
        {
            Assert.Catch<ArgumentOutOfRangeException>(() => new WarehousePallete(10, 10, 10, [CreateWarehouseBoxWithParametrizedWidthDepth(width,depth)]));
        }

        [TestCase(10,10)]
        [TestCase(5,5)]
        public void PalleteCreationTestSuccess(double width, double depth)
        {
            Assert.DoesNotThrow(() => new WarehousePallete(10, 10, 10, [CreateWarehouseBoxWithParametrizedWidthDepth(width,depth)]));
        }

        [Test]
        public void PalleteGroupingByDateTests()
        {
            var listPalletes = new List<WarehousePallete>()
            {
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,10)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,10)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,10)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,11)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,11)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,10,9)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,11,10)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,8,10)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,11,15)),
                CreatePalleteWithOneDatedBox(new DateTime(2012,11,01)),
            };

            var result = PalleteUtils.GroupPalletesByDate(listPalletes);

            Assert.That(() => result.ContainsKey(new DateTime(2012, 10, 10)) &&
                              result[new DateTime(2012,10,10)].Count == 3 &&
                              result.ContainsKey(new DateTime(2012, 10, 11)) && 
                              result[new DateTime(2012, 10, 11)].Count == 2 &&
                              result.ContainsKey(new DateTime(2012, 10, 9)) &&
                              result[new DateTime(2012, 10, 9)].Count == 1 &&
                              result.ContainsKey(new DateTime(2012,11,10)) &&
                              result[new DateTime(2012, 11, 10)].Count == 1 &&
                              result.ContainsKey(new DateTime(2012,8,10)) &&
                              result[new DateTime(2012, 8, 10)].Count == 1 &&
                              result.ContainsKey(new DateTime(2012, 11, 15)) &&
                              result[new DateTime(2012, 11, 15)].Count == 1 &&
                              result.ContainsKey(new DateTime(2012, 11, 01)) &&
                              result[new DateTime(2012, 11, 01)].Count == 1);
        }

        [Test]
        public void MaxExpiredDateInPalleteTest()
        {
            var pallete = new WarehousePallete(10, 10, 10, [CreateBoxWothParametrizedDate(new DateTime(2012,10,15)),
                                                            CreateBoxWothParametrizedDate(new DateTime(2013,1,30)),
                                                            CreateBoxWothParametrizedDate(new DateTime(2015,1,18))]);

            DateTime result = PalleteUtils.GetMaxExpiredDateInPallete(pallete);

            Assert.That(new DateTime(2015, 1, 18), Is.EqualTo(result));
        }

        [Test]
        public void MinExpiredDateInListTest()
        {
            var palletes = new[]{new WarehousePallete(10, 10, 10, [CreateBoxWothParametrizedDate(new DateTime(2012,10,15)),
                                                                   CreateBoxWothParametrizedDate(new DateTime(2013,1,30)),
                                                                   CreateBoxWothParametrizedDate(new DateTime(2015,1,18))]),
                                new WarehousePallete(10, 10, 10, [CreateBoxWothParametrizedDate(new DateTime(2015,1,16)),
                                                                  CreateBoxWothParametrizedDate(new DateTime(2015,1,15))])};

            KeyValuePair<DateTime, WarehousePallete> result = PalleteUtils.GetMinExpDateInList(palletes.ToList());

            Assert.That(new DateTime(2015, 1, 16), Is.EqualTo(result.Key));
        }

        private WarehouseBox CreateWarehouseBoxWithParametrizedWidthDepth(double width, double depth) 
        {
            return new WarehouseBox(width, 10, depth, 10 , new DateTime());
        }

        private WarehouseBox CreateBoxWothParametrizedDate(DateTime date)
        {
            return new WarehouseBox(10,10,10,10, date);
        }

        private WarehousePallete CreatePalleteWithOneDatedBox(DateTime boxDate)
        {
            return new WarehousePallete(10,10,10, [CreateWarehouseBoxWithDate(boxDate)]);
        }

        private WarehouseBox CreateWarehouseBoxWithDate(DateTime date)
        {
            return new WarehouseBox(10, 10, 10, 10, date);
        }
    }
}