using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Vector3 _centerPos = new Vector3(0, 1, 0);
    [SerializeField] float _speed = 5f;
    [SerializeField] float _radiusA = 3f;
    [SerializeField] float _radiusB = 1.2f;
    public float HorizontalX = 0;
    public float VerticalY = 0;
    public bool IsMoving = false;
    [SerializeField]
    private float _currentAngle = 0f; // 現在の楕円上の角度パラメータ

    private Vector2 moveInputValue;// 入力ベクトルを格納する変数

    void Update()
    {
        if (Mathf.Abs(HorizontalX) > 0.01f)
        {
            // 入力方向に応じて角度を更新、HorizontalX < 0 で左移動、> 0 で右移動
            // _speedで角速度を制御、時間ステップでスムーズな移動を保証
            float angleSpeed = _speed / Mathf.Max(_radiusA, _radiusB); // 正規化された角速度
            _currentAngle += HorizontalX * angleSpeed * Time.deltaTime;
            
            // 角度を[0, 2π]の範囲内に保持
            if (_currentAngle < 0) _currentAngle += 2 * Mathf.PI;
            if (_currentAngle > 2 * Mathf.PI) _currentAngle -= 2 * Mathf.PI;
            
            // 楕円のパラメータ方程式に基づいて新しい位置を計算
            Vector3 newPosition = new Vector3(
                _centerPos.x + _radiusA * Mathf.Cos(_currentAngle),
                _centerPos.y + _radiusB * Mathf.Sin(_currentAngle),
                _centerPos.z
            );
            
            // 位置を更新
            transform.position = newPosition;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInputValue = context.ReadValue<Vector2>();
        HorizontalX = moveInputValue.x;
        VerticalY = moveInputValue.y;
        IsMoving = (Mathf.Abs(VerticalY) != 0 || Mathf.Abs(HorizontalX) != 0);
    }
}
