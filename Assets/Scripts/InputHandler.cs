using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] float _maxDistance;
    [SerializeField] LayerMask _layerMask;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _layerMask))
            {
                hit.transform.TryGetComponent(out Cube cube);

                if (cube != null)
                {
                    cube.OnClicked();
                }
            }
        }
    }
}
