using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private GameObject _battleField;
        private Mode _battleMode = Mode.PreBattle;

        private EnemyInfo _enemyInfo;
        [SerializeField] private int _contributionPoint;
        [SerializeField] private int _enemyPoint;
        private int _damage;

        private int _lastContributionPoint;
        private int _lastEnemyPoint;
        private int _lastExperience;

        async void Start()
        {
            //最初のCPをセット
            if (!_isTest)
            {
                _contributionPoint = UserRepository.LoadContributionPoint();
                _enemyPoint = UserRepository.LoadEnemyPoint();
                _enemyInfo = EnemyList.GetEnemy(UserRepository.LoadEnemyName());
            }
            
            _lastContributionPoint = _contributionPoint;
            _lastEnemyPoint = _enemyPoint;
            
            
            _uiController.SetMyGage(_contributionPoint);
            if (_enemyPoint <= 0)
            {
                _monsterManager.CreateMonster(_enemyInfo.GetModel());
                _enemyPoint = _enemyInfo.Hp;
                _uiController.SetEnemyGage(_enemyPoint);
            }
            else
            {
                _uiController.SetEnemyGage(_enemyPoint);
            }

            _lastExperience = UserRepository.LoadExperience();
            
            
            
            await _characterController.OnSetPosition; //ここからゲームスタート
            
            
            _uiController.StartDialogue();
            
            //会話進める
            var dialogue =this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0) && _uiController.IsDialogueActivate)
                .Subscribe(_ =>
                {
                    if(_enemyPoint<=0)_uiController.ChangeLastDialogue();
                    _uiController.AdvanceDialogue();
                    
                    switch (_uiController.DialogueIndex)
                    {
                        case 1: 
                            _uiController.DialogueDeactivate();
                            break;
                        case 2:
                            _uiController.DialogueDeactivate();
                            if (_enemyPoint <= 0)
                            {
                                _monsterManager.Die();
                            }
                            else
                            {
                                _monsterManager.Attack();
                                _characterController.Escape();
                            }
                            break;
                    }
                })
                .AddTo(this);
            
            //攻撃
            
            var decreasingStep = (int)Mathf.Floor(_contributionPoint / 5.0f);

            /*for (int x = _contributionPoint; x >= 0; x -= decreasingStep)
            {
                var damage = decreasingStep * 10;
                _enemyPoint -= damage;
                _uiController.ReadyDamage(damage,_enemyPoint);
                if (x!=0 && x - decreasingStep < 0)
                {
                    var damage2 = x * 10;
                    _enemyPoint -= damage2;
                    _uiController.ReadyDamage(damage2,_enemyPoint);
                }
            }*/
            
            var attack= this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0) && _characterController.Allow)
                .Subscribe(_ =>
                {
                    _characterController.Attack();
                    var newDecreasingStep =
                        _contributionPoint - decreasingStep < 0 ? _contributionPoint : decreasingStep;
                    _contributionPoint -= newDecreasingStep;
                    _uiController.SetMyGage(_contributionPoint);
                    
                    var damage = newDecreasingStep * Random.Range(2f, 2.5f); // 0以上100未満の整数;
                    _enemyPoint -= (int)damage;
                    _damage += (int)damage;
                    _uiController.ReadyDamage((int)damage,_enemyPoint);
                    
                })
                .AddTo(this);
            
            //どちらかのゲージが尽きるまで戦闘が続く
            await UniTask.WaitUntil(() => _contributionPoint <= 0 || _enemyPoint <= 0);
            
            attack.Dispose();
            _uiController.DialogueActivate();

            await UniTask.WaitUntil(() => _uiController.DialogueIndex == 2 && Input.GetMouseButtonDown(0));

            if (_enemyPoint <= 0)
            {
                
            }

            var experience = _lastExperience + 1000;
            
            _uiController.SetPopUp(0, _lastEnemyPoint, _lastContributionPoint, _lastExperience, 15);
            await _uiController.ActivatePopup();
            _uiController.SetPopUpAnimation(_damage, _enemyPoint, _contributionPoint, experience, 15);
            
            
            await UniTask.WaitUntil(() => _uiController.IsBattleEnd);

            UserRepository.SaveExperience(_lastExperience);
            UserRepository.SaveContributionPoint(_contributionPoint);
            UserRepository.SaveEnemyPoint(_enemyPoint);
            
            //TODO: メニューに帰る前に、ユーザーのレベルやら経験値やらを保存する
            SceneTransition.ToMenu();
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


