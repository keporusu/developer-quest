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
    private ContributionData _contributionData;
    
    //private ContributionData _allContributionData;
    
    
    //初期化: 
    public  ContributionDataHolder()
    {
        _gitHubService = new GitHubService();
        
    }
    
    /// <summary>
    /// とりあえず、ゲーム起動時はこれを実行すること
    /// </summary>
    public async void Login()
    {
        _userName = await _gitHubService.SendLoginQuery();
        //ebug.Log(_userName.ToString());
    }

    
    /// <summary>
    /// 必要な分だけのContributionを得られる。初回は全部でいいんじゃないか
    /// </summary>
    /// <param name="need">放置した日数+1を入れよう（何も入れないとすべて取得する）</param>
    public async void RequestContributions(int need=-1)
    {
        Task<ContributionData> contributionCalendar = _gitHubService.SendContributionsQuery(_userName, need);
        _contributionData = await contributionCalendar;
        //Debug.Log(_contributionData.TotalContributions);
        //Debug.Log(_contributionData.ContributionCalendar);
    }

    public int GetTotalContributions()
    {
        return _totalContributions;
    }

    public ContributionData GetContributionData()
    {
        return _contributionData;
    }

    public int GetContributionFromDate(string day)
    {
        var count=(_contributionData.ContributionCalendar.FirstOrDefault(dayInfo => dayInfo.Day == day)).Count;
        if (count == null)
        {
            return 0;
        }
        else
        {
            return count;
        }
    }


}
