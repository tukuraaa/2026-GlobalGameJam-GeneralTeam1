using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonClass<MySceneManager>
{
    public string NowSceneName { get; private set; }
    public async UniTask LoadSceneAsync(string sceneName)
    {
        //LoadingView.Instance.ShowLoading();
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        Debug.Log($"Scene Loaded: {sceneName}");
        NowSceneName = sceneName;
        //LoadingView.Instance.CloseLoading();
    }
}