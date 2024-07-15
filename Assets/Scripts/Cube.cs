using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private static readonly float s_attenuationMax = 1f;

    [SerializeField] private int _chanceSeparate = 100;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> Clicked;

    public int ChanceSeparate => _chanceSeparate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(Vector3 scale, int chanceSeparate)
    {
        transform.localScale = scale;
        _chanceSeparate = chanceSeparate;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void AddForce(float force, Vector3 explosionPosition, float radius)
    {
        Vector3 direction = transform.position - explosionPosition;
        float distance = direction.magnitude;

        if (distance > radius)
            return;

        float attenuation = s_attenuationMax - (distance / radius);
        
        _rigidbody.AddForce(direction.normalized * force * attenuation, ForceMode.Impulse);
    }

    public void Destroy()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }
}