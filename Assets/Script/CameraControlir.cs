using UnityEngine;

public class CameraControlir : MonoBehaviour
{
    public Transform Pleir;
    public Vector3 smesenia;
    public float camVis = 8.95f;


    //если не указан плеир то ишет на сене
    private void Start()
    {
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }
    }

    //двигает камеру
    private void FixedUpdate()
    {
        Vector3 t = Pleir.position + smesenia;
        if (t.y < camVis*2) t.y = camVis;
        else if (t.y > camVis*2 && t.y <30) t.y /=2f;

        transform.position = t;
    }
}
