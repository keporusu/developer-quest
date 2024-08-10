using System.Collections;
using System.Collections.Generic;
using Code.Menu;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Code.Battle
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private UIView _uiView;
        private DialogueManager _dialogueManager;
        private bool _isDialogueActive = false;
        public bool IsDialogueActive => _isDialogueActive;
        
        private void Start()
        {
            _dialogueManager = new DialogueManager();
            var contributionPoint = UserRepository.LoadContributionPoint();
            SetMyGage(contributionPoint);
            
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
            _dialogueManager.AdvanceDialogue();
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

