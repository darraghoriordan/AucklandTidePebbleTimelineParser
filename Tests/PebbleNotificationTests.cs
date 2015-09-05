using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AucklandTidePebbleTimelineParser;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
   public class PebbleNotificationTests
    {
       [Test]
       public void CanGetCurlString()
       {
           var tideinfo = new TideInformation()
           {
               Type = "Low",
               HeightMeters = 1.2,
               TideOccurence = new DateTime(2015,09,05,1,45,00),
               TideNumberThisDay = 1
           };
           
           var notificationFactory = new PebbleTimeTideNotificationFactory("testKey");
           var opstring = notificationFactory.GetTideNotification(tideinfo);

           Assert.That(opstring, Is.Not.Null);
       }
    }
}
