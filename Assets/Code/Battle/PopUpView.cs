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

        [SerializeField] private int _maxExperience = 80000;
        [SerializeField] private int _maxLevel = 120;

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

        public void SetHp(int hp)
        {
            _updateNum(hp, _hpNum);
        }

        public void SetCp(int cp)
        {
            _updateNum(cp,_contributionNum);
        }

        public void SetDamage(int damage)
        {
            _updateNum(damage, _damageNum);
        }

        public void SetExperience(int ex)
        {
            _updateGage(ex, _maxExperience,_experienceGage);
        }
        public void SetLevel(int level)
        {
            _updateGage(level,_maxLevel,_levelGage);
        }

        private void _updateGage(int res, int max, GameObject gage)
        {
            var scale = gage.transform.localScale;
            
            gage.transform.DOScale(new Vector3((float)res / max, scale.y, scale.z), 1.0f)
                .SetEase(Ease.OutQuad);
            //else gage.transform.localScale = new Vector3((float)res / max,scale.y,scale.z);
        }

        private void _updateNum(int res, TextMeshProUGUI text)
        {
            DOTween.To(() => 0, x => 
                {
                    text.text = x.ToString();
                }, res, 0.5f)
                .SetEase(Ease.OutQuad);
        }
        
    }

}
