using NUnit.Framework;
using System.Collections.Generic;
using accommodationsRESTService.Models;
using accommodationsRESTService.JsonModels;
using System;

namespace accommodationsRESTServiceTests
{
    public class InfoAboutAccommodationsTests
    {
        private AllAccommodations allAccommodations;

        [SetUp]
        public void Setup()
        {
            var ac = new Accommodation() { Id = 1, Name = "one" };
            var infoAboutPrice1 = new InfoAboutPrice() { Date = new DateTime(2022, 5, 10), Price = 15 };
            var infoAboutPrice2 = new InfoAboutPrice() { Date = new DateTime(2022, 4, 10), Price = 20 };
            var infoAboutPrice3 = new InfoAboutPrice() { Date = new DateTime(2022, 3, 10), Price = 25 };
            ac.Prices = new List<InfoAboutPrice>();
            ac.Prices.AddRange(new List<InfoAboutPrice>() { infoAboutPrice1, infoAboutPrice2 , infoAboutPrice3});

            var ac2 = new Accommodation() { Id = 2, Name = "second" };
            infoAboutPrice1 = new InfoAboutPrice() { Date = new DateTime(2022, 6, 10), Price = 15 };
            infoAboutPrice2 = new InfoAboutPrice() { Date = new DateTime(2022, 5, 10), Price = 4 };
            infoAboutPrice3 = new InfoAboutPrice() { Date = new DateTime(2022, 4, 10), Price = 21 };
            infoAboutPrice3 = new InfoAboutPrice() { Date = new DateTime(2022, 3, 16), Price = 24 };
            ac2.Prices = new List<InfoAboutPrice>();
            ac2.Prices.AddRange(new List<InfoAboutPrice>() { infoAboutPrice1, infoAboutPrice2, infoAboutPrice3 });

            allAccommodations = new AllAccommodations();
            allAccommodations.Accommodations = new List<Accommodation>();
            allAccommodations.Accommodations.AddRange(new List<Accommodation>() { ac, ac2 });
        }

        [Test]
        [TestCase(3, 24)]
        [TestCase(4, 20)]
        [TestCase(5, 4)]
        [TestCase(6, 15)]
        public void ShouldReturnTheLowestPriceForSpecificMonth(int month, int expectedPrice)
        {
            
            var dict = allAccommodations.GetDictMonthsWithCheapestAcc();

            Assert.AreEqual(expectedPrice, dict[month].Item1); // dict[month].Item1 returns price from tuple
        }


        [Test]
        [TestCase(3, 2)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        public void ShouldReturnCorrectAccommodationIdForSpecificMonth(int month, int expectedAccommodationId)
        {

            var dict = allAccommodations.GetDictMonthsWithCheapestAcc();

            Assert.AreEqual(expectedAccommodationId, dict[month].Item2); // dict[month].Item2 returns accommodations'Id from tuple
        }
    }
}