using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Menu
{
    public class LoadAction : MonoBehaviour
    {
        [SerializeField] private Button _loginQueryButton;
        [SerializeField] private Button _contributionQueryButton;
        [SerializeField] private Button _createDataButton;
        [SerializeField] private string endDayForDebug;
        void Start()
        {
            SceneTransition.Initialize();
            SceneTransition.Come();
            
            
            var dataHandler = new DataHandler();
            var uiController = GameObject.Find("UI").GetComponent<UIController>();
            var userPresenter = GameObject.Find("UserPresenter").GetComponent<UserPresenter>();
        
            _loginQueryButton.onClick.AddListener(() =>
            {
                dataHandler.Login();
            });
        
            _createDataButton.onClick.AddListener(() =>
            {
                dataHandler.GetContributionsDebug(endDayForDebug);
            });
        
            _contributionQueryButton.onClick.AddListener(async () =>
            {
                dataHandler.PullData();
                var previous = dataHandler.GetPreviousContributions().ToList();
                var todayContribution = dataHandler.GetTodayContributionsCount();
                var contributionChange = dataHandler.TodayContributionsChange();
                var totalContributions = dataHandler.GetTotalContributionsCount();
                var required = dataHandler.GetRequiredContributions().ToList();

                var requiredCount = required.Select(x => x.Count).Sum();
                
                uiController.GenerateContributionPopUp(requiredCount,totalContributions, previous, required);
                userPresenter.AddContributionPoint(requiredCount);
            });
        }

    
    }
}

