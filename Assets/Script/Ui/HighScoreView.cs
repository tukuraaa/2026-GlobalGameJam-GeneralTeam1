using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    [SerializeField]
    ScrollRect highScoreView;

    void OnValidate()
    {
        if(highScoreView == null)
        {
            highScoreView = transform.Find("HighScoreView").GetComponent<ScrollRect>();
        }

    }

}
