using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Code.Battle
{
    public class MonsterController : MonoBehaviour
    {
        private Animator _anim;
        private static readonly int Damaged1 = Animator.StringToHash("Damaged");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int FallBack = Animator.StringToHash("FallBack");

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void Damaged()
        {
            _anim.SetTrigger(Damaged1);
        }

        public void Attack()
        {
            _anim.SetTrigger(Attack1);
        }

        public void Die()
        {
            _anim.SetTrigger(FallBack);
        }
    }

}
