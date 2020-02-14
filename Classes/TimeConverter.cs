using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            //Here is where we could have made this code a lot more robust, by converting the string object
            //to DateTime using DateTime.TryParse() and using the DateTime object as an argument to Clock
            //We could have used the power of the FCL DateTime class and filtered out any string formatting errors
            //However we cannot really do that since 24:00:00 is an invalid time string as per the DateTime class
            //Also it is very odd and very non-standard to have two representations that is 00:00:00 and 24:00:00 of the 
            //exact same instant of time.
            return new Clock().SetTime(aTime).ToString();
        }
    }
}
