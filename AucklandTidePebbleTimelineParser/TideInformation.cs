using System;

namespace AucklandTidePebbleTimelineParser
{


   public class TideInformation
   {
       public DateTime TideOccurence { get; set; }
       public double HeightMeters { get; set; }
       public string Type { get; set; }
       public int TideNumberThisDay { get; set; }
    }
}
