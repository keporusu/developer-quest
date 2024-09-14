using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.PlayerLoop;

namespace Code.Battle
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageNum;
        [SerializeField] private TextMeshProUGUI _hpNum;
        [SerializeField] private TextMeshProUGUI _contributionNum;
        [SerializeField] private GameObject _experienceGage;
        [SerializeField] private GameObject _levelGage;
        [SerializeField] private Button _okButton;

        private void _initialize()
        {
            _okButton.onClick.AddListener(_inActivate);
        }
        
        public void Activate()
        {
            _initialize();
            gameObject.SetActive(true);
            gameObject.transform.localScale = Vector3.zero;
            gameObject.transform.DOScale(new Vector3(0.0028f, 0.0028f, 0.0028f), 0.5f);
        }
        
        private void _inActivate()
        {
            gameObject.transform.DOScale(Vector3.zero, 0.2f)
                .OnComplete(()=>gameObject.SetActive(false));
            
        }
        
    }

}
