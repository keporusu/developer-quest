using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContributionPopUpView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _totaltxt;
    [SerializeField] private TextMeshProUGUI _allcount;
    [SerializeField] private Button _okButton;
    [SerializeField] private GameObject _contributionsCalander;
    private void Awake()
    {
        
    }

    void Start()
    {
        _okButton.onClick.AddListener(() => Destroy(this.gameObject));
    }

    
}
