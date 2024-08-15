using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;

namespace Code.Battle
{
    
    public class DamagePointAnimation : MonoBehaviour
    {
        
        private void Awake()
        {
            transform.localPosition =  new Vector3(-32f, 144f, 0f);
        }

        public void Begin(int damage)
        {
            var text = GetComponent<TextMeshProUGUI>();
            text.text = damage.ToString();
            transform.DOLocalMove(new Vector3(-31.98f,220f,0f), 1f);
            text.DOFade(0f, 1f).OnComplete(()=>Destroy(this.gameObject));
        }
    }

}
