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

        [SerializeField] private int _contributionPoint;
        [SerializeField] private int _enemyPoint;

        async void Start()
        {
            //最初のCPをセット
            if (!_isTest)
            {
                _contributionPoint = UserRepository.LoadContributionPoint();
                _enemyPoint = UserRepository.LoadEnemyPoint();
            }
            
            
            _uiController.SetMyGage(_contributionPoint);
            if (_enemyPoint <= 0)
            {
                _monsterManager.CreateMonster();
                _enemyPoint = 100;
                _uiController.SetEnemyGage(_enemyPoint);
            }
            else
            {
                _uiController.SetEnemyGage(_enemyPoint);
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
                    _uiController.SetMyGage(--_contributionPoint);
                    
                    //TODO:もし連続で攻撃を入れたいなら、ここにforを使う
                    _uiController.ReadyDamage(0,--_enemyPoint);
                })
                .AddTo(this);
            
            
            //TODO: ここに戦闘終了後の処理を書く

        }



        [SerializeField] private bool _isTest = true;

    }

    public enum Mode
    {
        PreBattle,
        Battle,
        Finish
    }
}


