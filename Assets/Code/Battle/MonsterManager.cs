using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Code.Battle
{
    public class MonsterManager : MonoBehaviour
    {
        private MonsterController _monsterController;

        private void Start()
        {
            _monsterController = GameObject.Find("Monster").GetComponent<MonsterController>();
        }

        public void CreateMonster()
        {
            
        }

        public void Attack()
        {
            _monsterController.Attack();
        }

        public void Damaged()
        {
            _monsterController.Damaged();
        }

        public void Die()
        {
            _monsterController.Die();
        }
    }

}
