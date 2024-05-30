using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public void ApplyRotation(Quaternion quaternion)
    {
        _transform.rotation = quaternion;
    }
}