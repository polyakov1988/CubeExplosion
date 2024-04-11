using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private int _minCubes;
    [SerializeField] private int _maxCubes;
    [SerializeField] private CubePool _cubePool;

    private void Awake()
    {
        Cube startCube = _cubePool.GetCube();
        
        startCube.Init();
        startCube.Exploded += CreateCubes;
        startCube.Destroyed += PutCubeIntoPool;
    }

    private void CreateCubes(Cube cube)
    {
        int count = Random.Range(_minCubes, _maxCubes);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = _cubePool.GetCube();
            
            newCube.Exploded += CreateCubes;
            newCube.Destroyed += PutCubeIntoPool;
            
            newCube.Init(cube);
        }

        cube.Explode();
        
        PutCubeIntoPool(cube);
    }

    private void PutCubeIntoPool(Cube cube)
    {
        cube.Exploded -= CreateCubes;
        cube.Destroyed -= PutCubeIntoPool;
        
        _cubePool.PutCube(cube);
    }
}
