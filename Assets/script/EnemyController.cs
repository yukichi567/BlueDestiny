using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("移動速度")] [SerializeField] float moveSpeed = 3;
    [Header("HP")][SerializeField] float _Hp = 90;
    [Tooltip("設置判定")] [SerializeField] bool _isGrounded = true;
    [Tooltip("地面と判定するレイヤーを設定する")] [SerializeField] LayerMask _groundLayer;
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
    //    // 接地判定
    //    Vector3 start = _groundCheckStartOffset + transform.position;
    //    Vector3 end = _groundCheckEndOffset + transform.position;
    //    Debug.DrawLine(start, end);
    //    _isGrounded = Physics.Linecast(start, end, _groundLayer);


    //    // 移動
    //    if (_isGrounded)    // 空中では移動できない
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
