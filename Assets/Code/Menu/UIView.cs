using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UniRx;
using Unity.VisualScripting;
using Unit = UniRx.Unit;
using DG.Tweening;


namespace Code.Menu
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private GameObject _contributionPointGage;
        private float _maxCP = 100f;

        [SerializeField] private Button _battle;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _levelStatus;
        [SerializeField] private Button _githubStatus;
        [SerializeField] private Button _setting;
        

        public IObservable<Unit> Battle { get; private set; }
        public IObservable<Unit> Exit { get; private set; }
        public IObservable<Unit> GitHub { get; private set; }
        public IObservable<Unit> Level  { get; private set; }
        public IObservable<Unit> Setting  { get; private set; }

        private void Awake()
        {
            Battle = _battle.OnClickAsObservable();
            Exit = _exit.OnClickAsObservable();
            GitHub = _githubStatus.OnClickAsObservable();
            Level = _levelStatus.OnClickAsObservable();
            Setting = _setting.OnClickAsObservable();
        }

        public void SetContributionPointGage(int contributionPoint, bool useAnimation = true)
        {
            var scale = _contributionPointGage.transform.localScale;
            
            if(useAnimation)_contributionPointGage.transform.DOScale(new Vector3(contributionPoint / _maxCP, scale.y, scale.z), 1.0f)
                .SetEase(Ease.OutQuad);
            else _contributionPointGage.transform.localScale = new Vector3(contributionPoint / _maxCP,scale.y,scale.z);
        }


    }

}
