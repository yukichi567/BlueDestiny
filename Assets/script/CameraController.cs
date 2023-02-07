using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    int _enemyTransformIndex = 0;

    //[SerializeField] CinemachineVirtualCamera _cvc;

    [Header("Pattern2")]
    List<Transform> _enemyTransforms = new();
    List<CinemachineVirtualCamera> _cvcs = new();

    void Update()
    {
        CameraLook();
    }

    void CameraLook()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            ChangeCamera();
        }

        _cvcs[_enemyTransformIndex].Priority = 20;
    }

    void ChangeCamera()
    {
        _cvcs[_enemyTransformIndex].Priority = 0;
        _enemyTransformIndex++;
        _enemyTransformIndex = _enemyTransformIndex % _enemyTransforms.Count;
    }

    public void AddEnemy(Transform transform)
    {
        _enemyTransforms.Add(transform);
        _cvcs.Add(transform.GetChild(0).GetComponent<CinemachineVirtualCamera>());
    }

    public void RemoveEnemy(Transform transform)
    {
        if(_enemyTransformIndex == _enemyTransformIndex % _enemyTransforms.Count)
        {
            ChangeCamera();
        }
        _enemyTransforms.Remove(transform);
        _cvcs.Remove(transform.GetChild(0).GetComponent<CinemachineVirtualCamera>());
    }
}
