using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _enemyPrefub;

    [SerializeField] private float _interval = 0f;
    private float _time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if(_time >= _interval)
        {
            //enemy���C���X�^���X������(��������)
            GameObject enemy = Instantiate(_enemyPrefub);
            //���������G�̍��W�����肷��(����X=0,Y=10,Z=20�̈ʒu�ɏo��)
            enemy.transform.position = new Vector3(0, 10, 20);
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            _time = 0f;
        }
    }
}
