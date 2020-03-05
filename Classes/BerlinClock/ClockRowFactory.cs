using System;

namespace BerlinClock.Classes.BerlinClock
{
    internal static class ClockRowFactory
    {
        #region Static variables and Constants
        private const int TimeMultiple = 5;


        //The following are pure formulas to decide the number of lamps to switch on for each row
        //Switch on 1 lamp if its an even number
        private static readonly Func<int, int> OneLampForEveryEvenNumber = i => i % 2 == 0 ? 1 : 0;
        //Switch on the multiples of the factor number of lamps
        private static readonly Func<int, int> GetCountOfTimeMultiples = i => i / TimeMultiple;
        //Switch on the remainder from the factor number of lamps
        private static readonly Func<int, int> GetRemainderFromTimeMultiples = i => i % TimeMultiple;


        private static readonly Func<int, LampState> SwitchOnLampToRed = _ => LampState.R;
        private static readonly Func<int, LampState> SwitchOnLampToYellow = _ => LampState.Y;
        private static readonly Func<int, LampState> SwitchOnMinuteHandsLamps = i => (i + 1) == 3 || (i + 1) == 6 || (i + 1) == 9 ? LampState.R : LampState.Y;
        #endregion

        internal static ClockRow CreateClockRow(ClockFaceElement clockFaceElement)
        {
            switch (clockFaceElement)
            {
                case ClockFaceElement.SecondsRow:
                    //There is just one seconds lamp for every even number of seconds gets switched on to Yellow
                    return new ClockRow(1, dt => dt.Second, OneLampForEveryEvenNumber, SwitchOnLampToYellow);
                case ClockFaceElement.UpperHourRow:
                    //The first hour row of 4 lamps, each lamp representing <TimeFactor> number of hours and gets set to Red
                    return new ClockRow(4, dt => dt.Hour, GetCountOfTimeMultiples, SwitchOnLampToRed);
                case ClockFaceElement.LowerHourRow:
                    //The second hour row of 4 lamps, each lamp representing 1 hour and gets set to Red
                    return new ClockRow(4, dt => dt.Hour, GetRemainderFromTimeMultiples, SwitchOnLampToRed);
                case ClockFaceElement.UpperMinutesRow:
                    //The first minute row of 11 lamps, each lamp representing <TimeFactor> number of minutes and 
                    //has red lamps in the quarter hour position and the remaining lamps are all yellow
                    return new ClockRow(11, dt => dt.Minute, GetCountOfTimeMultiples, SwitchOnMinuteHandsLamps);
                case ClockFaceElement.LowerMinutesRow:
                    //The second minute row of 4 lamps, each lamp representing 1 minute and gets set to Yellow             
                    return new ClockRow(4, dt => dt.Minute, GetRemainderFromTimeMultiples, SwitchOnLampToYellow);
                default:
                    return null;
            }
        }
    }
}