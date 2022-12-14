using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シューティングゲームの自機を操作するためのコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController2D : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    /// <summary>プレイヤーの回転速度</summary>
    [SerializeField] float m_rotateSpeed = 5f;
    /// <summary>通常弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    /// <summary>チャージショットのプレハブ</summary>
    [SerializeField] GameObject m_chargeShotPrefab;
    /// <summary>チャージショットを撃つために必要なチャージ時間（単位: 秒）</summary>
    [SerializeField] float m_chargeTime = 1f;
    /// <summary>弾の発射位置</summary>
    [SerializeField] Transform m_muzzle;
    /// <summary>一画面の最大弾数</summary>
    [SerializeField] int m_bulletLimit = 3;

    /// <summary>チャージ時間を計測するタイマー</summary>
    float m_chargeTimer;
    Rigidbody2D m_rb2d;
    Animator m_anim;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        m_rb2d.velocity = transform.up * m_moveSpeed;   // 直進させる

        float h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する

        if (h != 0)
        {
            transform.Rotate(transform.forward, m_rotateSpeed * Time.deltaTime * h * -1);   // 回転させる
        }

        // 左クリックまたは左 Ctrl で弾を発射する（単発）
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.GetComponentsInChildren<BulletController2D>().Length < m_bulletLimit)    // 画面内の弾数を制限する
            {
                Fire(m_bulletPrefab, false);
            }
        }

        // 右クリックまたは左 Alt で弾を発射する（チャージショット）
        if (Input.GetButton("Fire2"))
        {
            m_chargeTimer += Time.deltaTime;

            if (m_anim)
            {
                if (m_chargeTimer <= m_chargeTime)
                {
                    m_anim.Play("Charging");
                }
                else
                {
                    m_anim.Play("Charged");
                }
            }
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            if (m_chargeTimer > m_chargeTime)
            {
                Fire(m_chargeShotPrefab);
            }

            m_chargeTimer = 0;

            if (m_anim)
            {
                m_anim.Play("Default");
            }
        }
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    /// <param name="bullet">弾のオブジェクト（プレハブ）</param>
    /// <param name="isCreatedAsChild">生成した弾を自分の子オブジェクトにするか</param>
    void Fire(GameObject bullet, bool isCreatedAsChild = false)
    {
        GameObject go = Instantiate(bullet, m_muzzle.position, m_muzzle.rotation);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
        
        if (isCreatedAsChild)
        {
            go.transform.SetParent(this.transform);
        }
    }
}