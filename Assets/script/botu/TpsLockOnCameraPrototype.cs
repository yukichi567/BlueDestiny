using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsLockOnCameraPrototype : MonoBehaviour
{
    /// <summary>
    /// �����L�����N�^�[
    /// </summary>
    [SerializeField]
    private Transform _attachTarget = null;

    /// <summary>
    /// �����L�����N�^�[����̃J�����I�t�Z�b�g�ʒu
    /// </summary>
    [SerializeField]
    private Vector3 _attachOffset = new Vector3(0f, 2f, -5f);

    /// <summary>
    /// �����^�[�Q�b�g
    /// </summary>
    [SerializeField]
    private Transform _lookTarget = null;

    /// <summary>
    /// ���݂̒����_
    /// </summary>
    private Vector3 _lookTargetPosition = Vector3.zero;


    private void LateUpdate()
    {
        _lookTargetPosition = _lookTarget.position;

        // �^�[�Q�b�g�ւ̃x�N�g��
        Vector3 targetVector = _lookTargetPosition - _attachTarget.position;

        // �^�[�Q�b�g�ւ̃x�N�g����O���Ƃ���N�H�[�^�j�I��
        Quaternion targetRotation = targetVector != Vector3.zero ? Quaternion.LookRotation(targetVector) : transform.rotation;

        // �ʒu�ƌ���
        Vector3 position = _attachTarget.position + targetRotation * _attachOffset;
        Quaternion rotation = Quaternion.LookRotation(_lookTargetPosition - position);

        transform.SetPositionAndRotation(position, rotation);
    }
}