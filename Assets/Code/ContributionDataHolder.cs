using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// データがある場所
/// </summary>
public class ContributionDataHolder
{

    private readonly GitHubService _gitHubService;
    
    private string _userName;
    
    private int _totalContributions;
    private ContributionsData _contributionsData;
    
    //private ContributionData _allContributionData;
    
    
    //初期化: 
    public  ContributionDataHolder()
    {
        _gitHubService = new GitHubService();
        
    }
    
    /// <summary>
    /// とりあえず、ゲーム起動時はこれを実行すること
    /// </summary>
    public async Task Login()
    {
        _userName = await _gitHubService.SendLoginQuery();
        //ebug.Log(_userName.ToString());
    }

    
    /// <summary>
    /// 必要な分だけのContributionを確保する。初回は全部でいいんじゃないか
    /// </summary>
    /// <param name="need">放置した日数+1を入れよう（何も入れないとすべて取得する）</param>
    public async void RequestContributions(int need=-1)
    {
        _contributionsData = await _gitHubService.SendContributionsQuery(_userName, need);
    }
    
    /// <summary>
    /// 今までのContribution数を取得
    /// </summary>
    /// <returns></returns>
    public int GetTotalContributions()
    {
        return _contributionsData.TotalContributions;
    }

    /// <summary>
    /// 取得済みのContributionを返す
    /// </summary>
    /// <returns></returns>
    public ContributionsData GetContributionData()
    {
        return _contributionsData;
    }
    
    /// <summary>
    /// 日にちを渡して、その日のContributionを返す
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public int GetContributionFromDate(string day)
    {
        int count=(_contributionsData.ContributionCalendar.FirstOrDefault(dayInfo => dayInfo.Day == day)).Count;
        if (count == null)
        {
            return 0;
        }
        else
        {
            return count;
        }
    }
    
    
    
    //デバッグ用
    /// <summary>
    /// デバッグ用
    /// </summary>
    /// <param name="endDay">どこからの記録が欲しいか。引っ張ってきたデータより以前の日にちを入れても、引っ張て来たデータ分しかとれない</param>
    /// <returns></returns>
    public ContributionsData GetContributionsDebug(string endDay)
    {
        var list = new List<DayContribution>();
        var count = 0;
        var flag = false;

        var dataTime1 = DateTime.ParseExact(endDay, "yyyy-MM-dd", null);
        var dataTime2 = DateTime.ParseExact(_contributionsData.ContributionCalendar.Last().Day, "yyyy-MM-dd", null);
        if ((dataTime1 - dataTime2).Days < 0) flag = true;
        
        foreach (var x in _contributionsData.ContributionCalendar)
        {
            if (!flag && x.Day == endDay)
            {
                flag = true;
            }

            if (flag)
            {
                list.Add(x);
                count += x.Count;
            }
        }

        var newData = new ContributionsData(count, list);
        ContributionsDataRepository.Save(newData);
        return newData;
    }


}
