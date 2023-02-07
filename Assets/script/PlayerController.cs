using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody _rb;

    [Header("�ړ����x")]
    [SerializeField] float _moveSpeed = 3;

    [Header("�W�����v�́A��")]
    [SerializeField] float _jumpPower = 10;
    [SerializeField] float _maxJumpCount = 2;
    float _jumpCount;

    [Header("�u�[�X�g�ʁA�X�s�[�h")]
    [SerializeField] float _boostMax = 100f;
    [SerializeField] float _boostSpeed = 3;
    [SerializeField] float _maxBoostTime = 1;
    float _boostTime;
    float _boost;

    [Header("���V")]

    [Header("�ڒn����")]
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
        // �J�����̃��[�J�����W�n����� dir ��ϊ�����
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        _rb.velocity = dir * _moveSpeed + _rb.velocity.y * Vector3.up;  // Y �������̑��x�͕ς����AXZ �������Ɉړ�����

        //transform.forward = dir.normalized;
        // �����̓��͂��Ȃ��ꍇ�͉������Ȃ�, ���͂��ꂽ�炻�̕���������
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
