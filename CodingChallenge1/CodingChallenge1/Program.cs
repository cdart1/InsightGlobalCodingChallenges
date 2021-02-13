using System;
using System.Collections.Generic;

namespace CodingChallenge1
{
    class Program
    {
        /* Determine the leap years in a 20 year time span.
         * 
         * A leap year occurs approximately every four years, where February 
         * will have 29 days instead of 28.
         */
        static void Main(string[] args)
        {
            // User inputs the current year.
            Console.WriteLine("Please enter the current calendar year (YYYY): ");
            int currYear = Convert.ToInt32(Console.ReadLine());
            List<int> leapYrList = FindLeapYears(currYear);
            Console.WriteLine("\nBelow are the leap years within the next 20 years:");
            foreach (int lp in leapYrList)
            {
                Console.WriteLine(lp);
            }
        }
        /// <summary>
        /// Determines which years are leap years in a 20 year time span.
        /// </summary>
        /// <param name="currYear"></param>
        /// <returns></returns>
        public static List<int> FindLeapYears(int currYear)
        {
            List<int> leapYrList = new List<int>();
            // The amount of years to go through to find leap years.
            int timeSpan = 0;
            while (timeSpan != 20)
            {
                // if 'daysInFeb' is 29, 'currYear' is a leapYear.
                int daysInFeb = DateTime.DaysInMonth(currYear, 2);
                if (daysInFeb == 29)
                {
                    leapYrList.Add(currYear);
                }
                currYear += 1;
                timeSpan += 1;
            }
            return leapYrList;
        }
    }
}
