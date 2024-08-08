using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _contributionPopUpPref;
    [SerializeField] private UIView _uiView;

    private bool _isPopUpDisabled = false;
    private bool _isFirstGageSetting = true;
    

    /// <summary>
    /// ポップアップ表示関数
    /// </summary>
    public async void GenerateContributionPopUp(int todayContributions, int totalContributions,IList<DayContribution> previous, IList<DayContribution> required)
    {
        var newPopUp = Instantiate(_contributionPopUpPref, transform);

        int blankDays;
        var isLastChanged = previous.Any() && required.Any() && (previous.First().Day == required.Last().Day);
        if (isLastChanged) blankDays = 31 - (previous.Count() + required.Count());
        else blankDays = 30 - (previous.Count() + required.Count());
        
        //ポップアップが消えた後に、ゲージが増える
        _isPopUpDisabled = await newPopUp.GetComponent<ContributionPopUpView>().Init(todayContributions, totalContributions, blankDays,
            previous.Reverse(), required.Reverse(), isLastChanged);
    }

    public async void SetContributionPointGage(int contributionPoint)
    {
        await UniTask.WaitUntil(() => _isPopUpDisabled ^  _isFirstGageSetting); //最初 or ポップアップが死んでいるなら
        _uiView.SetContributionPointGage(contributionPoint);
        _isFirstGageSetting = false;
    }
}
