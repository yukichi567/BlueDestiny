using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    /// �����^�[�Q�b�g
    [SerializeField]
    public Transform target = null;

    /// ���b�N�؂�ւ�����
    [SerializeField]
    private float _changeDuration = 0.1f;

    /// ���b�N�؂�ւ��^�C�}�[
    private float _timer = 0f;
    void Update()
    {
        transform.LookAt(target.transform);
    }

    public void ChangeTarget(Transform target)
    {
        //_latestTargetPosition = _lookTargetPosition;
        //_lookTarget = target;

        //_timer = 0f;
    }
}