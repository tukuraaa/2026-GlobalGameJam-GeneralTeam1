using UnityEngine;

public class BootRoot : MonoBehaviour
{
    [SerializeField]
    string _BootScene = "Title";

    async void Start()
    {
        await MySceneManager.Instance.LoadSceneAsync(_BootScene);
    }
}