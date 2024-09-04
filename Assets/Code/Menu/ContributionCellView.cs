using Code.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        // ここにパネルに入ったときの処理を書きます
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("マウスカーソルがパネルから出ました");
        _bubble.SetActive(false);
        // ここにパネルから出たときの処理を書きます
    }
}