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

        public void CreateMonster(GameObject model)
        {
            Destroy(_monsterController.gameObject);
            var character= Instantiate(model, transform);
            character.transform.localPosition = new Vector3(29.2000008f, 22.9545708f, -96.1999969f);
            character.transform.localScale = new Vector3(1.60000002f, 1.60000002f, 1.60000002f);
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
