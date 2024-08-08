using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadAction : MonoBehaviour
{
    [SerializeField] private Button _loginQueryButton;
    [SerializeField] private Button _contributionQueryButton;
    [SerializeField] private Button _createDataButton;
    void Start()
    {
        var dataHandler = new DataHandler();
        var uiController = GameObject.Find("UI").GetComponent<UIController>();
        var userPresenter = GameObject.Find("UserPresenter").GetComponent<UserPresneter>();
        
        _loginQueryButton.onClick.AddListener(() =>
        {
            dataHandler.Login();
        });
        
        _createDataButton.onClick.AddListener(() =>
        {
            dataHandler.GetContributionsDebug("2024-08-08");
        });
        
        _contributionQueryButton.onClick.AddListener(async () =>
        {
            dataHandler.PullData();
            var previous = dataHandler.GetPreviousContributions().ToList();
            var todayContribution = dataHandler.GetTodayContributionsCount();
            var contributionChange = dataHandler.TodayContributionsChange();
            var totalContributions = dataHandler.GetTotalContributionsCount();
            var required = dataHandler.GetRequiredContributions().ToList();
            uiController.GenerateContributionPopUp(todayContribution,totalContributions, previous, required);
            
            //TODO: ここのtodayContributionは差分にする（IsTodayContributionsChangeを int TodayContributionsChangeにすればよさげ）
            userPresenter.AddContributionPoint(contributionChange);
        });
    }

    
}
