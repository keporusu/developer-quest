using System;
using System.Collections;
using System.Collections.Generic;
using Code.Battle;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private MonsterController _monsterController;
    
    private static readonly int Damaged = Animator.StringToHash("Damaged");

    private void Start()
    {
        _monsterController = GameObject.Find("Monster").GetComponent<MonsterController>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //どうせ剣しか通らないので手抜きで
        Debug.Log("Yeah");
        _monsterController.Damaged();
    }
}
