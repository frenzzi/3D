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
        [SerializeField] private CubeDestoyer _prefab;
        [SerializeField] private float _repeatRate;
        [SerializeField] private int _poolCapacity;
        [SerializeField] private int _poolMaxSize;

        private float RandomValue => Random.value;

        private ObjectPool<CubeDestoyer> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<CubeDestoyer>
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

        private CubeDestoyer Create()
        {
            CubeDestoyer gameObject = Instantiate(_prefab);
            gameObject.Died += ActionOnRelease;

            return gameObject;
        }
        public void ActionOnRelease(CubeDestoyer obj)
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _pool.Release(obj);
        }

        private void Start()
        {
            InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
        }

        private void ActionOnGet(CubeDestoyer obj)
        {
            SetRandomPositionAt(obj);
            obj.gameObject.SetActive(true);
        }

        private void GetCube()
        {
            _pool.Get();
        }

        private void SetRandomPositionAt(CubeDestoyer Obj)
        {
            float positionX = Mathf.Lerp(_startSpawnX, _endSpawnX, RandomValue);
            float positionY = Mathf.Lerp(_startSpawnY, _endSpawnY, RandomValue);
            float positionZ = Mathf.Lerp(_startSpawnZ, _endSpawnZ, RandomValue);

            Obj.transform.position = new Vector3(positionX, positionY, positionZ);
        }
    }
}

