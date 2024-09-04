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
        _text.text = dayContribution.Day + "  " + dayContribution.Count + " Contributions";
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("マウスカーソルがパネルに入りました");
        _bubble.SetActive(true);
        var canvasGroup = _bubble.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 0.3f);
        // ここにパネルに入ったときの処理を書きます
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("マウスカーソルがパネルから出ました");
        var canvasGroup = _bubble.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, 0.3f);
        _bubble.SetActive(false);
        // ここにパネルから出たときの処理を書きます
    }
}