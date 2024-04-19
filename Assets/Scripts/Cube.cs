using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _startExplosionChancePercent;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _nextExplosionChanceRate;
    [SerializeField] private float _nextScaleRate;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] float _explosionForce;
    [SerializeField] float _explosionRadius;
    
    private float _currentExplosionChancePercentage;
    
    public Action<Cube> Exploded;
    public Action<Cube> Destroyed;
    
    public void Init()
    {
        _currentExplosionChancePercentage = _startExplosionChancePercent;
        transform.position = _startPosition;
        _meshRenderer.material.color = Random.ColorHSV();
    }
    
    public void Init(Cube parentCube)
    {
        Transform parentTransform = parentCube.transform;
        
        transform.position = parentTransform.position;
        _currentExplosionChancePercentage = parentCube._currentExplosionChancePercentage * _nextExplosionChanceRate;
        transform.localScale = parentTransform.localScale * _nextScaleRate;
        _meshRenderer.material.color = Random.ColorHSV();

        Explode(parentCube.transform.position);
    }
    
    public void OnClicked()
    {
        int randomNumber = Utils.GenerateRandomNumberInPercentage();

        if (randomNumber <= _currentExplosionChancePercentage)
        {
            Exploded?.Invoke(this);
        }
        else
        {
            CreateExplosion();
            Destroyed?.Invoke(this);
        }
    }
    
    private void Explode(Vector3 position)
    {
        AddForce(position, _explosionForce, _explosionRadius);
    }
    
    private void CreateExplosion()
    {
        float radius = _explosionRadius / _nextScaleRate;
        float force = _explosionForce / _nextScaleRate;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Cube cube))
            {
                cube.AddForce(transform.position, force, radius);
            }
        }
    }
    
    private void AddForce(Vector3 position, float force, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}
