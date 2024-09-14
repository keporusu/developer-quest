using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class SceneTransition
    {
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
            SceneManager.LoadScene("Battle");
        }
        public static void ToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    
    
    }

}
