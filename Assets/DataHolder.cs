using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using Unity.VisualScripting;
using UnityEngine;

public class DataHolder
{

    private GitHubService _gitHubService;
    
    private string _userName;

    private int _totalContributions;
    private ContributionData _contributionData;
    
    
    //初期化: 
    public  DataHolder()
    {
        _gitHubService= GameObject.Find("User").GetComponent<GitHubService>(); 
    }
    
    public async void Login()
    {
        _userName = await _gitHubService.SendLoginQuery();
        Debug.Log(_userName.ToString());
    }

    public async void GetContribution(int need)
    {
        Task<ContributionData> contributionCalendar = _gitHubService.SendContributionsQuery(_userName, need);
        _contributionData = await contributionCalendar;
        Debug.Log(_contributionData.TotalContributions);
        Debug.Log(_contributionData.ContributionCalendar);
    }

}
