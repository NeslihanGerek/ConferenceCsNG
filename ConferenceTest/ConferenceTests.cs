using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferenceCsNG;
using NUnit.Framework;

namespace NunitConferenceTest
{
    [TestFixture]
    public class ConferenceTests
    {
        public TalkContainer talkContainer = new TalkContainer();

        [Test]
        void sortGivenTextInput()
        {
            char[] cities = { 'A', 'B', 'C' };
            int distance = routeCalculations.FixedRouteDistance(cities);
            Assert.AreEqual(distance, 9);
        }
    }
}
