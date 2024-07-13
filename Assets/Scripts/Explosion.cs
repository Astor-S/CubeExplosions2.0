using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _baseRadius = 2f;
    [SerializeField] private float _power = 10f;

    public void Explode(Vector3 explosionPosition, List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            float radius = _baseRadius * cube.transform.localScale.x;
            cube.AddForce(_power, explosionPosition, radius);
        }
    }
}