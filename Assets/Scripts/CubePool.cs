using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    
    private Queue<Cube> _queue = new Queue<Cube>();
    
    private void Awake()
    {
        _queue = new Queue<Cube>();
    }

    public Cube GetCube()
    {
        if (_queue.Count == 0)
        {
            return Instantiate(_prefab);
        }

        Cube cube = _queue.Dequeue();
        cube.gameObject.SetActive(true);
        
        return cube;
    }

    public void PutCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _queue.Enqueue(cube);
    }
}
