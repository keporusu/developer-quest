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
            dataHandler.GetContributionsDebug("2024-06-22");
        });
        
        _contributionQueryButton.onClick.AddListener(() =>
        {
            dataHandler.PullData();
            //dataHandler.Get
        });
    }
}
