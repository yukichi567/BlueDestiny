using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    int _enemyTransformIndex = 0;

    //[SerializeField] CinemachineVirtualCamera _cvc;

    [Header("Pattern2")]
    [SerializeField] Transform[] _enemyTransforms;
    [SerializeField] CinemachineVirtualCamera[] _cvcs;

    void Update()
    {
        CameraLook();
    }

    void CameraLook()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            _cvcs[_enemyTransformIndex].Priority = 0;
            _enemyTransformIndex++;
            _enemyTransformIndex = _enemyTransformIndex % _enemyTransforms.Length;
        }

        _cvcs[_enemyTransformIndex].Priority = 20;
    }
}
