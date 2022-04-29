using UnityEngine;

public class platformVilazet : MonoBehaviour
{
    private Transform Pleir;
    private Vector3 nanravlenia;
    [SerializeField] private Vector3 smesenia = new Vector3(0, -5,0);
    [SerializeField] private float triger = 15;
    [SerializeField] private float speed = 5;
    public bool raznia;

    void Start()
    {
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }
        nanravlenia = transform.position;
        if(raznia)
            transform.position += smesenia * (1 +(Random.Range(0,2)* -2));
        else 
            transform.position += smesenia;
    }

    void FixedUpdate()
    {
        if(transform.position.x - Pleir.position.x < triger)
            transform.position = Vector2.MoveTowards(transform.position, nanravlenia, speed * Time.fixedDeltaTime);
    }
}
