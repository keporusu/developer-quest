using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Code.Battle
{
    public class DamagePointCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPref;

        public void Create(int damage)
        {
            var _ui = Instantiate(_uiPref, transform);
            _ui.GetComponent<DamagePointAnimation>().Begin(damage);
        }
    }
}

