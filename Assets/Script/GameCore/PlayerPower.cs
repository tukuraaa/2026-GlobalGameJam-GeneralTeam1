using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent] //プレイヤーは一つパワーだけ
public class PlayerPower : MonoBehaviour
{
    public enum PowerType
    {
        Barrier,    //バリア
        Beam        //衝撃砲
    }


    [Range(0, 30f)]
    public float fullDuration = 30f;

    [SerializeField]
    PowerType powerType = PowerType.Barrier;

    [SerializeField]
    BoxCollider barrier;

    [SerializeField]
    InputAction usePowerInputAction;

    private float _duration;
    public float CurrentDuration {
        get => _duration; 
        private set {
            _duration = (value <= fullDuration) ? value : fullDuration;
            _duration = (_duration >= 0) ? _duration : 0;
        }
    }


    void Start()
    {
        _duration = fullDuration;
        usePowerInputAction.Enable();
    }

    void OnDestroy()
    {
        usePowerInputAction.Disable();
        // barrier.
    }

    void Update()
    {
        // Debug.Log($"duration: {CurrentDuration:f2} s");
        if (usePowerInputAction.inProgress)
        {
            CurrentDuration -= Time.deltaTime;
        }
        switch (powerType)
        {
            case PowerType.Barrier:
                //do barrier things

                barrier.gameObject.SetActive(usePowerInputAction.inProgress && CurrentDuration > 0);
                break;
            case PowerType.Beam:
                break;
        }
    }
}
