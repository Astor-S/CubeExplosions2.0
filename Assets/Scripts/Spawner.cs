using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 7;
    [SerializeField] private int _minChance = 0;
    [SerializeField] private int _maxChance = 100;

    [SerializeField] private int _decreaseScale = 2;
    [SerializeField] private int _decreaseChance = 2;

    [SerializeField] private List<Cube> _cubes;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Explosion _explosion;

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            cube.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            cube.Clicked -= OnClicked;
    }

    private void OnClicked(Cube cube)
    {
        if (cube.ChanceSeparate >= UnityEngine.Random.Range(_minChance, _maxChance) && cube.ChanceSeparate > 0)
        {
            int countSpawn = RandomizeCount();
            
            List<Cube> createdCubes = new List<Cube>();

            for (int i = _minCount; i <= countSpawn; i++)
            {
                createdCubes.Add(InstantiateCubes(cube));
            }

            _cubes.Remove(cube);
            cube.Clicked -= OnClicked;
            _explosion.Explode(cube.transform.position, createdCubes);
        }
        else
        {
            _cubes.Remove(cube);
            cube.Clicked -= OnClicked;
            _explosion.ExplodeWithoutSeparation(cube.transform.position, _cubes);
            Destroy(cube.gameObject);
        }
    }

    private Cube InstantiateCubes(Cube cube)
    {
        Cube newCube = Instantiate(_cubePrefab, cube.transform.position, Quaternion.identity);
        newCube.Initialize(cube.transform.localScale/ _decreaseScale, cube.ChanceSeparate/ _decreaseChance);
        newCube.Clicked += OnClicked;

        _cubes.Add(newCube);

        return newCube;
    }

    private int RandomizeCount() => UnityEngine.Random.Range(_minCount, _maxCount);
}