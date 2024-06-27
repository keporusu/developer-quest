using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler
{
    private ContributionDataHolder _contributionDataHolder;
    private string _today;
    private List<DayContribution> _requiredContributions; //変更がある分のContribution
    
    public DataHandler()
    {
        var _contributionDataHolder= new ContributionDataHolder();
        var _today = DateTime.Today.ToString("yyyy-MM-dd");
    }
    
    /// <summary>
    /// ログイン時に実行
    /// </summary>
    /// <param name="isFirst">初回起動なら</param>
    public void Login(bool isFirst=false)
    {
        _contributionDataHolder.Login();
        if(isFirst)_contributionDataHolder.RequestContributions();
        else _contributionDataHolder.RequestContributions(7);
        
        //TODO _requiredContributionを作成する
    }
    
    /// <summary>
    /// 今日のContribution数を返す
    /// </summary>
    /// <returns></returns>
    public int GetTodayContributionsCount()
    {
        return _contributionDataHolder.GetContributionFromDate(_today);
    }
    
    /// <summary>
    /// 今日のContributionsが変化したかどうかを返す（変化してたらアニメーション必要だね）
    /// </summary>
    /// <returns></returns>
    public bool IsTodayContributionsChange()
    {
        return _requiredContributions.Count != 0;
    }
    
    /// <summary>
    /// 起動していない日数分のContributionを得る
    /// </summary>
    /// <returns></returns>
    public IEnumerable<DayContribution> GetNeedContributions()
    {
        return _requiredContributions;
    }

    
    void Save()
    {
        
    }
    
    
}
