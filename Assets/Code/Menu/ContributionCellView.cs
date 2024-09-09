using Code.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ContributionCellView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _bubble;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetContribution(DayContribution dayContribution)
    {
        // 初期テキストを設定
        _text.text = $"{dayContribution.Day}  0 Contributions";

        // カウント部分のアニメーション
        DOTween.To(() => 0, x => 
            {
                _text.text = $"{dayContribution.Day}  {Mathf.RoundToInt(x)} Contributions";
            }, dayContribution.Count, 0.5f)
            .SetEase(Ease.OutQuad);
    }

    public void ActivateBubble()
    {
        _bubble.SetActive(true);
        var canvasGroup = _bubble.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.3f);
    }

    public void InActivateBubble()
    {
        var canvasGroup = _bubble.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, 0.3f);
        _bubble.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("マウスカーソルがパネルに入りました");
        ActivateBubble();
        // ここにパネルに入ったときの処理を書きます
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("マウスカーソルがパネルから出ました");
        InActivateBubble();
        // ここにパネルから出たときの処理を書きます
    }
}