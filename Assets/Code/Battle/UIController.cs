using System.Collections;
using System.Collections.Generic;
using Code.Menu;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Battle
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIView _uiView;

        private DamagePointCreator _damagePointCreator;
        
        private DialogueManager _dialogueManager;
        private bool _isDialogueActive = false;
        public bool IsDialogueActivate => _isDialogueActive;

        private ActionManager _damageActions;

        private int _dialogueIndex = 0;
        public int DialogueIndex => _dialogueIndex;
        
        
        private void Start()
        {
            _dialogueManager = new DialogueManager();

            _damageActions = new ActionManager();
            
            var hitBox = GameObject.Find("HitBox");
            _damagePointCreator = GameObject.Find("DamagePointCreator").GetComponent<DamagePointCreator>();
            
            //どうせここは剣しか来ない
            hitBox.OnTriggerEnterAsObservable().Subscribe(_ =>
            {
                _damageActions?.InvokeAction();
                //_damageActions?.RemoveOldestAction();
            });
            
            //タップで次のダイアログ
            //this.UpdateAsObservable()
                //.Where(_ => Input.GetMouseButtonDown(0) && _isDialogueActive)
                //.Subscribe(_ => _dialogueManager.AdvanceDialogue())
                //.AddTo(this);
            
            //UIに反映
            _dialogueManager.CurrentDialogue
                .Subscribe(dialogue => _uiView.SetBubble(dialogue))
                .AddTo(this);

        }


        public void StartDialogue()
        {
            _isDialogueActive = true;
            _dialogueManager.StartDialogue();
        }

        public void AdvanceDialogue()
        {
            //ダイアログが無効になっているなら会話を進められない
            if(!_isDialogueActive)return;
            _dialogueIndex = _dialogueManager.AdvanceDialogue();
            if (_dialogueIndex == 1) _isDialogueActive = false;
        }

        public void DialogueActivate()
        {
            _isDialogueActive = true;
        }

        public void DialogueDeactivate()
        {
            _isDialogueActive = false;
        }

        public void ChangeLastDialogue()
        {
            _dialogueManager.ChangeLast();
        }
        
        
        //ダメージのアクションはためておく
        public void ReadyDamage(int damage, int point)
        {
            _damageActions.AddAction(()=>
            {
                _damagePointCreator.Create(damage);
                SetEnemyGage(point);
            });
        }
        
        

        public void SetMyGage(int point)
        {
            _uiView.SetMyGage(point);
        }

        public void SetEnemyGage(int point)
        {
            _uiView.SetEnemyGage(point);
        }

        public void SetBubble(string text)
        {
            _uiView.SetBubble(text);
        }
    }
}

