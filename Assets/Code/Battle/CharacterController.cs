using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UIElements;
using ObservableExtensions = UniRx.ObservableExtensions;

namespace Code.Battle
{
    public class CharacterController : MonoBehaviour
    {
        private Animator _animator;
        private static Vector3 _firstPosition = new Vector3(28.3999996f, 22.9545708f, -85f);
        private static Vector3 _lastPosition = new Vector3(28.3999996f, 22.9545708f, -92.0999985f);
        private static readonly int Stop = Animator.StringToHash("Stop");

        public readonly Subject<Unit> OnSetPosition = new Subject<Unit>();
        private static readonly int SimpleAttack = Animator.StringToHash("SimpleAttack");
        
        //アニメーション開始直後はfalseにする
        private bool _allow = true;
        private static readonly int Escape1 = Animator.StringToHash("Escape");
        private static readonly int JumpAttack = Animator.StringToHash("JumpAttack");
        private static readonly int Slash = Animator.StringToHash("Slash");
        public bool Allow => _allow;
        

        private async void Start()
        {
            
            _animator = GetComponent<Animator>();
            
            ObservableExtensions.Subscribe(this.UpdateAsObservable()
                    .Where(_ => transform.localPosition.z < -91.2f)
                    .Take(1), _ =>
                {
                    _animator.SetTrigger(Stop);
                }).AddTo(this);
            
            
            
            transform.localPosition = _firstPosition;
            
            await transform.DOLocalMove(_lastPosition, 1.5f)
                .SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    Debug.Log("Animation completed");
                    OnSetPosition.OnNext(Unit.Default);
                    OnSetPosition.OnCompleted();
                }
                    
                    )
                .AsyncWaitForCompletion();

            
            ObservableExtensions.Subscribe(this.UpdateAsObservable()
                    .Where(_ => Input.GetMouseButtonDown(0)), _ =>
                {
                    
                }).AddTo(this);

        }


        public void Attack(int? param=null)
        {
            //bool isSpecificAnimPlaying = _animator.GetCurrentAnimatorStateInfo(0).IsName("SimpleAttack");
            //if (isSpecificAnimPlaying) return false; //失敗
            var random = UnityEngine.Random.Range(0,3);
            var res = _makeDelay();

            switch (random)
            {
                case 0:
                    _animator.SetTrigger(JumpAttack);
                    break;
                case 1: 
                    _animator.SetTrigger(Slash);
                    break;
                default:
                    _animator.SetTrigger(SimpleAttack);
                    break;
            }
            
            //return true; //成功
        }

        public void Escape()
        {
            _animator.SetTrigger(Escape1);
        }

        private async UniTask _makeDelay()
        {
            _allow = false;
           await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
           _allow = true;
        }
        
    }

}
