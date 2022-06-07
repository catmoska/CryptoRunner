using System.Collections;
using UnityEngine;

//двизить монетку по вертикале и ишезает (всо очееен запутана)
public class MoneuDellControlir : MonoBehaviour
{
    public bool nrig = true;
    public Transform mobeuZob;
    private float speed = 200;
    private static string neimtegNOisk = "Player";
    //private static string neimtegNOisk = "MoneuUi";


    private void Start()
    {
        if (nrig && Random.Range(0, 3) == 0)
        {
            Animator an = GetComponent<Animator>();
            int i = Random.Range(1, 4);
            an.SetInteger("nerexod", i);
        }

        if (mobeuZob == null)
            mobeuZob = GameObject.FindGameObjectsWithTag(neimtegNOisk)[0].GetComponent<Transform>();
        
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

        if (mobeuZob == null)
            mobeuZob = GameObject.FindGameObjectsWithTag(neimtegNOisk)[0].GetComponent<Transform>();
        

        Destroy(gameObject, 0.2f);
        //Vector3 dell = new Vector3(-20, 15);
        //float rastoi = 10;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, mobeuZob.position, speed * Time.fixedDeltaTime); ;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
