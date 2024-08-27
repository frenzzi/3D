using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace CubeRain
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private float _startSpawnX;
        [SerializeField] private float _endSpawnX;
        [SerializeField] private float _startSpawnY;
        [SerializeField] private float _endSpawnY;
        [SerializeField] private float _startSpawnZ;
        [SerializeField] private float _endSpawnZ;
        [SerializeField] private Cube _prefab;
        [SerializeField] private float _repeateRate;
        [SerializeField] private int _poolCapacity;
        [SerializeField] private int _poolMaxSize;

        private float RandomValue => Random.value;

        private ObjectPool<Cube> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Cube>
                (
                createFunc: () => Create(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
                );
        }

        private void OnValidate()
        {
            if (_startSpawnX > _endSpawnX)
                _endSpawnX = _startSpawnX;

            if (_startSpawnY > _endSpawnY)
                _endSpawnY =_startSpawnY ;

            if (_startSpawnZ > _endSpawnZ)
                _endSpawnZ = _startSpawnZ;
        }

        private void Start()
        {
            StartCoroutine(Repeater());
        }

        private Cube Create()
        {
            return Instantiate(_prefab);
        }

        public void Release(Cube obj)
        {
            _pool.Release(obj);
            obj.Died -= Release;
        }

        private void ActionOnGet(Cube obj)
        {
            obj.Died += Release;

            SetRandomPositionAt(obj);
            obj.Reset();
            obj.gameObject.SetActive(true);
        }

        private void GetCube()
        {
            Cube obj = _pool.Get();
        }

        private void SetRandomPositionAt(Cube obj)
        {
            float positionX = Mathf.Lerp(_startSpawnX, _endSpawnX, RandomValue);
            float positionY = Mathf.Lerp(_startSpawnY, _endSpawnY, RandomValue);
            float positionZ = Mathf.Lerp(_startSpawnZ, _endSpawnZ, RandomValue);

            obj.transform.position = new Vector3(positionX, positionY, positionZ);
        }

        private IEnumerator Repeater()
        {
            while (isActiveAndEnabled)
            {
                GetCube();

                yield return new WaitForSeconds(_repeateRate);
            }
        }
    }
}

