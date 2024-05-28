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
    private GameObject _placedObject;

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
                }
                _placedObject.transform.position = rayCastResult.point;
                break;
            }
        }

        Debug.Log($"pressPosition:{eventData.pressPosition} position:{eventData.position}");
    }
}
