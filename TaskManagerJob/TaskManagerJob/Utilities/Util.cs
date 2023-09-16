namespace TaskManagerJob.Utilities
{
    public static class Util
    {
        public static bool IsDateDue(DateTime startTime)
        {
            DateTime endTime = DateTime.UtcNow;
            TimeSpan span = endTime.Subtract(startTime);

            int totalHours = 0;
            if (span.Days > 0)
                totalHours = 24 * span.Days;
            if (span.Hours > 0)
                totalHours = totalHours + span.Hours;

            //  IT SHOULD ONLY SELECT 47 AND 48 HOURS... WE DON'T WANT TO MAKE THE RANGE TOO WIDE
            if (totalHours >= 47 && totalHours < 49)
                return true;
            return false;
        }
    }
}
