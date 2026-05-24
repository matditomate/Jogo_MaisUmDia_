using System.Collections;
using UnityEngine;

public class CameraMinigame : MonoBehaviour
{
    public static CameraMinigame instance;

    private float speed = 5f;
    private Vector3 destino;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        destino = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            speed * Time.deltaTime
        );
    }

    public void MoverCamera(float x, float y)
    {
        destino = new Vector3(x, y, transform.position.z);
    }
}