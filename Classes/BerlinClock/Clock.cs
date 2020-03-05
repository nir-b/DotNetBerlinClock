using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock.Classes.BerlinClock
{
    /// <summary>
    /// Conceptuallly the entire problem can be decomposed into just three classes The Clock, which is nothing but a collection of ClockRows which in turn has a collection of Lamps
    /// Each ClockRow whether it is Hour, Minute or Second is inherently the same object - they only differ in the number of lamps and their behaviour. 
    /// Once we can isolate this difference in behaviour the mechanism w.r.t lighting them up, our object model becomes quite simple and elegant even.
    /// The function of the Clock class is then to simply distribute the respective time components to each ClockRow and set their individual behaviour.
    /// With each ClockRow then responsible for displaying itself.
    /// </summary>
    public class Clock
    {
        private readonly List<ClockRow> _rows = new List<ClockRow>();

        /// <summary>
        /// Delegates the responsibilty of lighting to each individual row, given its time component.
        /// </summary>
        /// <param name="time">The time string</param>
        /// <returns>A self reference</returns>
        public Clock SetTime(DateTime time)
        {
            _rows.ForEach(r => r.SwitchOn(time));
            //Be fluent
            return this;
        }

        internal void AddClockRow(ClockRow clockRow)
        {
            _rows.Add(clockRow);
        }

        public override string ToString()
        {
            var result = _rows.Aggregate(new StringBuilder(),
                (strb, r) => strb.Length == 0 ? strb.Append(r) : strb.Append(Environment.NewLine).Append(r),
                strb => strb.ToString());
            return result;
        }
    }
}
