using System.Collections;
using UnityEngine;

//двизить монетку по вертикале и ишезает
public class MoneuDellControlir : MonoBehaviour
{
    public bool nrig = true;
    private void Start()
    {
        if (nrig && Random.Range(0, 5) == 0)
        {
            Animator an = GetComponent<Animator>();
            an.SetBool("nerexod", true);
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

        Destroy(gameObject,0.2f);
        Vector3 dell = new Vector3(-20,15);
        float rastoi = 10;
        while (true)
        {
            transform.position += dell/ rastoi;
            yield return new WaitForSeconds(0.02f);
        }
    }

}
