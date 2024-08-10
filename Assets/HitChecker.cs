using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private Animator _mutantAnimator;
    private static readonly int Damaged = Animator.StringToHash("Damaged");

    private void Start()
    {
        _mutantAnimator = GameObject.Find("Mutant").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //どうせ剣しか通らないので手抜きで
        Debug.Log("Yeah");
        _mutantAnimator.SetTrigger(Damaged);
        
    }
}
