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
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(_enemyPrefub);
            //生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = new Vector3(0, 10, 20);
            //経過時間を初期化して再度時間計測を始める
            _time = 0f;
        }
    }
}
