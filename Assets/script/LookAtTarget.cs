using UnityEngine;

/// <summary>
/// �^�[�Q�b�g�ɐU������X�N���v�g
/// </summary>
internal class LookAtTarget : MonoBehaviour
{
    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform _target;

    private void Update()
    {
        // �^�[�Q�b�g�̕����Ɏ��g����]������
        _self.LookAt(_target);
    }
}