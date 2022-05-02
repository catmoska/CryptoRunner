using UnityEngine;

public class anuimationOntimizzeit : MonoBehaviour
{
    private Transform Pleir;
    private float triger = 26;
    public bool raznia;
    private bool active = true;
    private Animator an;


    void Start()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if(an == null)
            an = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if (an == null)
        {
            an = GetComponent<Animator>();
            if (an == null)
            {
                gameObject.GetComponent<anuimationOntimizzeit>().enabled = false;
                return;
            }
        }

        if (transform.position.x - Pleir.position.x < triger)
        {
            if (!active)
            {
                an.enabled = true;
                active = true;
            }

            if (transform.position.x - Pleir.position.x < -triger)
                Destroy(gameObject);
        }
        else
        {
            if (active)
            {
                an.enabled = false;
                active = false;
            }
        }
    }
}
