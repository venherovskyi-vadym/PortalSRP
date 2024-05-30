using UnityEngine;
using UnityEngine.UI;

public class CameraCullingSwitcher : MonoBehaviour
{
    [SerializeField] private LayerMask _initialCullingMask;
    [SerializeField] private LayerMask _alternativeCullingMask;
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _switchButton;

    private bool _switched;

    private void Start()
    {
        _switchButton.onClick.AddListener(SwitchCulling);
        SwitchCulling();
    }

    private void OnDestroy()
    {
        _switchButton.onClick.RemoveListener(SwitchCulling);
    }

    public void SwitchCulling()
    {
        if (_switched)
        {
            _camera.cullingMask = _alternativeCullingMask;
        }
        else 
        {
            _camera.cullingMask = _initialCullingMask;
        }

        _switched = !_switched;
    }
}