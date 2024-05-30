using Unity.XR.CoreUtils;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToSwitchLayer;
    [SerializeField] private string _layer;
    [SerializeField] private Vector3 _switchDirectionLocal;
    private bool _switched;
    private bool _triggered;
    private int[] _originalLayers;
    private Vector3 _contactDirection;

    private Vector3 SwitchDirection => transform.rotation * _switchDirectionLocal;

    private void Awake()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, SwitchDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            return;
        }

        _triggered = true;

        if (_switched)
        {
            return;
        }

        _contactDirection = other.transform.position - transform.position;
        TryInitOriginalLayers();
        ProcessCollision(_contactDirection);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!_triggered)
        {
            return;
        }

        _triggered = false;

        if (!_switched)
        {
            return;
        }

        _contactDirection = other.transform.position - transform.position;
        TryInitOriginalLayers();
        ProcessCollision(_contactDirection);
    }

    private void TryInitOriginalLayers() 
    {
        if (_originalLayers == null)
        {
            _originalLayers = new int[_objectsToSwitchLayer.Length];

            for (int i = 0; i < _objectsToSwitchLayer.Length; i++)
            {
                _originalLayers[i] = _objectsToSwitchLayer[i].layer;
            }
        }
    }

    private void ProcessCollision(Vector3 direction)
    {
        if (Vector3.Dot(direction, SwitchDirection) > 0)
        {
            SwitchLayers();
        }
    }

    private void SwitchLayers()
    {
        if (_switched)
        {
            for (int i = 0; i < _objectsToSwitchLayer.Length; i++)
            {
                _objectsToSwitchLayer[i].SetLayerRecursively(_originalLayers[i]);
            }
        }
        else
        {
            for (int i = 0; i < _objectsToSwitchLayer.Length; i++)
            {
                _objectsToSwitchLayer[i].SetLayerRecursively(LayerMask.NameToLayer(_layer));
            }
        }

        _switched = !_switched;
    }
}