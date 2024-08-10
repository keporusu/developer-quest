using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Code.Battle
{
    public class DialogueManager
    {
        private string[] _dialogues = new string[]
        {
            "ミュータントが現れた！",
            "あなたの攻撃！",
            "ミュータントが攻撃してきた！ あなたは逃げた！"
        };

        private int _currentIndex = -1;

        private readonly ReactiveProperty<string> _currentDialogue  = new ReactiveProperty<string>();
        public IReadOnlyReactiveProperty<string> CurrentDialogue => _currentDialogue;

        public void StartDialogue()
        {
            _currentIndex = -1;
            AdvanceDialogue();
        }

        public int AdvanceDialogue()
        {
            _currentIndex++;
            if (_currentIndex < _dialogues.Length)
            {
                _currentDialogue.Value = _dialogues[_currentIndex];
            }
            else
            {
                Debug.Log("会話が終了しました。");
                // ここで会話終了後の処理を行う
            }

            return _currentIndex;
        }
    }

}
