using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �e/���[�U�[�𔭎˂���@�\��񋟂���B
/// Fire1 �Ńv���n�u�̒e�𔭎˂���
/// Fire2 �� Line Renderer ���g�������[�U�[�𔭎˂���
/// </summary>
public class ShootController : MonoBehaviour
{
    /// <summary>�u�e�v�̃v���n�u</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    [SerializeField] float interval = 1f;
    [SerializeField] float bulletTimer = 1f;
    [SerializeField] float bulletAmmo = 60f;
    //�u�U���e�v�̃v���n�u
    [SerializeField] GameObject missilePrefab = default;
    /// <summary>�e/���[�U�[�𔭎˂���n�_��ݒ肷��</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>�Ə��̃I�u�W�F�N�g</summary>
    [SerializeField] Image _crosshair = default;
    /// <summary>�Ə��ɉ��������Ă��Ȃ����̏Ə��̐F</summary>
    [SerializeField] Color _defaultCrosshairColor = Color.white;
    /// <summary>�Ə��Ƀ^�[�Q�b�g�𑨂������̏����̐F</summary>
    [SerializeField] Color _lockedCrosshairColor = Color.red;
    /// <summary>���[�U�[�̎˒�����</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>�Ə��ő�������^�[�Q�b�g�̃��C���[</summary>
    [SerializeField] LayerMask _layerMask = default;
    /// <summary>���[�U�[��`�����߂� Line Renderer</summary>
    [SerializeField] LineRenderer _line = default;
    /// <summary>���[�U�[�������������ɉ������</summary>
    [SerializeField] float _shootPower = default;

    void Update()
    {
        bulletTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && bulletTimer > interval && bulletAmmo > 0)
        {
            var go = Instantiate(_bulletPrefab);
            go.transform.position = _muzzle.position;
            go.transform.forward = _muzzle.forward;
            bulletTimer = 0;
            bulletAmmo -= 1;

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            bulletAmmo = 60;
        }

        // �J��������Ə��Ɍ������� Ray ���΂��A�����ɓ������Ă��邩���ׂ�
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;
        Vector3 hitPosition = _muzzle.transform.position + _muzzle.transform.forward * _shootRange;  // hitPosition �� Ray �����������ꏊ�BLine �̏I�_�ƂȂ�B�����l�i���ɂ��������Ă��Ȃ����j�� Muzzle ����˒����������O���ɂ���B
        Collider hitCollider = default;    // Ray �����������R���C�_�[

        // Ray �������ɓ����������E�������Ă��Ȃ����ŏ����𕪂���        
        if (Physics.Raycast(ray, out hit, _shootRange, _layerMask))
        {
            _crosshair.color = _lockedCrosshairColor;
            hitPosition = hit.point;    // Ray �����������ꏊ
            hitCollider = hit.collider;    // Ray �����������I�u�W�F�N�g
        }
        else
        {
            _crosshair.color = _defaultCrosshairColor;
        }

        //if (Input.GetButton("Fire2"))
        //{
 
        //}
        //else
        //{
        //    DrawLaser(_muzzle.position);   // �����Ă��Ȃ����́ALine �̏I�_�Ǝn�_�𓯂��ʒu�ɂ��邱�Ƃ� Line ������
        //}
    }

    /// <summary>
    /// Line Renderer ���g���ă��[�U�[��`��
    /// </summary>
    /// <param name="destination">���[�U�[�̏I�_</param>
    void DrawLaser(Vector3 destination)
    {
        Vector3[] positions = { _muzzle.position, destination };   // ���[�U�[�̎n�_�͏�� Muzzle �ɂ���
        _line.positionCount = positions.Length;   // Line ���I�_�Ǝn�_�݂̂ɐ�������
        _line.SetPositions(positions);
    }

    /// <summary>
    /// �V���b�g���I�u�W�F�N�g�ɓ����������ɌĂяo���B
    /// </summary>
    /// <param name="hitObject"></param>
    void Hit(Collider collider)
    {
        // ����́u���������I�u�W�F�N�g�� Rigidbody �R���|�[�l���g���A�^�b�`����Ă�����A"���C���J�����̕����{��"�ɗ͂�������v����������
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.AddForce((Camera.main.transform.forward + Vector3.up) * _shootPower, ForceMode.Impulse);
        }
    }
}
