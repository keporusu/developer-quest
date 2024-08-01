using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _contributionPopUpPref;
    
    /// <summary>
    /// ポップアップ表示関数
    /// </summary>
    void GenerateContributionPopUp()
    {
        Instantiate(_contributionPopUpPref, transform);
    }
}
