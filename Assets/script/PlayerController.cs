using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody _rb;

    [Header("移動速度")]
    [SerializeField] float _moveSpeed = 3;

    [Header("ジャンプ力、回数")]
    [SerializeField] float _jumpPower = 10;
    [SerializeField] float _maxJumpCount = 2;
    float _jumpCount;

    [Header("ブースト量、スピード")]
    [SerializeField] float _boostMax = 100f;
    [SerializeField] float _boostSpeed = 3;
    [SerializeField] float _maxBoostTime = 1;
    float _boostTime;
    float _boost;

    [Header("浮遊")]

    [Header("接地判定")]
    [SerializeField] bool _isGround = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); 

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        // カメラのローカル座標系を基準に dir を変換する
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        _rb.velocity = dir * _moveSpeed + _rb.velocity.y * Vector3.up;  // Y 軸方向の速度は変えず、XZ 軸方向に移動する

        //transform.forward = dir.normalized;
        // 方向の入力がない場合は何もしない, 入力されたらその方向を向く
        //if (dir != Vector3.zero)
        //{
        //    //this.transform.forward = dir;
        //    transform.forward = dir.normalized;
        //}

        if (Input.GetButton("Fire1"))
        {
            if (dir != Vector3.zero)
            {
                this.transform.forward = dir;
            }
        }

        if (Input.GetKeyDown("space") && _jumpCount < _maxJumpCount) Jump();       
        //if(Input.GetKeyDown("Q") && _boost < _boostMax)Boost();           
    }  

    void FixedUpdate()
    {
         
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "")
        {

        }        
    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Ground"))
        {
            _isGround = true;
            _jumpCount = 0;
            _boost = 0;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }
    void Boost()
    {
        _boostTime += Time.deltaTime;
        _boost += 20;

        if (_boostTime < _maxBoostTime)
        {
            _rb.AddForce(10, 0, 0);
        }
    }
            
    void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);
        //_rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        _jumpCount += 1;
    }
    
}
