using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ContributionPopUpView _contributionPopUpPref;
    
    /// <summary>
    /// ポップアップ表示関数
    /// </summary>
    public void GenerateContributionPopUp(int todayContributions, int totalContributions,IList<DayContribution> previous, IList<DayContribution> required)
    {
        var newPopUp = Instantiate(_contributionPopUpPref, transform);

        int blankDays;
        var isLastChanged = (previous.First().Day == required.Last().Day);
        if (isLastChanged) blankDays = 31 - (previous.Count() + required.Count());
        else blankDays = 30 - (previous.Count() + required.Count());
        _contributionPopUpPref.Init(todayContributions, totalContributions, blankDays, previous.Reverse(),required.Reverse(), isLastChanged);
    }
}
