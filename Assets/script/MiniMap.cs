using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform _mother = default;

    void Start()
    {
        // A rotation 30 degrees around the y-axis
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
    }
    void Update()
    {
        var pos = _mother.transform.rotation;
        pos.y = transform.position.y;
        //transform.position = pos;
    }
}
