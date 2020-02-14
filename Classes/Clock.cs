using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BerlinClock
{
    public enum LampState
    {
        O,
        Y,
        R
    }
    /// <summary>
    /// Conceptuallly the entire problem can be decomposed into just three classes The Clock, which is nothing but a collection of ClockRows which in turn has a collection of Lamps
    /// Each ClockRow whether it is Hour, Minute or Second is inherently the same object - they only differ in the number of lamps and their behaviour. 
    /// Once we can isolate this difference in behaviour the mechanism w.r.t lighting them up, our object model becomes quite simple and elegant even.
    /// The function of the Clock class is then to simply distribute the respective time components to each ClockRow and set their individual behaviour.
    /// With each ClockRow then responsible for displaying itself.
    /// </summary>
    public class Clock
    {
        #region Static variables and Constants
        private const int TimeMultiple = 5;

        private const int HoursIndex = 0;
        private const int MinutesIndex = 1;
        private const int SecondsIndex = 2;
        

        private static readonly Func<int[], int> GetSeconds = i => i[SecondsIndex];
        private static readonly Func<int[], int> GetHours = i => i[HoursIndex];
        private static readonly Func<int[], int> GetMinutes = i => i[MinutesIndex];

        //The following are pure formulas to decide the number of lamps to switch on for each row
        //Switch on 1 lamp if its an even number
        private static readonly Func<int, int> OneLampForEveryEvenNumber = i => i % 2 == 0 ? 1 : 0;
        //Switch on the multiples of the factor number of lamps
        private static readonly Func<int, int> GetCountOfTimeMultiples = i => i / TimeMultiple;
        //Switch on the remainder from the factor number of lamps
        private static readonly Func<int, int> GetRemainderFromTimeMultiples = i => i % TimeMultiple;


        private static readonly Func<int, LampState> SwitchOnLampToRed = _ => LampState.R;
        private static readonly Func<int, LampState> SwitchOnLampToYellow = _ => LampState.Y;
        #endregion

        private List<ClockRow> _rows;
        public Clock()
        {
            CreateClockRows();
        }

        /// <summary>
        /// This is where we essentially "create" the clock
        /// Setting up each clock row should feel like a declarative statement.
        /// For example create a row with 4 lamps representing hours, where the number of lamps to light is given by the remainder term and switch on the lamps to Red
        /// </summary>
        private void CreateClockRows()
        {
            _rows = new List<ClockRow>
            {
                //There is just one seconds lamp for every even number of seconds gets switched on to Yellow
                new ClockRow(1, GetSeconds, OneLampForEveryEvenNumber, SwitchOnLampToYellow),

                //The first hour row of 4 lamps, each lamp representing <TimeFactor> number of hours and gets set to Red
                new ClockRow(4, GetHours, GetCountOfTimeMultiples, SwitchOnLampToRed),

                //The second hour row of 4 lamps, each lamp representing 1 hour and gets set to Red
                new ClockRow(4, GetHours, GetRemainderFromTimeMultiples, SwitchOnLampToRed),

                //The first minute row of 11 lamps, each lamp representing <TimeFactor> number of minutes and 
                //has red lamps in the quarter hour position and the remaining lamps are all yellow
                new ClockRow(11, GetMinutes, GetCountOfTimeMultiples, i => (i + 1) == 3 || (i + 1) == 6 || (i + 1) == 9? LampState.R : LampState.Y),

                //The second minute row of 4 lamps, each lamp representing 1 minute and gets set to Yellow             
                new ClockRow(4, GetMinutes, GetRemainderFromTimeMultiples, SwitchOnLampToYellow),
            };
        }

        /// <summary>
        /// Delegates the responsibilty of lighting to each individual row, given its time component.
        /// </summary>
        /// <param name="time">The time string</param>
        /// <returns>A self reference</returns>
        public Clock SetTime(String time)
        {
            var timeArray = time.Split(':').Select(s => int.Parse(s)).ToArray();
            _rows.ForEach(r => r.SwitchOn(timeArray));
            //Be fluent
            return this;
        }

        public override string ToString()
        {
            var result = _rows.Aggregate(new StringBuilder(), (strb, r) => strb.Length == 0 ? strb.Append(r): strb.Append(Environment.NewLine).Append(r), strb => strb.ToString());
            return result;
        }
    }
}
