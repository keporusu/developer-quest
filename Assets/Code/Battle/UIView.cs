using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Battle
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _myGage;
        private float _maxCP = 40f;

        [SerializeField] private RectTransform _enemyGage;

        public void SetMyGage(int point)
        {
            _myGage.localScale = new Vector3(point / _maxCP,_myGage.localScale.y,_myGage.localScale.z);
        }

        public void SetEnemyGage(int point)
        {
            
        }
            
        
    }
}

