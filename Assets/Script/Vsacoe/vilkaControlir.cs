using UnityEngine;

public class vilkaControlir : MonoBehaviour
{
    private Transform Pleir;
    public float triger;
    public float speed;

    private void Start()
    {
        if (Pleir == null)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");
            Pleir = gos[0].GetComponent<Transform>();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x - Pleir.position.x <= triger)
        {
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
            if (transform.position.x - Pleir.position.x <= -triger)
                Destroy(gameObject);
        }
    }
}
