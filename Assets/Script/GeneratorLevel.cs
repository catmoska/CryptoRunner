using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLevel : MonoBehaviour
{
    public Transform Pleir;
    //public int Sidd;
    public float Zaderzka;
    public float Dolg;
    public List<GameObject> PlatformLeft;
    public List<GameObject> Platform;
    public List<GameObject> PlatformDuble;
    public List<GameObject> PlatformRight;
    public List<GameObject> Prepsta;
    public List<int> PrepstaDovz;
    public List<GameObject> Kill;
    public GameObject Moneu;


    public List<GameObject> Dell;

    public int takt;
    public int taktKolibat;
    private int vidi = 4;

    void Start()
    {
        generit(Vector2.zero);
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }
    }


    private void del()
    {
        for (int i = 0; i < Dell.Count; i++)
            if (Dell[i] != null)
                Destroy(Dell[i]);
        Dell = new List<GameObject>();
    }

    public void generit(Vector2 kord)
    {
        del();
        StartCoroutine(generitKadar(kord));
    }


    private float moneusic;
    private int biom;
    public IEnumerator generitKadar(Vector2 kord)
    {
        biom = Random.Range(0, Platform.Count/2);
        int takti = Random.Range(takt- taktKolibat, takt- taktKolibat);

        for (int i = 0; i < 5;i++) 
            Instantiate(Platform[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity);
        

        moneusic = 2;

        for(int i =5; i < takti;)
        {
            moneusic /= 2;
            switch (Randik(vidi))
            {
                case 0: //платформа
                    i = GENplatform(i, kord);
                    break;
                case 1: //платформа двоиная
                    i = GENplatformDuble(i, kord);
                    break;
                case 2:  //префабс
                    i = GENprefabc(i, kord);
                    break;
                case 3:// платформа над шипами
                    i = GENnlatformSip(i, kord);
                    break;
                default:
                    i++;
                    Dell.Add(Instantiate(Platform[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
                    Debug.LogError("типа нет ето EROOR");
                    break;
            }
            yield return new WaitForSeconds(Zaderzka);
        } 
    }


    private int Randik(int Max)
    {
        for (int i = 0; i < Max; i++)
            if (Random.Range(0, 2) == 0)
                return i;
        return Max-1;
    }

    //////////////////////////////////////
    private int GEN(int i, Vector2 kord)
    {
        return i;
    }

    private void GENmoneu(int i, Vector2 kord)
    {
        if (Random.Range(0, (int)moneusic) == 0)
        {
            moneusic *= 4;
            if (Random.Range(0, 3) != 0)
                Dell.Add(Instantiate(Moneu, new Vector2(kord.x + i * Dolg, kord.y + Dolg), Quaternion.identity));
            else
                Dell.Add(Instantiate(Kill[Random.Range(0, Kill.Count)], new Vector2(kord.x + i * Dolg, kord.y + Dolg), Quaternion.identity));
        }
    }

    private int GENplatform(int i,Vector2 kord,bool mon = true)
    {
        if(mon) 
            GENmoneu(i, kord);
        Dell.Add(Instantiate(Platform[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
        i++;
        return i;

    }

    private int GENplatformDuble(int i, Vector2 kord)
    {
        GENmoneu(i, kord);
        Dell.Add(Instantiate(PlatformDuble[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
        i += 2;
        return i;
    }

    private int GENprefabc(int i, Vector2 kord)
    {
        int ran = Random.Range(0, Prepsta.Count);
        for (int g = 0; g < PrepstaDovz[ran]; g++)
            Dell.Add(Instantiate(Platform[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg + g * Dolg, kord.y), Quaternion.identity));
        Dell.Add(Instantiate(Prepsta[ran], new Vector2(kord.x + i * Dolg, kord.y + Dolg), Quaternion.identity));
        i += PrepstaDovz[ran];
        for (int g = 0; g < 3; g++)
        {
            Dell.Add(Instantiate(Platform[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
            i += 1;
        }
        return i;
    }

    private Vector2 smesenia_GENnlatformSip = new Vector2(0,2.5f);
    private int GENnlatformSip(int i, Vector2 kord)
    {
        int ran = Random.Range(3, 15);
        int ran2 = Random.Range(0, Kill.Count);
        for (int g = 0; g < ran; g++)
        {
            Dell.Add(Instantiate(Kill[ran2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
            i++;
        }
        i -= ran;

        Dell.Add(Instantiate(PlatformLeft[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y + Dolg + 2.5f), Quaternion.identity));i++;
        for (int g = 0; g < ran - 2; g++)
            i = GENplatform(i, kord+ smesenia_GENnlatformSip);
        Dell.Add(Instantiate(PlatformRight[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y + Dolg + 2.5f), Quaternion.identity));i++;

        for (int g = 0; g < ran / 2; g++)
            i = GENplatform(i, kord);
        return i;
    }

}
