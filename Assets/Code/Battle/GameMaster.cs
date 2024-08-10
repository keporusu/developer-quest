using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Code.Battle
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private UIController _uiController;
        private Mode _battleMode = Mode.PreBattle;

        async void Start()
        {
            await _characterController.OnSetPosition; //ここからゲームスタート
            
            
            _uiController.StartDialogue();
            
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => _uiController.AdvanceDialogue())
                .AddTo(this);
            
            //CharacterControllerのUpdate

            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => _characterController.Attack())
                .AddTo(this);

        }
        
    }

    public enum Mode
    {
        PreBattle,
        Battle,
        Finish
    }
}


