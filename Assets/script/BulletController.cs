using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float Speed = 20f;
    [SerializeField] float LifeTime = 1.5f;

    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * Speed;  // ÅuëOÅvÇ…îÚÇŒÇ∑

    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed / 10);
        Destroy(gameObject, LifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
