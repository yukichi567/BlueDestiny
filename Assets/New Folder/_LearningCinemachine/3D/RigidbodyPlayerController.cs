using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// ControlType を設定することで、オールドタイプ（ラジコン型）と現代的なタイプの操作系を切り替えられる
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerController : MonoBehaviour
{
    /// <summary>操作系のタイプ</summary>
    [SerializeField] ControlType m_controlType = ControlType.MoveWithSmoothTurn;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 3f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float m_jumpPower = 5f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 1.1f;
    /// <summary>キャラクターの Animator</summary>
    [SerializeField] Animator m_anim;

    Rigidbody m_rb;


    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ControlType と入力に応じてキャラクターを動かす
        if (m_controlType == ControlType.Turn)
        {
            // 左右で回転させる
            if (h != 0)
            {
                this.transform.Rotate(this.transform.up, h * m_turnSpeed);
            }

            // 上下で前後移動する。ジャンプした時の y 軸方向の速度は保持する。
            Vector3 velo = this.transform.forward * m_movingSpeed * v;
            velo.y = m_rb.velocity.y;
            m_rb.velocity = velo;
        }
        else if (m_controlType == ControlType.Move || m_controlType == ControlType.MoveWithSmoothTurn)
        {
            // 入力方向のベクトルを組み立てる
            Vector3 dir = Vector3.forward * v + Vector3.right * h;

            if (dir == Vector3.zero)
            {
                // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
            else
            {
                // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
                dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
                dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

                if (m_controlType == ControlType.Move)
                {
                    this.transform.forward = dir;   // 入力した方向にオブジェクトを向ける
                }
                else if (m_controlType == ControlType.MoveWithSmoothTurn)
                {
                    // 入力方向に滑らかに回転させる
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);
                }
                
                Vector3 velo = dir.normalized * m_movingSpeed; // 入力した方向に移動する
                velo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
                m_rb.velocity = velo;   // 計算した速度ベクトルをセットする
            }
        }

        // Animator Controller のパラメータをセットする
        if (m_anim)
        {
            // 攻撃ボタンを押された時の処理
            if (Input.GetButtonDown("Fire1") && IsGrounded())
            {
                m_anim.SetTrigger("Attack");
            }

            // 水平方向の速度を Speed にセットする
            Vector3 velocity = m_rb.velocity;
            velocity.y = 0f;
            m_anim.SetFloat("Speed", velocity.magnitude);

            // 地上/空中の状況に応じて IsGrounded をセットする
            if (m_rb.velocity.y <= 0f && IsGrounded())
            {
                m_anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);

            // Animator Controller のパラメータをセットする
            if (m_anim)
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }
    }

    /// <summary>
    /// 地面に接触しているか判定する
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: 体の中心
        Vector3 end = start + Vector3.down * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
}

public enum ControlType
{
    /// <summary>初代バイオハザードのようなラジコン操作</summary>
    Turn,
    /// <summary>カメラを基準とした方向に移動する</summary>
    Move,
    /// <summary>カメラを基準とした方向に移動する。方向転換の際には滑らかに回転する</summary>
    MoveWithSmoothTurn,
}