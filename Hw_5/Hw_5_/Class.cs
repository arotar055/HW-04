using System;
using static System.Console;

namespace Hw_5_CustomDate
{
    class Date
    {
        private int day;
        private int month;
        private int year;

        public int Day
        {
            get { return day; }
            set
            {
                if (IsValidDate(value, month, year))
                {
                    day = value;
                    WriteLine($"Great choice! The day has been set to {day:D2}.");
                }
                else
                {
                    WriteLine($"Oops! {value} is not a valid day for the month {month:D2} and year {year}.");
                }
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                if (IsValidDate(day, value, year))
                {
                    month = value;
                    WriteLine($"Month updated to {month:D2}! Ready for new adventures?");
                }
                else
                {
                    WriteLine($"Sorry, but {value} isn't a valid month for the day {day:D2} and year {year}. Let's try again.");
                }
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                if (IsValidDate(day, month, value))
                {
                    year = value;
                    WriteLine($"Welcome to the year {year}! Hope it's going to be a great one!");
                }
                else
                {
                    WriteLine($"Hmm, {value} doesn't seem right. Could you check the year and try again?");
                }
            }
        }

        public string Day_Of_Week
        {
            get
            {
                int d = day;
                int m = month;
                int y = year;

                if (m < 3)
                {
                    m += 12;
                    y -= 1;
                }

                int K = y % 100;
                int J = y / 100;
                int h = (d + (13 * (m + 1)) / 5 + K + (K / 4) + (J / 4) + 5 * J) % 7;
                int dayOfWeek = ((h + 5) % 7) + 1;

                switch (dayOfWeek)
                {
                    case 1: return "Monday";
                    case 2: return "Tuesday";
                    case 3: return "Wednesday";
                    case 4: return "Thursday";
                    case 5: return "Friday";
                    case 6: return "Saturday";
                    case 7: return "Sunday";
                    default: return "Unknown day";
                }
            }
        }

        public Date()
        {
            day = 1;
            month = 1;
            year = 2000;
            WriteLine("A fresh start! We've set your default date to 01.01.2000.");
        }

        public Date(int day, int month, int year)
        {
            if (IsValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
                WriteLine($"You've set a new date: {day:D2}.{month:D2}.{year}. Exciting times ahead!");
            }
            else
            {
                WriteLine("That date doesn't seem quite right. We've set your date to the default: 01.01.2000.");
                this.day = 1;
                this.month = 1;
                this.year = 2000;
            }
        }

        private bool IsValidDate(int day, int month, int year)
        {
            if (year < 1 || month < 1 || month > 12)
                return false;

            int[] daysInMonth =
            {
                31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };

            if (day < 1 || day > daysInMonth[month - 1])
                return false;

            return true;
        }

        private bool IsLeapYear(int year)
        {
            if (year % 400 == 0)
                return true;
            else if (year % 100 == 0)
                return false;
            else if (year % 4 == 0)
                return true;
            else
                return false;
        }

        public int DifferenceInDays(Date other)
        {
            int totalDays1 = CountDays(this);
            int totalDays2 = CountDays(other);

            return Math.Abs(totalDays1 - totalDays2);
        }

        private int CountDays(Date date)
        {
            int days = date.day;

            for (int y = 1; y < date.year; y++)
            {
                days += IsLeapYear(y) ? 366 : 365;
            }

            int[] daysInMonth =
            {
                31, IsLeapYear(date.year) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };

            for (int m = 1; m < date.month; m++)
            {
                days += daysInMonth[m - 1];
            }

            return days;
        }

        public void AddDays(int daysToAdd)
        {
            int totalDays = CountDays(this) + daysToAdd;

            int newYear = 1;
            while (true)
            {
                int daysInYear = IsLeapYear(newYear) ? 366 : 365;
                if (totalDays > daysInYear)
                {
                    totalDays -= daysInYear;
                    newYear++;
                }
                else
                {
                    break;
                }
            }

            int[] daysInMonth =
            {
                31, IsLeapYear(newYear) ? 29 : 28, 31, 30, 31, 30,
                31, 31, 30, 31, 30, 31
            };
            int newMonth = 1;
            while (true)
            {
                if (totalDays > daysInMonth[newMonth - 1])
                {
                    totalDays -= daysInMonth[newMonth - 1];
                    newMonth++;
                }
                else
                {
                    break;
                }
            }

            day = totalDays;
            month = newMonth;
            year = newYear;
        }

        public void PrintDate()
        {
            WriteLine($"The current date is: {day:D2}.{month:D2}.{year}. Enjoy the moment!");
        }

      
    }
}
