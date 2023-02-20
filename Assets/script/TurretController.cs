using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float _hp = 10;
    [SerializeField] private Transform _player = default;
    [SerializeField] float _range = 50f;
    //弾」のプレハブ
    [SerializeField] GameObject _bulletPrefab = default;
    [SerializeField] float rate = 0.5f;
    [SerializeField] float interval = 10f;
    [SerializeField] float bulletTimer = 1f;
    [SerializeField] float bulletAmmo = 30f;
    float _relodetime = 10f;
    //弾を発射する地点
    [SerializeField] Transform _muzzle1 = default;

    float _bullettimer;
    float _timer;

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(this.transform.position, _player.transform.position);
        _bullettimer += Time.deltaTime;        
        if (dis < _range)
        {
            transform.LookAt(_player.transform.position);
            Shot();
        }

        if (bulletAmmo == 0)
        {
            _timer += Time.deltaTime;
            if(_relodetime < _timer)
            {
                bulletAmmo = 30;
            }
        }
    }

    private void Shot()
    {
        if (bulletTimer > interval && bulletAmmo > 0)
        {
            _bulletPrefab = Instantiate(_bulletPrefab);
            _bulletPrefab.transform.position = _muzzle1.position;
            _bulletPrefab.transform.forward = _muzzle1.forward;
            _bullettimer = 0;
            bulletAmmo--;
        }
    }
}
