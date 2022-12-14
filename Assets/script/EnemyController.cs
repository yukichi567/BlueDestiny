using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb = default;
    [Header("ˆÚ“®‘¬“x")]
    [SerializeField] float moveSpeed = 3;

    [Header("HP")]
    [SerializeField] float EnemyHp = 90;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
