using System.Collections.Generic;
using System.Linq;

namespace AucklandTidePebbleTimelineParser
{
    public class PebbleTimeTideNotificationFactory
    {
        private readonly string _apiKey;

        public PebbleTimeTideNotificationFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public IEnumerable<string> GetTideNotifications(IEnumerable<TideInformation> tideInfos)
        {
            return tideInfos.Select(GetTideNotification);

        }


        public string GetTideNotification(TideInformation tideInfo)
        {
            var notificationTitle = string.Format("tide-auckland-{0}-{1}", tideInfo.TideOccurence.ToString("yyyyMMdd"),
                tideInfo.TideNumberThisDay);
            var curlString =
                string.Format(
                    "curl -X PUT https://timeline-api.getpebble.com/v1/shared/pins/{0} --header 'Content-Type: application/json' --header 'X-API-Key: {1}' --header 'X-Pin-Topics: auckland'", notificationTitle, _apiKey);
            curlString = curlString + " -d '{\"id\": \"" + notificationTitle + "\",\"time\": \"" +
                         tideInfo.TideOccurence.ToString("yyyy-MM-ddTHH:mm:ss+12:00") +
                         "\",\"layout\": {\"type\": \"genericPin\",\"title\": \"" + tideInfo.Type + " Tide (" +
                         tideInfo.HeightMeters + "m)\",\"tinyIcon\": \"system://images/NOTIFICATION_FLAG\"}}'";
            return curlString;
        }
    }
}
