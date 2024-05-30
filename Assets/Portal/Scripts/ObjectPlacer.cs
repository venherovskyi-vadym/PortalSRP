using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _objectToPlace;
    [SerializeField] private Transform _objectParent;
    [SerializeField] private Camera _camera;
    [SerializeField] private GraphicRaycaster _graphicRaycaster;
    [SerializeField] private string _arLayer;
    [SerializeField] private Vector3 _direction;
    private GameObject _placedObject;
    private RotatableObject[] _rotatableObjects;

    public void OnPointerDown(PointerEventData eventData)
    {
        var ray = _camera.ScreenPointToRay(eventData.position);
        Debug.DrawRay(ray.origin, ray.direction,Color.red, 10);
        var raycastHits = Physics.RaycastAll(ray.origin,ray.direction,100);

        for (int i = 0; i < raycastHits.Length; i++)
        {
            var rayCastResult = raycastHits[i];
            if (rayCastResult.collider != null && rayCastResult.collider.gameObject.layer == LayerMask.NameToLayer(_arLayer))
            {
                if (_placedObject == null)
                {
                    _placedObject = Instantiate(_objectToPlace, _objectParent);
                    _rotatableObjects = _placedObject.GetComponentsInChildren<RotatableObject>(false);
                }
                _placedObject.transform.position = rayCastResult.point;
                var lookAtPos = rayCastResult.point - _camera.transform.position;
                lookAtPos.y = 0;

                for (int j = 0; j < _rotatableObjects.Length; j++)
                {
                    _rotatableObjects[j].ApplyRotation(Quaternion.LookRotation(lookAtPos) * Quaternion.Euler(_direction));
                }

                break;
            }
        }

        Debug.Log($"pressPosition:{eventData.pressPosition} position:{eventData.position}");
    }
}
