using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

namespace Code.Menu
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Run = Animator.StringToHash("Run");
    
        private static Vector3 _battlePos = new Vector3(-7.0f, 0f, 0f);
        private static Vector3 _exitPos = new Vector3(7.0f, 0f, 0f);

        void Start()
        {
        
        }

        public async UniTask GoBattle()
        {
            _animator.SetTrigger(Run);

            transform.DORotate(Vector3.up * 270f, 0.6f);
            await transform.DOMove(_battlePos, 1.7f).AsyncWaitForCompletion();
        }

        public async UniTask Exit()
        {
            _animator.SetTrigger(Run);
        
            // 回転と移動を同時に開始
            transform.DORotate(Vector3.up * 90f, 0.6f);
            await transform.DOMove(_exitPos, 1.7f).AsyncWaitForCompletion();
        }

    
    }

}
