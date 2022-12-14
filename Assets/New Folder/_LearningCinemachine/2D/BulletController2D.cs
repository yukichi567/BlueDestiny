using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シューティングゲームで自機から発射される弾を制御するコンポーネント
/// 弾はインスタンス化されたら自ら飛んでいく
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BulletController2D : MonoBehaviour
{
    /// <summary>弾の飛ぶ速度</summary>
    [SerializeField] float m_bulletSpeed = 10f;
    /// <summary>弾の飛ぶ距離</summary>
    [SerializeField] float m_travelDistance = 10f;

    Rigidbody2D m_rb2d;
    GameObject m_player;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        Vector3 v = transform.up * m_bulletSpeed;   // 弾が飛ぶ速度ベクトルを計算する
        m_rb2d.velocity = v;                      // 速度ベクトルを弾にセットする
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(m_player.transform.position, transform.position);
        if (distance > m_travelDistance)
        {
            Destroy(gameObject);
        }
    }
}