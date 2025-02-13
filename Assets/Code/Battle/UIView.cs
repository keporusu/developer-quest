using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Code.Battle
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _myGage;
        private float _maxCP = 100f;

        [SerializeField] private RectTransform _enemyGage;
        private float _maxEP = 5000f;

        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            SceneTransition.Initialize();
            SceneTransition.Come();
        }


        public void SetMyGage(int point)
        {
            //_myGage.localScale = new Vector3(point / _maxCP,_myGage.localScale.y,_myGage.localScale.z);
            _myGage.DOScale(new Vector3(point / _maxCP, _myGage.localScale.y, _myGage.localScale.z), 0.8f).SetEase(Ease.OutExpo);
        }

        public void SetEnemyGage(int point)
        {
            if (point < 0) point = 0;
            _enemyGage.localScale = new Vector3(point / _maxEP, _enemyGage.localScale.y, _enemyGage.localScale.z);
        }

        public void SetBubble(string text)
        {
            _text.text = text;
        }
        
        
    }
}

