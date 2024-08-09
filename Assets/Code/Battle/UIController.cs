using System;
using System.Collections;
using System.Collections.Generic;
using Code.Menu;
using UnityEngine;

namespace Code.Battle
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIView _uiView;
        
        private void Start()
        {
            var contributionPoint = UserRepository.LoadContributionPoint();
            SetMyGage(contributionPoint);
        }

        public void SetMyGage(int point)
        {
            _uiView.SetMyGage(point);
        }

        public void SetEnemyGage(int point)
        {
            _uiView.SetEnemyGage(point);
        }

        public void SetBubble(string text)
        {
            _uiView.SetBubble(text);
        }
    }
}

