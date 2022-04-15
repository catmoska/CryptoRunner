using System.Collections.Generic;
using UnityEngine;

public class GeneratorLevel : MonoBehaviour
{
    public Transform Pleir;
    public int Sidd;
    public float Dolg;
    public List<GameObject> PlatformLeft;
    public List<GameObject> Platform;
    public List<GameObject> PlatformRight;
    public List<GameObject> Prepsta;
    public List<GameObject> Moneu;




    void Start()
    {
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }

        Instantiate(PlatformLeft[Random.Range(0,PlatformLeft.Count)], new Vector2(0f, 0f), Quaternion.identity);
        for (int i = 0; i < 50; i++)
        {
            if(Random.Range(0, 10)==0) 
                Instantiate(Moneu[Random.Range(0, Moneu.Count)], new Vector2(Dolg * i + Dolg, Dolg), Quaternion.identity);
            Instantiate(Platform[Random.Range(0, Platform.Count)], new Vector2(Dolg * i + Dolg, 0f), Quaternion.identity);
        }
        Instantiate(PlatformRight[Random.Range(0, PlatformRight.Count)], new Vector2(Dolg*51, 0f), Quaternion.identity);
    }
}
