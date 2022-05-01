using System.Collections;
using UnityEngine;

//двизить монетку по вертикале и ишезает
public class MoneuDellControlir : MonoBehaviour
{
    public bool nrig = true;
    private void Start()
    {
        if (nrig && Random.Range(0, 3) == 0)
        {
            Animator an = GetComponent<Animator>();
            int i = Random.Range(1, 4);
            an.SetInteger("nerexod", i);
        }
    }

    public void start()
    {
        StartCoroutine(barerc());
    }

    public IEnumerator barerc()
    {
        Animator g = GetComponent<Animator>();
        g.enabled = false;
        BoxCollider2D f = GetComponent<BoxCollider2D>();
        f.enabled = false;

        Destroy(gameObject, 0.2f);
        Vector3 dell = new Vector3(-20, 15);
        float rastoi = 10;
        while (true)
        {
            transform.position += dell / rastoi;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private int Randik(int Max)
    {
        for (int i = 0; i < Max; i++)
            if (Random.Range(0, 2) == 0)
                return i;
        return Max - 1;
    }
}
