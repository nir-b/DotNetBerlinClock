using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BerlinClock.Classes.BerlinClock;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            //No Exception handling because the exceptions should be rethrown back at the client
            var input = DateTime.Parse(aTime);
            var clock = new Clock()
                .With(ClockFaceElement.SecondsRow)
                .With(ClockFaceElement.UpperHourRow)
                .With(ClockFaceElement.LowerHourRow)
                .With(ClockFaceElement.UpperMinutesRow)
                .With(ClockFaceElement.LowerMinutesRow);
            return clock.SetTime(input).ToString();
        }
    }
}
