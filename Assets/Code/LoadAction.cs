using System.Collections;
using System.Collections.Generic;
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
        
        _loginQueryButton.onClick.AddListener(() =>
        {
            dataHandler.Login();
        });
        
        _createDataButton.onClick.AddListener(() =>
        {
            dataHandler.GetContributionsDebug("2024-07-22");
        });
        
        _contributionQueryButton.onClick.AddListener(async () =>
        {
            dataHandler.PullData();
            var x = dataHandler.GetPreviousContributions(7);
            var z = dataHandler.GetTodayContributionsCount();
            var w = dataHandler.IsTodayContributionsChange();
            var y = dataHandler.GetRequiredContributions();
        });
    }
}
