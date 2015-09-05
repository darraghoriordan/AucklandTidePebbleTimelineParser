using System.Collections.Generic;
using System.Linq;
using AucklandTidePebbleTimelineParser;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TideParserTests
    {
        [Test]
        public void can_parse_dates()
        {
            var tideParser = new TideParser();
            var day = 6;
            var month = 9;
            var year = 2015;
            var time = "00:59";
            
            var result = tideParser.GetTideTime(year, month, day, time);

            Assert.That( result.Day, Is.EqualTo(day));
            Assert.That(result.Month, Is.EqualTo(month));
            Assert.That(result.Year, Is.EqualTo(year));
            Assert.That(result.TimeOfDay.Hours, Is.EqualTo(0));
            Assert.That(result.TimeOfDay.Minutes, Is.EqualTo(59));
           
        }
        [Test]
        public void can_parse_tide()
        {
            var tideParser = new TideParser();
            var day = 6;
            var month = 9;
            var year = 2015;
            var time = "00:59";

            var result = tideParser.ParseTide(year, month, day, time,"0.9",1);

            Assert.That(result.TideOccurence.Year, Is.EqualTo(year));
            Assert.That(result.HeightMeters, Is.EqualTo(0.9));
            Assert.That(result.TideNumberThisDay, Is.EqualTo(1));
        }
        [Test]
        public void can_parse_tides()
        {
            var tideParser = new TideParser();
            var csvLine = new List<string>()
            {
                "6",
                "Su",
                "9",
                "2015",
                "00:59",
                "3.2",
                "06:59",
                "0.5",
                "13:27",
                "3.1"
            };

            var result = tideParser.ParseTides(csvLine.ToArray());
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.ElementAt(0).TideOccurence.Year, Is.EqualTo(2015));
            Assert.That(result.ElementAt(0).TideOccurence.Month, Is.EqualTo(9));
            Assert.That(result.ElementAt(0).TideOccurence.Day, Is.EqualTo(6));
            Assert.That(result.ElementAt(0).TideOccurence.TimeOfDay.Hours, Is.EqualTo(0));
            Assert.That(result.ElementAt(0).TideOccurence.TimeOfDay.Minutes, Is.EqualTo(59));
            Assert.That(result.ElementAt(0).HeightMeters, Is.EqualTo(3.2));
            Assert.That(result.ElementAt(0).TideNumberThisDay, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Type, Is.EqualTo("High"));

            Assert.That(result.ElementAt(1).TideOccurence.TimeOfDay.Hours, Is.EqualTo(6));
            Assert.That(result.ElementAt(1).TideOccurence.TimeOfDay.Minutes, Is.EqualTo(59));
            Assert.That(result.ElementAt(1).HeightMeters, Is.EqualTo(0.5));
            Assert.That(result.ElementAt(1).TideNumberThisDay, Is.EqualTo(2));
            Assert.That(result.ElementAt(1).Type, Is.EqualTo("Low"));
        }
        [Test]
        public void can_parse_the_fourth_tide()
        {
            var tideParser = new TideParser();
            var csvLine = new List<string>()
            {
                "6",
                "Su",
                "9",
                "2015",
                "00:59",
                "3.2",
                "06:59",
                "0.5",
                "13:27",
                "3.1",
                "19:34",
                "0.7"
            };

            var result = tideParser.ParseTides(csvLine.ToArray());
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.That(result.ElementAt(3).TideOccurence.Year, Is.EqualTo(2015));
            Assert.That(result.ElementAt(3).TideOccurence.Month, Is.EqualTo(9));
            Assert.That(result.ElementAt(3).TideOccurence.Day, Is.EqualTo(6));
            Assert.That(result.ElementAt(3).TideOccurence.TimeOfDay.Hours, Is.EqualTo(19));
            Assert.That(result.ElementAt(3).TideOccurence.TimeOfDay.Minutes, Is.EqualTo(34));
            Assert.That(result.ElementAt(3).HeightMeters, Is.EqualTo(0.7));
            Assert.That(result.ElementAt(3).TideNumberThisDay, Is.EqualTo(4));
            Assert.That(result.ElementAt(3).Type, Is.EqualTo("Low"));

        }
    }
}
