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
            Destroyed?.Invoke(this);
        }
    }
    
    public void Explode()
    {
        _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
