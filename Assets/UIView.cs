using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] private GameObject _contributionPointGage;
    private float _maxCP = 40f;

    public void SetContributionPointGage(int contributionPoint)
    {
        var scale = _contributionPointGage.transform.localScale;
        _contributionPointGage.transform.localScale = new Vector3(contributionPoint / _maxCP,scale.y,scale.z);
    }
}
