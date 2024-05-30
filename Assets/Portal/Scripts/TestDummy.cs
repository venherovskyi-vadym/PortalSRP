using UnityEngine;

public class TestDummy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transform;

    private void Update()
    {
        var input = Vector3.zero;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Rise");
        input.z = Input.GetAxis("Vertical");
        input *= _speed * Time.deltaTime;
        _transform.position += input;
    }
}