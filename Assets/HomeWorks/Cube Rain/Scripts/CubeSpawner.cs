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
        [SerializeField] private float _repeatRate;
        [SerializeField] private int _poolCapacity;
        [SerializeField] private int _poolMaxSize;

        private float RandomValue => Random.value;

        private ObjectPool<Cube> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Cube>
                (
                createFunc: () => Create(),
                actionOnGet: (obj) => Get(obj),
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

        private Cube Create()
        {
            Cube gameObject = Instantiate(_prefab);
            return gameObject;
        }

        public void Release(Cube obj)
        {
            obj.Died -= Release;

            _pool.Release(obj);
        }

        private void Start()
        {
            InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
        }

        private void Get(Cube obj)
        {
            SetRandomPositionAt(obj);
            obj.Reset();
            obj.gameObject.SetActive(true);
        }

        private void GetCube()
        {
            Cube obj = _pool.Get();
            obj.Died += Get;
        }

        private void SetRandomPositionAt(Cube Obj)
        {
            float positionX = Mathf.Lerp(_startSpawnX, _endSpawnX, RandomValue);
            float positionY = Mathf.Lerp(_startSpawnY, _endSpawnY, RandomValue);
            float positionZ = Mathf.Lerp(_startSpawnZ, _endSpawnZ, RandomValue);

            Obj.transform.position = new Vector3(positionX, positionY, positionZ);
        }
    }
}

