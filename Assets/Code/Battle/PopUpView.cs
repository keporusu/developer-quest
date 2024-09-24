using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.PlayerLoop;
using Unit = UniRx.Unit;

namespace Code.Battle
{
    public class PopUpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageNum;
        [SerializeField] private TextMeshProUGUI _hpNum;
        [SerializeField] private TextMeshProUGUI _contributionNum;
        [SerializeField] private GameObject _experienceGage;
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private GameObject _levelGage;
        [SerializeField] private Button _okButton;

        [SerializeField] private int _maxExperience = 80000;
        [SerializeField] private int _maxLevel = 120;

        public Subject<Unit> OnPopUpEnd { get; private set; }

        private void Awake()
        {
            OnPopUpEnd = new Subject<Unit>();
        }

        private void _initialize()
        {
            _okButton.onClick.AddListener(async ()=>
            {
                await _inActivate();
                OnPopUpEnd.OnNext(Unit.Default);
            });
        }
        
        public async UniTask Activate()
        {
            _initialize();
            gameObject.SetActive(true);
            gameObject.transform.localScale = Vector3.zero;
            await gameObject.transform
                .DOScale(new Vector3(0.0028f, 0.0028f, 0.0028f), 0.5f)
                .AsyncWaitForCompletion();
        }
        
        private async UniTask _inActivate()
        {
            gameObject.transform.DOScale(Vector3.zero, 0.2f)
                .OnComplete(()=>gameObject.SetActive(false));
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        }

        public void SetHp(int hp, bool anim = false)
        {
            _updateNum(hp, _hpNum, anim);
        }

        public void SetCp(int cp, bool anim = false)
        {
            _updateNum(cp,_contributionNum, anim);
        }

        public void SetDamage(int damage, bool anim = false)
        {
            _updateNum(damage, _damageNum, anim);
        }

        public void SetExperience(int ex, bool anim = false)
        {
            _updateGage(ex, _maxExperience,_experienceGage, anim);
            _updateNum2(ex,_maxExperience,_experience, anim);
        }
        public void SetLevel(int level, bool anim = false)
        {
            _updateGage(level,_maxLevel,_levelGage, anim);
            _updateNum2(level, _maxLevel, _level, anim);
        }

        private void _updateGage(int res, int max, GameObject gage, bool anim = false)
        {
            var scale = gage.transform.localScale;
            if (anim)
            {
                gage.transform.DOScale(new Vector3((float)res / max, scale.y, scale.z), 1.0f)
                    .SetEase(Ease.OutQuad);
                //else gage.transform.localScale = new Vector3((float)res / max,scale.y,scale.z);
            }
            else
            {
                gage.transform.localScale = new Vector3((float)res / max, scale.y, scale.z);
            }
            
        }

        private void _updateNum(int res, TextMeshProUGUI text, bool anim = false)
        {
            if (anim)
            {
                DOTween.To(() => 0, x => 
                    {
                        text.text = x.ToString();
                    }, res, 0.5f)
                    .SetEase(Ease.OutQuad);
            }
            else
            {
                text.text = res.ToString();
            }
        }

        private void _updateNum2(int res, int max, TextMeshProUGUI text, bool anim = false)
        {
            if (anim)
            {
                DOTween.To(() => 0, x =>
                    {
                        text.text = $"{x.ToString()} / {res.ToString()}";
                    }, res, 0.5f)
                    .SetEase(Ease.OutQuad);
            }
            else
            {
                text.text = res.ToString();
            }
            
        }

    }

}
