using System.Collections;
using UnityEngine;

public class MoneuDellControlir : MonoBehaviour
{
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
