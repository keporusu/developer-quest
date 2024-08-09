using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Code.Battle
{
    public class CharacterController : MonoBehaviour
    {
        private Animator _animator;
        private static Vector3 _firstPosition = new Vector3(28.3999996f, 22.9545708f, -85f);
        private static Vector3 _lastPosition = new Vector3(28.3999996f, 22.9545708f, -92.0999985f);
        private static readonly int Stop = Animator.StringToHash("Stop");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            this.UpdateAsObservable()
                .Where(_ => transform.localPosition.z < -91.2f)
                .Take(1)
                .Subscribe(_ =>
                {
                    _animator.SetTrigger(Stop);
                }).AddTo(this);
            
            
            transform.localPosition = _firstPosition;
            transform.DOLocalMove(_lastPosition, 1.5f).SetEase(Ease.OutSine);
        }
    }

}
