using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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
    /// 操作の入力判定時間の変数
    /// </summary>
    [SerializeField]
    private float _inputTime = 0.01f;
    /// <summary>
    /// プレイヤーの停止時間の変数
    /// </summary>
    [SerializeField]
    private float _stopTime = 1f;
    /// <summary>
    /// 待機時間の変数
    /// </summary>
    [SerializeField]
    private float _waitTime = 2f;
    /// <summary>
    /// プレイヤーIDの変数
    /// </summary>
    [SerializeField]
    private int _playerId = 1;
    /// <summary>
    /// プレイヤー接触時のイベント変数
    /// </summary>
    [SerializeField]
    private UnityEvent onEnter = null;

    /// <summary>
    /// プレイヤーの水平方向入力値の変数
    /// </summary>
    public float _horizontalX = 0;
    /// <summary>
    /// プレイヤーの垂直方向入力値の変数
    /// </summary>
    public float _verticalY = 0;
    /// <summary>
    /// 円周率の乗数の変数
    /// </summary>
    public float _piMultiplier = 2f;
    /// <summary>
    /// プレイヤーの移動状態フラグの変数
    /// </summary>
    public bool _isMoving = true;
    /// <summary>
    /// プレイヤーの操作有効フラグの変数
    /// </summary>
    public bool _isActiving = true;
    /// <summary>
    /// コルーチン開始フラグの変数
    /// </summary>
    public bool _isStopON = true;
    /// <summary>
    /// プレイヤーの移動入力値の変数
    /// </summary>
    private Vector2 _moveInputValue = Vector2.zero;

    /// <summary>
    /// プレイヤー接触時のイベントのプロパティ
    /// </summary>
    public UnityEvent OnEnter { get => onEnter; set => onEnter = value; }

    /// <summary>
    /// プレイヤーの位置を楕円軌道上に更新する関数
    /// </summary>
    void Update()
    {
        // 入力が一定時間以上で、操作が有効な場合にのみ移動処理を実行
        if (Mathf.Abs(_horizontalX) > _inputTime && _isActiving)
        {
            // 入力方向に応じて角度を更新、HorizontalX < 0 で左移動、> 0 で右移動
            // _speedで角速度を制御、時間ステップでスムーズな移動を保証
            float angleSpeed = _speed / Mathf.Max(_radiusA, _radiusB);// 正規化された角速度
            _currentAngle += _horizontalX * angleSpeed * Time.deltaTime;// 角度を更新

            // 角度を[0, 2π]の範囲内に保持
            if (_currentAngle < 0) _currentAngle += _piMultiplier * Mathf.PI;// 2πを超えた場合に調整
            if (_currentAngle > _piMultiplier * Mathf.PI) _currentAngle -= 2 * Mathf.PI;// 0未満になった場合に調整

            // 楕円のパラメータ方程式に基づいて新しい位置を計算
            Vector3 newPosition = new Vector3(
                _centerPos.x + _radiusA * Mathf.Cos(_currentAngle),// X座標
                _centerPos.y + _radiusB * Mathf.Sin(_currentAngle),// Y座標
                _centerPos.z);// Z座標（固定）

            transform.position = newPosition;
        }
    }

    /// <summary>
    /// トリガー内に他のオブジェクトが侵入してきた際に呼び出される関数
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー接触判定
        if (other.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }  
    }

    /// <summary>
    /// プレイヤーの停止処理を開始する関数
    /// </summary>
    public void StopPlayer()
    {
        // 停止処理が有効で、現在停止中でない場合にのみコルーチンを開始
        if (_isStopON)
        {
            _isStopON = false;// 停止フラグをOFFに設定

            StartCoroutine(OnStop());// 停止コルーチンを開始
        }
    }

    /// <summary>
    /// プレイヤーの停止処理を行うコルーチン関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnStop()
    {
        _isActiving = false;// 操作有効フラグを無効化
        yield return new WaitForSeconds(_stopTime);// 指定された停止時間だけ待機

        // 停止時間が経過した後の処理
        _isActiving = true;// 操作有効フラグを再設定

        yield return new WaitForSeconds(_waitTime);// 少し待機してから
        _isStopON = true;// 停止フラグを再度ONに設定

        // プレイヤーが変な方向に移動しないように入力値をリセット
        _horizontalX = 0;
        _verticalY = 0;
    }

    /// <summary>
    /// プレイヤーが移動するための入力処理関数
    /// </summary>
    /// <param name="context"></param>
    public void MoveInput(InputAction.CallbackContext context)
    {
        // 操作が有効な場合にのみ入力値を処理
        if (_isActiving)
        {
            _moveInputValue = context.ReadValue<Vector2>();// 入力値を取得
            _horizontalX = _moveInputValue.x;// 水平方向の入力値を設定
            _verticalY = _moveInputValue.y;// 垂直方向の入力値を設定
            _isMoving = (Mathf.Abs(_verticalY) != 0 || Mathf.Abs(_horizontalX) != 0);// 移動状態を更新
        }
    }

    /// <summary>
    /// バリア発動の入力処理関数
    /// </summary>
    /// <param name="context"></param>
    public void BarrierInput(InputAction.CallbackContext context)
    {
        Debug.Log("Barrier!");
        Stage.Instance.EarthUnit.ActiveBarrier(_playerId, this);// バリアスキルIDを発動
    }
}