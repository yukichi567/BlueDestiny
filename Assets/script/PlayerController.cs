using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = default;
    [Header("�ړ����x")]
    [SerializeField] float moveSpeed = 3;

    [Header("�W�����v�́A��")]
    [SerializeField] float jumpPower = 10;
    [SerializeField] float jumpCount = 3;

    [SerializeField] int boost = 100;

    [Header("�ڒn����")]
    [SerializeField] bool grounded = true;

    [Header("���C���J����")]
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
        // �J�����̃��[�J�����W�n����� dir ��ϊ�����
        dir = Camera.main.transform.TransformDirection(dir);
        // �J�����͎΂߉��Ɍ����Ă���̂ŁAY ���̒l�� 0 �ɂ��āuXZ ���ʏ�̃x�N�g���v�ɂ���
        dir.y = 0;
        // �ړ��̓��͂��Ȃ����͉�]�����Ȃ��B���͂����鎞�͂��̕����ɃL�����N�^�[��������B
        if (dir != Vector3.zero) this.transform.forward = dir;

        //���������̑��x��ێ�����
        Vector3 velocity = dir.normalized * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        //�W�����v
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
