using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.transform.position;
    }


    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
