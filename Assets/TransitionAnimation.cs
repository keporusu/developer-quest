using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _circle;


    public async UniTask FadeInOut(string sceneName)
    {
        await _circle.transform.DOScale(0f, 0.5f).OnComplete( async () =>
        {
            await SceneManager.LoadSceneAsync(sceneName).AsAsyncOperationObservable();
            _circle.transform.DOScale(new Vector3(23f, 23f, 23f), 0.5f);
        }).AsyncWaitForCompletion();
    }
    
    public async void FadeOut(Vector3 scale, UnityAction onAnimationEndCallback = null)
    {
        _circle.transform.localScale = new Vector3(23f,23f,23f);
        await _circle.transform.DOScale(scale, 0.5f).AsyncWaitForCompletion();
        onAnimationEndCallback?.Invoke();
    }
    public async void FadeIn(Vector3 scale, UnityAction onAnimationEndCallback = null)
    {
        _circle.transform.localScale = new Vector3(0f,0f,0f);
        await _circle.transform.DOScale(scale, 0.5f).AsyncWaitForCompletion();
        onAnimationEndCallback?.Invoke();
    }
    
}
