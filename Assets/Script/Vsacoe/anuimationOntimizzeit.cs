using UnityEngine;

//(всо очен запутана) анимируимий обекти отклучаиюся послу вихода из зони достизения
public class anuimationOntimizzeit : RunMonoBehaviour
{
    private Transform Pleir;
    private float triger = 26;
    public bool raznia;
    private bool active = true;
    private Animator an;


    private void Start()
    {
        updeitMeneger.singleton.RMB.Add(this);
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if(an == null)
            an = GetComponent<Animator>();

        if (active && !(transform.position.x - Pleir.position.x < triger))
        {
            an.enabled = false;
            active = false;
        }
    }


    public override bool Run()
    {
        if (Pleir == null)
            Pleir = PleirControlir.singletonGameObject.transform;
        if (an == null)
        {
            an = GetComponent<Animator>();
            if (an == null)
            {
                Destroy(this);
                gameObject.GetComponent<anuimationOntimizzeit>().enabled = false;
                Debug.Log("ddddd");
                return true;
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
            return true;
        }
        else
        {
            if (active)
            {
                an.enabled = false;
                active = false;
            }
        }

        return false;
    }
}
