using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("�ړ����x")] [SerializeField] float moveSpeed = 3;
    [Header("HP")][SerializeField] float _Hp = 90;
    [Tooltip("�ݒu����")] [SerializeField] bool _isGrounded = true;
    [Tooltip("�n�ʂƔ��肷�郌�C���[��ݒ肷��")] [SerializeField] LayerMask _groundLayer;
    int interval = 0;
    GameObject _target;
    Rigidbody _rb;
    CameraController _cameraController;

    void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _target = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody>();
        _cameraController.AddEnemy(transform);
    }

    void Update()
    {        
        transform.LookAt(_target.transform);
        transform.position += transform.forward * moveSpeed;
        if(_Hp <= 0)
        {
            _cameraController.RemoveEnemy(transform);
            Destroy(this.gameObject);
        }
    }
    //void FixedUpdate()
    //{
    //    // �ڒn����
    //    Vector3 start = _groundCheckStartOffset + transform.position;
    //    Vector3 end = _groundCheckEndOffset + transform.position;
    //    Debug.DrawLine(start, end);
    //    _isGrounded = Physics.Linecast(start, end, _groundLayer);


    //    // �ړ�
    //    if (_isGrounded)    // �󒆂ł͈ړ��ł��Ȃ�
    //    {

    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            _Hp -= 1;
        }
    }
}
