using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
    /// <summary>
    /// シーン切り替え時に破棄するか
    /// </summary>
    [SerializeField]
    bool dontDestoryOnLoad = false;

    void Awake()
    {
        if (dontDestoryOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
