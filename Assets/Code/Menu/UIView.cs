using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UniRx;
using Unity.VisualScripting;
using Unit = UniRx.Unit;


namespace Code.Menu
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private GameObject _contributionPointGage;
        private float _maxCP = 40f;

        [SerializeField] private Button _battle;
        [SerializeField] private Button _exit;
        [SerializeField] private Button _levelStatus;
        [SerializeField] private Button _githubStatus;
        [SerializeField] private Button _setting;

        public IObservable<Unit> Battle;
        public IObservable<Unit> Exit;

        private void Awake()
        {
            Battle = _battle.OnClickAsObservable();
            Exit = _exit.OnClickAsObservable();
        }

        public void SetContributionPointGage(int contributionPoint)
        {
            var scale = _contributionPointGage.transform.localScale;
            _contributionPointGage.transform.localScale = new Vector3(contributionPoint / _maxCP,scale.y,scale.z);
        }
    
    
    }

}
