using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの操作を管理するクラス
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの中心位置の変数
    /// </summary>
    [SerializeField]
    private Vector3 _centerPos = new Vector3(0, 1, 0);
    /// <summary>
    /// プレイヤーの移動速度の変数
    /// </summary>
    [SerializeField]
    private float _speed = 5f;
    /// <summary>
    /// プレイヤーの角度軸A（楕円の長軸）の変数
    /// </summary>
    [SerializeField]
    private float _radiusA = 3f;
    /// <summary>
    /// プレイヤーの角度軸B（楕円の短軸）の変数
    /// </summary>
    [SerializeField]
    private float _radiusB = 1.2f;
    /// <summary>
    /// プレイヤーの現在の角度（ラジアン単位）の変数
    /// </summary>
    [SerializeField]
    private float _currentAngle = 0f;
    /// <summary>
    /// プレイヤーIDの変数
    /// </summary>
    [SerializeField]
    private int playerId = 1;

    /// <summary>
    /// プレイヤーの水平方向入力値の変数
    /// </summary>
    public float HorizontalX = 0;
    /// <summary>
    /// プレイヤーの垂直方向入力値の変数
    /// </summary>
    public float VerticalY = 0;
    /// <summary>
    /// プレイヤーの移動状態フラグの変数
    /// </summary>
    public bool IsMoving = false;
    /// <summary>
    /// プレイヤーの移動入力値の変数
    /// </summary>
    private Vector2 moveInputValue;

    /// <summary>
    /// プレイヤーの位置を楕円軌道上に更新する関数
    /// </summary>
    void Update()
    {
        // 水平方向の入力がある場合にのみ処理を実行
        if (Mathf.Abs(HorizontalX) > 0.01f)
        {
            // 入力方向に応じて角度を更新、HorizontalX < 0 で左移動、> 0 で右移動
            // _speedで角速度を制御、時間ステップでスムーズな移動を保証
            float angleSpeed = _speed / Mathf.Max(_radiusA, _radiusB);// 正規化された角速度
            _currentAngle += HorizontalX * angleSpeed * Time.deltaTime;// 角度を更新

            // 角度を[0, 2π]の範囲内に保持
            if (_currentAngle < 0) _currentAngle += 2 * Mathf.PI;// 2πを超えた場合に調整
            if (_currentAngle > 2 * Mathf.PI) _currentAngle -= 2 * Mathf.PI;// 0未満になった場合に調整

            // 楕円のパラメータ方程式に基づいて新しい位置を計算
            Vector3 newPosition = new Vector3(
                _centerPos.x + _radiusA * Mathf.Cos(_currentAngle),// X座標
                _centerPos.y + _radiusB * Mathf.Sin(_currentAngle),// Y座標
                _centerPos.z);// Z座標（固定）

            transform.position = newPosition;
        }
    }

    /// <summary>
    /// プレイヤーが移動するための入力処理関数
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInputValue = context.ReadValue<Vector2>();// 入力値を取得
        HorizontalX = moveInputValue.x;// 水平方向の入力値を設定
        VerticalY = moveInputValue.y;// 垂直方向の入力値を設定
        IsMoving = (Mathf.Abs(VerticalY) != 0 || Mathf.Abs(HorizontalX) != 0);// 移動状態を更新
    }

    /// <summary>
    /// バリア発動の入力処理関数
    /// </summary>
    /// <param name="context"></param>
    public void OnBarrier(InputAction.CallbackContext context)
    {
        Debug.Log("Barrier!");
        Stage.Instance.EarthUnit.ActiveBarrier(playerId, this);// バリアスキルIDを発動
    }
}
