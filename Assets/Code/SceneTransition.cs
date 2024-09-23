using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace Code
{
    public class SceneTransition
    {

        private static TransitionAnimation _transitionAnimation;
        public static void Initialize()
        {
            _transitionAnimation = GameObject.Find("Transition").GetComponent<TransitionAnimation>();
            _activeTransition(false);
        }
        
        
        public static void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }

        public static void ToBattle()
        {
            _fadeTransition("Battle");
        }
        public static void ToMenu()
        {
            _fadeTransition("Menu");
        }

        public static void Come()
        {
            _activeTransition(true);
            _transitionAnimation.FadeIn(new Vector3(23f,23f,23f), () =>
            {
                _activeTransition(false);
            });
        }

        private static void _activeTransition(bool active)
        {
            _transitionAnimation.gameObject.SetActive(active);
        }

        private static void _fadeTransition(string sceneName)
        {
            _activeTransition(true);
            _transitionAnimation.FadeOut(new Vector3(0f,0f,0f), async () =>
            {
                await SceneManager.LoadSceneAsync(sceneName).AsAsyncOperationObservable();
                //_transitionAnimation.FadeIn(new Vector3(23f,23f,23f));
            });
        }
    
    
    }

}
