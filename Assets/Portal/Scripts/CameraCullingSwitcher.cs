using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CameraCullingSwitcher : MonoBehaviour
{
    [SerializeField] private LayerMask _initialCullingMask;
    [SerializeField] private LayerMask _alternativeCullingMask;
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _switchButton;
    [SerializeField] private UniversalRendererData _rendererData;

    private bool _switched;

    private void Start()
    {
        _switchButton.onClick.AddListener(SwitchCulling);
        var backgroundFeature = _rendererData.rendererFeatures.Find(f => f.GetType() == typeof(ARBackgroundRendererFeature));
        backgroundFeature.SetActive(true);
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