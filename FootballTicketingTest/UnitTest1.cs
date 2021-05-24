using FootballTicketingAPI.Models;
using FootballTicketingAPI.Models.FootballClub;
using FootballTicketingAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace FootballTicketingTest
{
    [TestClass]
    public class UnitTest1
    {
        FootballTicketingContext footballTicketingContext;
        List<FootballClubModel> entities;
        public void Setup()
        {
            entities = new List<FootballClubModel>();
            entities.Add(new FootballClubModel
            {
                Id = 1,
                Name = "CFR Cluj",
                BadgeURL = "pictures/cfrcluj.png"
            });
            var myDbMoq = new Mock<FootballTicketingContext>();
            myDbMoq.Setup(p => p.FootballClub).Returns(DbContextMock.GetQueryableMockDbSet(entities));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);
            footballTicketingContext = myDbMoq.Object;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Setup();

            var footballClubDbSet = footballTicketingContext.FootballClub;

            var expectedValues = new List<FootballClubModel>();
            expectedValues.Add(new FootballClubModel
            {
                Id = 1,
                Name = "CFR Cluj",
                BadgeURL = "pictures/cfrcluj.png"
            });

            Assert.AreEqual(footballClubDbSet.ToList().First(), expectedValues.First());
        }
    }
}
