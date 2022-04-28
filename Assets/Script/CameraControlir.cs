using UnityEngine;

public class CameraControlir : MonoBehaviour
{
    public Transform Pleir;
    public Vector3 smesenia;
    public float[] camVis;


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
        UpdateS();
    }

    public void UpdateS()
    {
        Vector3 t = Pleir.position;
        for (int i = 0; i < camVis.Length; i++)
            if (t.y < camVis[i] * 2)
            {
                t.y = camVis[i];
                break;
            }
        transform.position = t + smesenia;
    }
}
