using UnityEngine;

/// <summary>
/// Rigidbody ���g���ăv���C���[�𓮂����R���|�[�l���g
/// ���͂��󂯎��A����ɏ]���ăI�u�W�F�N�g�𓮂���
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerControllerM : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("�ړ��Ɏg����")] [SerializeField] float _movePower = 5f;
    [Tooltip("�ő�ړ����x")] [SerializeField] float _maxSpeed = 5f;
    [Tooltip("�����]���̑���")] [SerializeField] float _turnSpeed = 3f;
    [Tooltip("�W�����v��")] [SerializeField] float _jumpPower = 5f;
    [Tooltip("�ڒn����")][SerializeField] bool grounded = true;
    [Tooltip("�n�ʂƔ��肷�郌�C���[��ݒ肷��")] [SerializeField] LayerMask _groundLayer;
    [Tooltip("�ڒn����̊J�n�n�_�ɑ΂��� Pivot ����̃I�t�Z�b�g")] [SerializeField] Vector3 _groundCheckStartOffset = Vector3.zero;
    [Tooltip("�ڒn����̏I�_�ɑ΂��� Pivot ����̃I�t�Z�b�g")] [SerializeField] Vector3 _groundCheckEndOffset = Vector3.zero;
    [Header("Sound Effects")]
    Rigidbody _rb;
    Animator _anim;
    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = true;
    /// <summary>�ړ������̓��͒l</summary>
    float _h, _v;
    [Tooltip("�u�[�X�g")] 
    [SerializeField] float _boostMax = 100f;
    float _boost;   
    

    float _hovertimer;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �����̓��͂��擾���A���������߂�
        _v = Input.GetAxisRaw("Vertical");
        _h = Input.GetAxisRaw("Horizontal");

        // �W�����v�̓��͂��擾���A�ڒn���Ă��鎞�ɉ�����Ă�����W�����v����
        if (Input.GetKeyDown("space") && _isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // �ڒn����
        Vector3 start = _groundCheckStartOffset + transform.position;
        Vector3 end = _groundCheckEndOffset + transform.position;
        Debug.DrawLine(start, end);
        _isGrounded = Physics.Linecast(start, end, _groundLayer);
        Vector3 dir = new Vector3(_h, 0, _v);
        Vector3 velo = _rb.velocity;

        // �n��ړ�
        if (_isGrounded)  
        {
            _boost = _boostMax;
            // ���͂��Ȃ��ꍇ�͂����ɒ�߂�
            if (dir == Vector3.zero)
            {
                _rb.velocity = new Vector3(0, velo.y, 0);
                return;
            }

            velo.y = 0;

            if (velo.magnitude < _maxSpeed) // �ő�ړ����x�𒴂��Ă��鎞�͗͂������Ȃ�
            {
                // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɗ͂�������
                dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
                dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���
                _rb.AddForce(dir * _movePower);

                // ���͕����Ɋ��炩�ɉ�]������
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
    /// �W�����v����B
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
