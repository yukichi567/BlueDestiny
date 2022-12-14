using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsLockOnCameraPrototype : MonoBehaviour
{
    /// <summary>
    /// 取りつくキャラクター
    /// </summary>
    [SerializeField]
    private Transform _attachTarget = null;

    /// <summary>
    /// 取りつくキャラクターからのカメラオフセット位置
    /// </summary>
    [SerializeField]
    private Vector3 _attachOffset = new Vector3(0f, 2f, -5f);

    /// <summary>
    /// 注視ターゲット
    /// </summary>
    [SerializeField]
    private Transform _lookTarget = null;

    /// <summary>
    /// 現在の注視点
    /// </summary>
    private Vector3 _lookTargetPosition = Vector3.zero;


    private void LateUpdate()
    {
        _lookTargetPosition = _lookTarget.position;

        // ターゲットへのベクトル
        Vector3 targetVector = _lookTargetPosition - _attachTarget.position;

        // ターゲットへのベクトルを前方とするクォータニオン
        Quaternion targetRotation = targetVector != Vector3.zero ? Quaternion.LookRotation(targetVector) : transform.rotation;

        // 位置と向き
        Vector3 position = _attachTarget.position + targetRotation * _attachOffset;
        Quaternion rotation = Quaternion.LookRotation(_lookTargetPosition - position);

        transform.SetPositionAndRotation(position, rotation);
    }
}