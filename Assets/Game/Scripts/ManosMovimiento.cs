
using UnityEngine;

public class ManosMovimiento : MonoBehaviour
{
    [SerializeField] private float amplitude = 20f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float phaseOffset = 0f;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.localPosition;
    }
    private void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency + phaseOffset) * amplitude;

        transform.localPosition = startPosition + Vector3.up * offset;
    }
}
