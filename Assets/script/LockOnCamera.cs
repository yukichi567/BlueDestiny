using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    /// 注視ターゲット
    [SerializeField]
    public Transform target = null;

    /// ロック切り替え時間
    [SerializeField]
    private float _changeDuration = 0.1f;

    /// ロック切り替えタイマー
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