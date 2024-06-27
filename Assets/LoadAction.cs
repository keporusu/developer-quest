using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAction : MonoBehaviour
{
    [SerializeField] private Button _loginQueryButton;
    [SerializeField] private Button _contributionQueryButton;
    void Start()
    {
        var dataHolder = new ContributionDataHolder();
        
        _loginQueryButton.onClick.AddListener(() =>
        {
            dataHolder.Login();
        });
        _contributionQueryButton.onClick.AddListener(() =>
        {
            dataHolder.RequestContributions(7);
        });
    }
}
