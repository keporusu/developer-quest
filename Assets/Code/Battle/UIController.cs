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

        void SetMyGage(int point)
        {
            _uiView.SetMyGage(point);
        }
    }
}

