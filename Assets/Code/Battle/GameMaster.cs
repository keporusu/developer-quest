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
            //最初のCPをセット
            var contributionPoint = UserRepository.LoadContributionPoint();
            _uiController.SetMyGage(contributionPoint);
            
            await _characterController.OnSetPosition; //ここからゲームスタート
            
            
            _uiController.StartDialogue();
            
            //会話進める
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => _uiController.AdvanceDialogue())
                .AddTo(this);
            
            //攻撃
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    var success = _characterController.Attack();
                    if (success) _uiController.ReadyDamage(0);
                })
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


