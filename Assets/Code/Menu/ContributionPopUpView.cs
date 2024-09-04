using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;


namespace Code.Menu
{
    public class ContributionPopUpView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _totaltxt;
    [SerializeField] private TextMeshProUGUI _allcount;
    [SerializeField] private Button _okButton;
    [SerializeField] private GameObject _contributionsCalander;
    
    //移動に使う
    [SerializeField] private float _cellSize = 135.92f;
    [SerializeField] private Vector3 _initialContentPosition = new Vector3(-592.0758f,401.05f,0f);

    private bool _isDisabled = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    /// <param name="allCount"></param>
    /// <param name="blankDays">30日を超えてどれだけログインしていなかったか</param>
    /// <param name="previous"></param>
    /// <param name="required"></param>
    public async UniTask<bool> Init(int count, int allCount, int blankDays, IEnumerable<DayContribution> previous, IEnumerable<DayContribution> required, bool isLastChanged)
    {
        _count.text = count.ToString();
        _allcount.text = allCount.ToString();
        
        var childObjects = GetDirectChildrenImages().ToList();
        int index = 0;

        // previousの内容を反映
        foreach (var contribution in previous)
        {
            if (index >= childObjects.Count) break;
            childObjects[index].GetComponent<ContributionCellView>().SetContribution(contribution);
            ApplyContribution(childObjects[index], contribution);
            index++;
        }

        if (isLastChanged && index > 0)
        {
            // 最後のpreviousを上書き
            index--;
        }

        int previousIndex = index;
        
        
        //アニメーション
        transform.localScale = new Vector3(0f, 0f, 0f);
        await transform.DOScale(new Vector3(0.45f, 0.45f, 0.45f), 0.5f).AsyncWaitForCompletion();
        var calenderRect = _contributionsCalander.GetComponent<RectTransform>();//.anchoredPosition;
        await calenderRect.DOAnchorPos(_initialContentPosition, 0.5f).AsyncWaitForCompletion();
            //_initialContentPosition;
        
        //_contributionsCalander.transform.DOLocalMove(_initialContentPosition, 0.5f);
        
        // requiredの内容を反映
        foreach (var contribution in required)
        {
            if (index >= childObjects.Count) break;
            childObjects[index].GetComponent<ContributionCellView>().SetContribution(contribution);
            await ApplyContributionByAninmation(childObjects[index], contribution);
            index++;
            await calenderRect
                .DOAnchorPos(
                    new Vector2(_initialContentPosition.x, _initialContentPosition.y) -
                    new Vector2(_cellSize, 0f) * (index-previousIndex), 0.3f).AsyncWaitForCompletion();
        }
        
        
        _okButton.onClick.AddListener(() =>
        {
            transform.DOScale(new Vector3(0f, 0f, 0f), 0.2f)
                .OnComplete(()=>gameObject.SetActive(false));
            _isDisabled = true;
        });
        

        return true;
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
    
    private async UniTask ApplyContributionByAninmation(Image childImage, DayContribution contribution)
    {
        Color targetColor = GetColorByContributionCount(contribution.Count);
        Color startColor = childImage.color;
    
        // 色変更のアニメーション時間（秒）
        float colorChangeDuration = 0.5f;

        await childImage.DOColor(targetColor, colorChangeDuration).AsyncWaitForCompletion();
    }

    private Color GetColorByContributionCount(int count)
    {
        if (count == 0) return HexToColor("FFFFFF");//HexToColor("EEECF1");
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
}

