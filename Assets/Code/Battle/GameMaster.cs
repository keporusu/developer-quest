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
        [SerializeField] private MonsterManager _monsterManager;
        private Mode _battleMode = Mode.PreBattle;

        async void Start()
        {
            //最初のCPをセット
            // TODO: ここにUserRepositoryに登録されているやつ全部引き出す
            var contributionPoint = UserRepository.LoadContributionPoint();
            var enemyPoint = UserRepository.LoadEnemyPoint();
            
            _uiController.SetMyGage(contributionPoint);
            if (enemyPoint <= 0)
            {
                _monsterManager.CreateMonster();
                enemyPoint = 100;
                _uiController.SetEnemyGage(enemyPoint);
            }
            else
            {
                _uiController.SetEnemyGage(enemyPoint);
            }
            
            
            
            await _characterController.OnSetPosition; //ここからゲームスタート
            
            
            _uiController.StartDialogue();
            
            //会話進める
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => _uiController.AdvanceDialogue())
                .AddTo(this);
            
            //攻撃
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0) && _characterController.Allow)
                .Subscribe(_ =>
                {
                    _characterController.Attack();
                    _uiController.SetMyGage(--contributionPoint);
                    _uiController.ReadyDamage(0,--enemyPoint);
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


