using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Code.Battle
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private UIController _uiController;

        async void Start()
        {
            await _characterController.OnSetPosition; //ここからゲームスタート
            
            
            
        }
        
    }

}
