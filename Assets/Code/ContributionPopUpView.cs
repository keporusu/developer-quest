using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    /// <param name="allCount"></param>
    /// <param name="blankDays">30日を超えてどれだけログインしていなかったか</param>
    /// <param name="previous"></param>
    /// <param name="required"></param>
    public void Init(int count, int allCount, int blankDays, IEnumerable<DayContribution> previous, IEnumerable<DayContribution> required, bool isLastChanged)
    {
        _count.text = count.ToString();
        _allcount.text = allCount.ToString();
        
        var childObjects = GetDirectChildrenImages().ToList();
        int index = 0;

        // previousの内容を反映
        foreach (var contribution in previous)
        {
            if (index >= childObjects.Count) break;
            ApplyContribution(childObjects[index], contribution);
            index++;
        }

        if (isLastChanged && index > 0)
        {
            // 最後のpreviousを上書き
            index--;
        }

        // requiredの内容を反映
        foreach (var contribution in required)
        {
            if (index >= childObjects.Count) break;
            ApplyContribution(childObjects[index], contribution);
            index++;
        }
    }
    
    //ボタンクリック時
    void Start()
    {
        _okButton.onClick.AddListener(() => Destroy(this.gameObject));
    }
    
    
    private IEnumerable<Image> GetDirectChildrenImages()
    {
        var parent = _contributionsCalander.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            Image image = parent.GetChild(i).GetComponent<Image>();
            if (image != null)
            {
                yield return image;
            }
        }
    }

    private void ApplyContribution(Image childImage, DayContribution contribution)
    {
        Color color = GetColorByContributionCount(contribution.Count);
        childImage.color = color;
    }

    private Color GetColorByContributionCount(int count)
    {
        if (count == 0) return HexToColor("EEECF1");
        if (count >= 1 && count <= 9) return HexToColor("9AE6A8");
        if (count >= 10 && count <= 19) return HexToColor("40C776");
        if (count >= 20 && count <= 29) return HexToColor("EBFFF8");
        return HexToColor("FFFEFC"); // 30以上
    }

    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }
        return Color.white; // デフォルト色（エラー時）
    }
}
