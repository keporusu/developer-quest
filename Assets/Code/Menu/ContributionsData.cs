using System;
using System.Collections;
using System.Collections.Generic;


namespace Code.Menu
{
    public class ContributionsData
    {
        public ContributionsData(int totalContributions, List<DayContribution> contributionCalendar)
        {
            TotalContributions = totalContributions;
            ContributionCalendar = contributionCalendar;
        }
    
        public int TotalContributions; 
        public List<DayContribution> ContributionCalendar;
    }

    public class DayContribution
    {
        public DayContribution(string day, int count)
        {
            Day = day;
            Count = count;
        }
        public string Day;
        public int Count;
    }

}
