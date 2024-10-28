using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefabe;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;
    [SerializeField] private int _minPositionX = -14;
    [SerializeField] private int _maxPositionX = 14;
    [SerializeField] private int _minPositionY = 28;
    [SerializeField] private int _maxPositionY = 30;
    [SerializeField] private int _minPositionZ = -14;
    [SerializeField] private int _maxPositionZ = 14;

    private ObjectPool<Cube> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefabe),
        actionOnGet: (cube) => OnGet(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start() =>
        StartCoroutine(GetCube());

    private IEnumerator GetCube()
    {
        WaitForSeconds delay = new WaitForSeconds(_repeatRate);

        while (true)
        {
            _cubes.Get();
            yield return delay;
        }
    }

    private void OnGet(Cube cube)
    {
        cube.Destroyed += CubeRelease;
        cube.Init();
        cube.transform.position = GetRandomPosition();
    }

    private void CubeRelease(Cube cube)
    {
        cube.Destroyed -= CubeRelease;
        _cubes.Release(cube);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(_minPositionX, _maxPositionX),
            Random.Range(_minPositionY, _maxPositionY),
            Random.Range(_minPositionZ, _maxPositionZ));
    }
}