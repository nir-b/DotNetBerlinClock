namespace BerlinClock.Classes.BerlinClock
{
    public static class ClockExtension
    {
        public static Clock With(this Clock clock, ClockFaceElement clockFaceElement)
        {
            var clockRow = ClockRowFactory.CreateClockRow(clockFaceElement);
            if (clockRow != null)
                clock.AddClockRow(clockRow);
            return clock;
        }
    }
}