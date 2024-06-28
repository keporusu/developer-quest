using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DataHandler
{
    private ContributionDataHolder _contributionDataHolder;
    private string _today;
    private List<DayContribution> _requiredContributions; //変更がある分のContribution
    private List<DayContribution> _previousContributions; //前回までのContribution
    //private List<DayContribution> _latestContributions; //最新のContribution
    private int _totalContributionsCount;
    
    public DataHandler()
    {
        var _contributionDataHolder = new ContributionDataHolder();
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
        _totalContributionsCount = _contributionDataHolder.GetTotalContributions();
        
        //TODO _requiredContributionを作成する 以前のデータをロードして、今のデータと比較して、今のデータを保存する

        var prevContributionsData = ContributionsDataRepository.Load(); //前回までの記録をすべてロード
        _previousContributions = prevContributionsData.ContributionCalendar;
        _requiredContributions = _makeRequiredContributions();
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
    public IEnumerable<DayContribution> GetRequiredContributions()
    {
        return _requiredContributions;
    }

    /// <summary>
    /// 前回までのContributionのうち、必要な分だけとってくる
    /// </summary>
    /// <param name="need"></param>
    /// <returns></returns>
    public IEnumerable<DayContribution> GetPreviousContributions(int need)
    {
        return _previousContributions.Take(need);
    }



    /// <summary>
    /// ここでは、DayContributionsを引数として与えること。
    /// そっちのほうが直観的。
    /// </summary>
    /// <param name="dayContributions"></param>
    private void _save(IEnumerable<DayContribution> dayContributions)
    {
        ContributionsDataRepository.Save(new ContributionsData(_totalContributionsCount,dayContributions.ToList()));
    }
    private ContributionsData _load()
    {
        return ContributionsDataRepository.Load();
    }

    
    /// <summary>
    /// 起動してない日数分のContributionwoとる
    /// </summary>
    /// <returns></returns>
    private List<DayContribution> _makeRequiredContributions()
    {
        var latestContributions = _contributionDataHolder.GetContributionData().ContributionCalendar;
        List<DayContribution> requiredContributions=new List<DayContribution>();

        var lastContribution = _previousContributions.First(); //
        foreach (var dayContribution in latestContributions)
        {
            if (dayContribution.Day == lastContribution.Day)
            {
                if (dayContribution.Count != lastContribution.Count)
                {
                    requiredContributions.Add(dayContribution);
                }
                break;
            }
            requiredContributions.Add(dayContribution);
        }

        return latestContributions;
    }
    
    
}
