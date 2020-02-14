using System;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    /// <summary>
    /// Conceptuallly the entire problem can be decomposed into just three classes The Clock, which is nothing but a collection of ClockRows which in turn has a collection of Lamps
    /// Each ClockRow whether it is Hour, Minute or Second is inherently the same object - they only differ in the number of lamps and their behaviour. 
    /// Once we can isolate this difference in behaviour the mechanism w.r.t lighting them up, our object model becomes quite simple and elegant even.
    /// The function of the Clock class is then to simply distribute the respective time components to each ClockRow and set their individual behaviour.
    /// With each ClockRow then responsible for displaying itself.
    /// </summary>
    internal class ClockRow
    {
        private readonly LampState[] _lamps;
        private readonly Func<int[], int> _timeComponentExtractor;
        private readonly Func<int, int> _numberOfLampsToSwitchOn;
        private readonly Func<int, LampState> _lampSwitchingOnFunc;
        internal ClockRow(int numberOfLamps, Func<int[], int> timeComponentExtractor, Func<int, int> numberOfLampsToSwitchOn, Func<int, LampState> lampSwitchingOnFunc)
        {
            //All lamps are initially switched off
            _lamps = Enumerable.Range(0, numberOfLamps).Select(_ => LampState.O).ToArray();
            
            //The time component this row represents
            _timeComponentExtractor = timeComponentExtractor;

            //Given a time component (hours, minutes, seconds) this decides the number of lamps to switch on
            _numberOfLampsToSwitchOn = numberOfLampsToSwitchOn;

            //Given the number of lamps to switch on this decides to what state (Red or Yellow) should the lamps be switched onto 
            _lampSwitchingOnFunc = lampSwitchingOnFunc;
        }

        internal void SwitchOn(int[] times)
        {
            var lampsToSwitchOn = _numberOfLampsToSwitchOn(_timeComponentExtractor(times));
            for (int i = 0; i < lampsToSwitchOn && i < _lamps.Length; i++)
                _lamps[i] = _lampSwitchingOnFunc(i);
        }

        public override string ToString()
        {
            return _lamps.Aggregate(new StringBuilder(), (strB, state) => strB.Append(state), strB => strB.ToString());
        }
    }
}
