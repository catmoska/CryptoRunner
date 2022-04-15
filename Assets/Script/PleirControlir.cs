using UnityEngine;
using UnityEngine.Events;

public class PleirControlir : MonoBehaviour
{
    public float Speed;
    public float jamp;
    private bool isGraund;
    private Rigidbody2D rb;

    public UnityEvent HitKill;
    public UnityEvent HitMoneuPlus;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Kill")
            HitKill.Invoke();
        else if (collision.gameObject.tag == "Moneu")
        {
            Destroy(collision.gameObject);
            HitMoneuPlus.Invoke();
        }
        else
        {
            isGraund = true;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGraund = true;
    }

    void FixedUpdate() 
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        if(transform.position.y <-10)
            HitKill.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& isGraund)
        {
            rb.AddForce(new Vector2(0, jamp), ForceMode2D.Impulse);
            //transform.Translate(Vector2.up * jamp);
            isGraund = false;
        }
    }
}
