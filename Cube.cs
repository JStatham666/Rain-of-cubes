using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minDelay = 2;
    [SerializeField] private int _maxDelay = 5;

    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;

    private bool _isTouched = false;

    public event Action<Cube> Destroyed;

    public void Init()
    {
        _isTouched = false;
        _rigidbody.velocity = Vector3.zero;
        _colorChanger.SetStartColor(_material);
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _isTouched == false)
        {
            _isTouched = true;
            _colorChanger.SetRandomColor(_material);
            StartCoroutine(CountDestroyDelay());
        }
    }

    private IEnumerator CountDestroyDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay));
        yield return delay;

        Destroyed?.Invoke(this);
    }
}