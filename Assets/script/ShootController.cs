using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 弾/レーザーを発射する機能を提供する。
/// Fire1 でプレハブの弾を発射する
/// Fire2 で Line Renderer を使ったレーザーを発射する
/// </summary>
public class ShootController : MonoBehaviour
{
    /// <summary>「弾」のプレハブ</summary>
    [SerializeField] GameObject _bulletPrefab = default;
    [SerializeField] float interval = 1f;
    [SerializeField] float bulletTimer = 1f;
    [SerializeField] float bulletAmmo = 60f;
    //「誘導弾」のプレハブ
    [SerializeField] GameObject missilePrefab = default;
    /// <summary>弾/レーザーを発射する地点を設定する</summary>
    [SerializeField] Transform _muzzle = default;
    /// <summary>照準のオブジェクト</summary>
    [SerializeField] Image _crosshair = default;
    /// <summary>照準に何も捉えていない時の照準の色</summary>
    [SerializeField] Color _defaultCrosshairColor = Color.white;
    /// <summary>照準にターゲットを捉えた時の昇順の色</summary>
    [SerializeField] Color _lockedCrosshairColor = Color.red;
    /// <summary>レーザーの射程距離</summary>
    [SerializeField] float _shootRange = 20f;
    /// <summary>照準で捉えられるターゲットのレイヤー</summary>
    [SerializeField] LayerMask _layerMask = default;
    /// <summary>レーザーを描くための Line Renderer</summary>
    [SerializeField] LineRenderer _line = default;
    /// <summary>レーザーが当たった時に加える力</summary>
    [SerializeField] float _shootPower = default;

    void Update()
    {
        bulletTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && bulletTimer > interval && bulletAmmo > 0)
        {
            var go = Instantiate(_bulletPrefab);
            go.transform.position = _muzzle.position;
            go.transform.forward = _muzzle.forward;
            bulletTimer = 0;
            bulletAmmo -= 1;

        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            bulletAmmo = 60;
        }

        // カメラから照準に向かって Ray を飛ばし、何かに当たっているか調べる
        Ray ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit = default;
        Vector3 hitPosition = _muzzle.transform.position + _muzzle.transform.forward * _shootRange;  // hitPosition は Ray が当たった場所。Line の終点となる。初期値（何にも当たっていない時）は Muzzle から射程距離だけ前方にする。
        Collider hitCollider = default;    // Ray が当たったコライダー

        // Ray が何かに当たったか・当たっていないかで処理を分ける        
        if (Physics.Raycast(ray, out hit, _shootRange, _layerMask))
        {
            _crosshair.color = _lockedCrosshairColor;
            hitPosition = hit.point;    // Ray が当たった場所
            hitCollider = hit.collider;    // Ray が当たったオブジェクト
        }
        else
        {
            _crosshair.color = _defaultCrosshairColor;
        }

        //if (Input.GetButton("Fire2"))
        //{
 
        //}
        //else
        //{
        //    DrawLaser(_muzzle.position);   // 撃っていない時は、Line の終点と始点を同じ位置にすることで Line を消す
        //}
    }

    /// <summary>
    /// Line Renderer を使ってレーザーを描く
    /// </summary>
    /// <param name="destination">レーザーの終点</param>
    void DrawLaser(Vector3 destination)
    {
        Vector3[] positions = { _muzzle.position, destination };   // レーザーの始点は常に Muzzle にする
        _line.positionCount = positions.Length;   // Line を終点と始点のみに制限する
        _line.SetPositions(positions);
    }

    /// <summary>
    /// ショットがオブジェクトに当たった時に呼び出す。
    /// </summary>
    /// <param name="hitObject"></param>
    void Hit(Collider collider)
    {
        // 今回は「当たったオブジェクトに Rigidbody コンポーネントがアタッチされていたら、"メインカメラの方向＋上"に力を加える」処理をする
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();

        if (rb)
        {
            rb.AddForce((Camera.main.transform.forward + Vector3.up) * _shootPower, ForceMode.Impulse);
        }
    }
}
