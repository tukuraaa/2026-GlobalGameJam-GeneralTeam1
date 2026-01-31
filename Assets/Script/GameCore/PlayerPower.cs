using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

[RequireComponent(typeof(Player), typeof(PlayerInput))]
[DisallowMultipleComponent] //プレイヤーは一つパワーだけ
public class PlayerPower : MonoBehaviour
{
    public enum PowerType
    {
        Barrier,    //バリア
        Beam        //衝撃砲
    }


    [Range(0, 5f)]
    public float fullDuration = 5f;

    [SerializeField]
    PowerType powerType = PowerType.Barrier;

    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    InputAction usePowerInputAction;

    private float _duration;
    public float CurrentDuration {
        get => _duration; 
        private set {
            _duration = (value <= fullDuration) ? value : fullDuration;  
        }
    }

    
    void OnValidate()
    {
        if(playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
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

    }

    void Update()
    {
        Debug.Log($"duration: {CurrentDuration:f2} s");
        if (usePowerInputAction.inProgress)
        {
            CurrentDuration -= Time.deltaTime;
            switch (powerType)
            {
                case PowerType.Barrier:
                    //do barrier things
                    
                    break;
                case PowerType.Beam:
                    break;
            }
            
        }
    }
}
