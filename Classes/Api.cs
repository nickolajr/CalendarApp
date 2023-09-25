using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;

namespace CalendarApp.Classes
{
    public class Api
    {
        public Api()
        {
        }

        public static async Task<List<Holiday>> GetHolidays()
        {
            const string ApiKey = "AIzaSyAsFTUmWwpqX7_WEAiybQzJq5hkonZvZq4";
            const string CalendarId = "da.danish#holiday@group.v.calendar.google.com";

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = "Calendar",
            });

            DateTime now = DateTime.Now;
            int days = 6;
            int test = -1;

            List<Holiday> holidays = new List<Holiday>();

            
                
                DateTime currentDate = now.AddDays(test);
                var request = service.Events.List(CalendarId);
                request.TimeMin = currentDate.Date;
                request.TimeMax = currentDate.Date.AddDays(days);
                request.Fields = "items(summary,start,end)";

                try
                {
                    var response = await request.ExecuteAsync();
                    if (response.Items != null && response.Items.Count > 0)
                    {
                        foreach (var item in response.Items)
                        {
                            // Check if the event is already in the list
                            if (!holidays.Any(h => h.Event == item.Summary && h.Start == Convert.ToDateTime(item.Start.Date)))
                            {
                                holidays.Add(new Holiday
                                {
                                    Event = item.Summary,
                                    Start = Convert.ToDateTime(item.Start.Date)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            

            return holidays;
        }

    }
}
