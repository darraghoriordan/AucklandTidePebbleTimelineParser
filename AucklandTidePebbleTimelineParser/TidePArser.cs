using System;
using System.Collections.Generic;

namespace AucklandTidePebbleTimelineParser
{
    public class TideParser
    {
        public IEnumerable<TideInformation> ParseTides(string[] csvLine)
        {
            var tides = new List<TideInformation>();
            var day = int.Parse(csvLine[0]);
            var month = int.Parse(csvLine[2]);
            var year = int.Parse(csvLine[3]);
            var firstTime = csvLine[4];
            var firstHeight = csvLine[5];
            var secondTime = csvLine[6];
            var secondHeight = csvLine[7];
            var thirdTime = csvLine[8];
            var thirdHeight = csvLine[9];
            //sometimes there isn't a fourth tide in the day
            string fourthTime = string.Empty;
            string fourthHeight = string.Empty;
            if (csvLine.Length > 10)
            {
                fourthTime = csvLine[10];
                fourthHeight = csvLine[11];
            }
            var firstTide = ParseTide(year, month, day, firstTime, firstHeight, 1);
            var secondTide = ParseTide(year, month, day, secondTime, secondHeight, 2);
            var thirdTide = ParseTide(year, month, day, thirdTime, thirdHeight, 3);
            var fourthTide = ParseTide(year, month, day, fourthTime, fourthHeight, 4);

            if (firstTide.HeightMeters > secondTide.HeightMeters)
            {
                firstTide.Type = "High";
                secondTide.Type = "Low";
                thirdTide.Type = "High";
                if (fourthTide != null)
                {
                    fourthTide.Type = "Low";
                }
            }
            else
            {
                firstTide.Type = "Low";
                secondTide.Type = "High";
                thirdTide.Type = "Low";
                if (fourthTide != null)
                {
                    fourthTide.Type = "High";
                }
            }
            tides.Add(firstTide);
            tides.Add(secondTide);
            tides.Add(thirdTide);
            if (fourthTide != null)
            {
                tides.Add(fourthTide);
            }
            return tides;
        }

        public TideInformation ParseTide(int year, int month, int day, string time, string height, int tideNumerThatDay)
        {
            if (string.IsNullOrWhiteSpace(time))
                return null;

            var tideInfo = new TideInformation();

            tideInfo.TideOccurence = GetTideTime(year, month, day, time);
            tideInfo.HeightMeters = double.Parse(height);
            tideInfo.TideNumberThisDay = tideNumerThatDay;
            return tideInfo;
        }

        public DateTime GetTideTime(int year, int month, int day, string time)
        {
            return DateTime.Parse(string.Format("{0}-{1,2}-{2,2}T{3}", year, month, day, time));
        }
    }
}
