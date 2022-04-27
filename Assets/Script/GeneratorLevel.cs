using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLevel : MonoBehaviour
{
    public float Zaderzka;
    public float Dolg;
    public List<GameObject> PlatformLeft;
    public List<GameObject> Platform;
    public List<GameObject> PlatformDuble;
    public List<GameObject> PlatformRight;
    public List<GameObject> Prepsta;
    public List<int> PrepstaDovz;
    public List<bool> PrepstaZerkal;
    public List<GameObject> PrepstaClon;
    public List<int> PrepstaDovzClon;
    public List<bool> PrepstaZerkalClon;
    public List<GameObject> Kill;
    public List<GameObject> KillMelas;
    public List<GameObject> KillKrupnac;
    public GameObject Moneu;
    public GameObject Temno;
    public Vector3 smesemiaTemno = new Vector3(19f, 0);

    //dell
    public List<GameObject> Dell;

    //nrodolzitolnast
    public int takt;
    public int taktKolibat;
    private int vidi = 4;

    private void Start()
    {
        generit();
    }

    public void start()
    {
        generitRES(Vector3.zero);
    }

    //главний гениратор
    float moneusic;
    int biom;
    public IEnumerator generitKadar(Vector2 kord)
    {
        int i;
        moneusic = 2;
        biom = Random.Range(0, Platform.Count / 2);
        int takti = Random.Range(takt - taktKolibat, takt + taktKolibat);

        yield return new WaitForSeconds(0.1f);
        for (i = 0; i < 9; i++) GENplatform(i, kord, false);
        yield return new WaitForSeconds(Zaderzka);
        for (; i < takti;)
        {
            int u = Randik(vidi + 1);
            switch (u)
            {
                case 0: //генирратор платформ
                    i = GENplatform(i, kord);
                    break;
                case 1: //генирратор двоиних платформ
                    i = GENplatformDuble(i, kord);
                    break;
                case 2:  //генирратор префабов вибраних
                    i = GENprefabc(i, kord);
                    break;
                case 3://генирратор платформ вес€сих в воздухе
                    i = GENnlatformSip(i, kord);
                    break;
                case 4://генирратор леани и 2 €руса
                    i = GENplatformEtaz(i, kord);
                    break;
                default:
                    i = GENplatform(i, kord);
                    vidi = u;
                    Debug.LogError("типа нет ето EROOR :GeneratorLevel -> generitKadar : " + u.ToString());
                    break;
            }
            yield return new WaitForSeconds(Zaderzka);
        }
        for (int g = 0; g < 10; g++) i = GENplatform(i, kord, false);
        Dell.Add(Instantiate(Temno, new Vector2(kord.x + i * Dolg + smesemiaTemno.x, kord.y), Quaternion.identity));
        yield return new WaitForSeconds(Zaderzka);
    }

    //StartUdalenia
    private void del()
    {
        StartCoroutine(delC(Dell[Dell.Count - 1]));
    }

    private void delR()
    {
        for (int i = 0; i < Dell.Count; i++)
        {
            if (Dell[i] != null)
                Destroy(Dell[i]);
        }
        Dell = new List<GameObject>();
    }

    public IEnumerator delC(GameObject OBject)
    {
        List<GameObject> Dell2 = Dell;
        Dell = new List<GameObject>();

        for (int i = 0; i < Dell2.Count - 1; i++)
        {
            if ((i%50)==0)
                yield return new WaitForSeconds(0);
            if (Dell2[i] != null)
                Destroy(Dell2[i]);
        }
        Dell = new List<GameObject>();
        yield return new WaitForSeconds(10);
        Destroy(OBject);
    }

    //StartGenerasia
    public void generit(Vector3 kord)
    {
        zbros();
        if (Dell.Count != 0)
            del();
        StartCoroutine(generitKadar(kord + smesemiaTemno));
    }

    public void generitRES(Vector3 kord)
    {
        zbros();
        if (Dell.Count != 0)
            delR();
        StartCoroutine(generitKadar(kord));
    }

    public void generit()
    {
        zbros();
        StartCoroutine(generitKadar(Vector3.zero));
    }

    //уменшиний рандом
    private int Randik(int Max)
    {
        for (int i = 0; i < Max; i++)
            if (Random.Range(0, 2) == 0)
                    return i;
        return Max-1;
    }

    //делает случаино поворот
    private Quaternion Zerkal()
    {
        return Quaternion.Euler(new Vector3(0, Random.Range(0, 2) * 180, 0));
    }

    //збрасивает все параметри до дефолт
    private void zbros()
    {
        isDvuska_GENplatformEtaz = false;
    }


    //////////////////////////////////////
    //стандарт гениратор
    private int GEN(int i, Vector2 kord)
    {
        return i;
    }

    //гениратор монет
    private void GENmoneu(int i, Vector2 kord, bool uron = true)
    {
        if (Random.Range(0, (int)moneusic) == 0)
        {
            moneusic++;
            moneusic *= 3;
            if (Random.Range(0, 6) != 5 || !uron)
                Dell.Add(Instantiate(Moneu, new Vector2(kord.x + i * Dolg, kord.y + Dolg), Quaternion.identity));
            else
                Dell.Add(Instantiate(Kill[Random.Range(0, Kill.Count)], new Vector2(kord.x + i * Dolg, kord.y + Dolg-0.5f), Zerkal()));
        }
        else
            moneusic /= 2;
    }

    //генирратор платформ
    private int GENplatform(int i, Vector2 kord, bool mon = true,bool uron = true)
    {
        if (mon)
            GENmoneu(i, kord, uron);
        Dell.Add(Instantiate(Platform[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Zerkal()));
        i++;
        return i;
    }

    //генирратор двоиних платформ
    private int GENplatformDuble(int i, Vector2 kord)
    {
        GENmoneu(i, kord);
        Dell.Add(Instantiate(PlatformDuble[Random.Range(0, 1) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y), Quaternion.identity));
        i += 2;
        return i;
    }

    //генирратор префабов
    private int GENprefabc(int i, Vector2 kord)
    {
        for (int g = 0; g < 1; g++)
            i = GENplatform(i, kord, false);

        int ran = Random.Range(0, Prepsta.Count);

        for (int g = 0; g < PrepstaDovz[ran]; g++)
            Dell.Add(Instantiate(Platform[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + (g + i) * Dolg, kord.y), Zerkal()));

        Dell.Add(Instantiate(Prepsta[ran], new Vector2(kord.x + i * Dolg, kord.y + Dolg), PrepstaZerkal[ran]? Zerkal(): Quaternion.identity));
        i += PrepstaDovz[ran];

        for (int g = 0; g < 1; g++)
            i = GENplatform(i, kord, false);

        return i;
    }

    //генирратор префабов вибраних
    private int GENprefabcClon(int i, Vector2 kord,int clonI = -1)
    {
        for (int g = 0; g < 1; g++)
            i = GENplatform(i, kord, false);

        int ran = clonI;
        if (clonI == -1) ran = Random.Range(0, PrepstaClon.Count);

        for (int g = 0; g < PrepstaDovzClon[ran]; g++)
            Dell.Add(Instantiate(Platform[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + (g+i) * Dolg , kord.y), Zerkal()));

        Dell.Add(Instantiate(PrepstaClon[ran], new Vector2(kord.x + i * Dolg, kord.y + Dolg), PrepstaZerkalClon[ran] ? Zerkal() : Quaternion.identity));
        i += PrepstaDovzClon[ran];

        for (int g = 0; g < 1; g++)
            i = GENplatform(i, kord, false);

        return i;
    }

    //генирратор платформ вес€сих в воздухе
    private Vector2 smesenia_GENnlatformSip = new Vector2(0, 3.5f);
    private Vector2Int ranNar_GENnlatformSip = new Vector2Int(4, 15);
    private int GENnlatformSip(int i, Vector2 kord)
    {
        int ran = Random.Range(ranNar_GENnlatformSip.x, ranNar_GENnlatformSip.y);
        int ran2 = Random.Range(0, Kill.Count);
        for (int g = 0; g < ran; g++)
            Dell.Add(Instantiate(Kill[ran2], new Vector2(kord.x + (i+g) * Dolg, kord.y), Zerkal()));

        i++;
        Dell.Add(Instantiate(PlatformLeft[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y+smesenia_GENnlatformSip.y), Quaternion.identity));i++;
        for (int g = 0; g < ran - 4; g++)
            i = GENplatform(i, kord+ smesenia_GENnlatformSip);
        Dell.Add(Instantiate(PlatformRight[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y+smesenia_GENnlatformSip.y), Quaternion.identity));i++;
        i++;
        for (int g = 0; g < ran / 2; g++)
            i = GENplatform(i, kord);
        return i;
    }
    
    //генирратор леани и 2 €руса
    private bool isDvuska_GENplatformEtaz = false;
    private Vector2 smesenia_GENplatformEtaz = new Vector2(0, 24);
    private Vector2Int ranNar_GENplatformEtaz = new Vector2Int(10, 25);
    private int GENplatformEtaz(int i, Vector2 kord)
    {
        if(isDvuska_GENplatformEtaz || Random.Range(0,5)!=0) return i;
        isDvuska_GENplatformEtaz = true;

        int rand = Random.Range(ranNar_GENplatformEtaz.x, ranNar_GENplatformEtaz.y);

        for (int g = 0; g < 3; g++) 
            i = GENplatform(i, kord, false);

        i = GENprefabcClon(i, kord,0);

        Dell.Add(Instantiate(PlatformLeft[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y) + smesenia_GENplatformEtaz, Quaternion.identity)); i++;
        for (int g = 0; g < rand-2; g++) 
            i = GENplatform(i, kord+ smesenia_GENplatformEtaz,true,false);
        Dell.Add(Instantiate(PlatformRight[Random.Range(0, 2) + biom * 2], new Vector2(kord.x + i * Dolg, kord.y) + smesenia_GENplatformEtaz, Quaternion.identity)); i++;
        
        int ii = i;
        i -= rand;

        for (int g = 0; g < rand/2; g++) 
            i = GENplatform(i, kord);
        i = GENnlatformSip(i, kord);
        while(ii-i >=-2) 
            i = GENplatform(i, kord);
        for (int g = 0; g < 5; g++) 
            i = GENplatform(i, kord);
        return i;
    }
}
