using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = default;
    [Header("移動速度")]
    [SerializeField] float moveSpeed = 3;

    [Header("ジャンプ力、回数")]
    [SerializeField] float jumpPower = 10;
    [SerializeField] float jumpCount = 3;

    [SerializeField] int boost = 100;

    [Header("接地判定")]
    [SerializeField] bool grounded = true;

    [Header("メインカメラ")]
    [SerializeField] GameObject MainCamera = default;
    private float front;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");        

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // カメラのローカル座標系を基準に dir を変換する
        dir = Camera.main.transform.TransformDirection(dir);
        // カメラは斜め下に向いているので、Y 軸の値を 0 にして「XZ 平面上のベクトル」にする
        dir.y = 0;
        // 移動の入力がない時は回転させない。入力がある時はその方向にキャラクターを向ける。
        if (dir != Vector3.zero) this.transform.forward = dir;

        //垂直方向の速度を保持する
        Vector3 velocity = dir.normalized * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        //ジャンプ
        if (boost >= 15)
        {
            if (Input.GetKeyDown("space") && jumpCount >= 0)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                boost -= 15;
                jumpCount -= 1;
                grounded = false; 
            }

            if(Input.GetKey("Q"))
            {
                if(Input.GetKeyDown("W"))
                {

                }
            }

        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "ground")
        {
            grounded = true;
            boost = 100;
            jumpCount = 3;
        }        
    }
}
