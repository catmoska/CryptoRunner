using UnityEngine;

public class CameraControlir : MonoBehaviour
{
    public Transform Pleir;
    public Vector3 smesenia;

    private void Start()
    {
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }
    }

    private void FixedUpdate()
    {
        transform.position = Pleir.position + smesenia;
    }
}
