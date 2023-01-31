using UnityEngine;

/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerControllerM : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("移動に使う力")] [SerializeField] float _movePower = 5f;
    [Tooltip("最大移動速度")] [SerializeField] float _maxSpeed = 5f;
    [Tooltip("方向転換の速さ")] [SerializeField] float _turnSpeed = 3f;
    [Tooltip("ジャンプ力")] [SerializeField] float _jumpPower = 5f;
    [Tooltip("接地判定")][SerializeField] bool grounded = true;
    [Tooltip("地面と判定するレイヤーを設定する")] [SerializeField] LayerMask _groundLayer;
    [Tooltip("接地判定の開始地点に対する Pivot からのオフセット")] [SerializeField] Vector3 _groundCheckStartOffset = Vector3.zero;
    [Tooltip("接地判定の終点に対する Pivot からのオフセット")] [SerializeField] Vector3 _groundCheckEndOffset = Vector3.zero;
    [Header("Sound Effects")]
    Rigidbody _rb;
    Animator _anim;
    /// <summary>接地フラグ</summary>
    bool _isGrounded = true;
    /// <summary>移動方向の入力値</summary>
    float _h, _v;
    [Tooltip("ブースト")] 
    [SerializeField] float _boostMax = 100f;
    float _boost;   
    

    float _hovertimer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        _v = Input.GetAxisRaw("Vertical");
        _h = Input.GetAxisRaw("Horizontal");

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetKeyDown("space") && _isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // 接地判定
        Vector3 start = _groundCheckStartOffset + transform.position;
        Vector3 end = _groundCheckEndOffset + transform.position;
        Debug.DrawLine(start, end);
        _isGrounded = Physics.Linecast(start, end, _groundLayer);
        Vector3 dir = new Vector3(_h, 0, _v);
        Vector3 velo = _rb.velocity;

        // 地上移動
        if (_isGrounded)  
        {
            _boost = _boostMax;
            // 入力がない場合はすぐに停める
            if (dir == Vector3.zero)
            {
                _rb.velocity = new Vector3(0, velo.y, 0);
                return;
            }

            velo.y = 0;

            if (velo.magnitude < _maxSpeed) // 最大移動速度を超えている時は力を加えない
            {
                // カメラを基準に入力が上下=奥/手前, 左右=左右に力を加える
                dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
                dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする
                _rb.AddForce(dir * _movePower);

                // 入力方向に滑らかに回転させる
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed);
            }
        }
        else 
        {
            _hovertimer = Time.deltaTime;
            if(_hovertimer > 1)
            {
                Hover();
            }
            
        }
    }

    /// <summary>
    /// ジャンプする。
    /// </summary>
    void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);        
    }
    void Hover()
    {
        if(Input.GetKeyDown("space") && _boost > 0)
        {          

        }
    }
}
