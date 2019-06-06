﻿using System;

namespace Sunsets.Transactions
{
    /// <summary>
    /// An event that occurs once a week on a specific day of the week.
    /// </summary>
    /// <seealso cref="Sunsets.Transactions.IFrequency"/>
    public class WeeklyEvent : IFrequency
    {
        public WeeklyEvent(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek { get; }

        public int ElapsedEvents(DateTime startDate, DateTime endDate)
        {
            int elapsedTimes = 0;

            DateTime nextDate = CoerceWeekday(startDate);

            while (nextDate <= endDate)
            {
                if (nextDate >= startDate)
                {
                    elapsedTimes++;
                }

                nextDate = nextDate.AddDays(7);
            }

            return elapsedTimes;
        }

        private DateTime CoerceWeekday(DateTime date)
        {
            int difference = (int) DayOfWeek - (int) date.DayOfWeek;

            if (difference < 0)
            {
                difference += 7;
            }

            return date.AddDays(difference);
        }
    }
}