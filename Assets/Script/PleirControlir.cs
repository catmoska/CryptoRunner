using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PleirControlir : MonoBehaviour
{
    [Header("Moving")]
    public float Speed;
    public float Jamp;

    [Header("Event")]
    public UnityEvent HitKill;
    public UnityEvent HitMoneuPlus;

    private bool isGraund;
    private Rigidbody2D rb;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // проверка колисий с умертвяисям обекте
        if (collision.gameObject.tag == "Kill")
            HitKill.Invoke();
        // проверка на столкновения с монетой
        else if (collision.gameObject.tag == "Moneu")
        {
            Destroy(collision.gameObject);
            HitMoneuPlus.Invoke();
        }
        //всо осталное земля
        else isGraund = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // проверка на столкновения с монетой
        if (collision.gameObject.tag == "Moneu")
        {
            Destroy(collision.gameObject);
            HitMoneuPlus.Invoke();
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

        if (transform.position.y < -10)
            HitKill.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGraund)
        {
            rb.AddForce(new Vector2(0, Jamp), ForceMode2D.Impulse);
            StartCoroutine(antiJamp());
            //transform.Translate(Vector2.up * jamp);
            //isGraund = false;
        }
    }

    public IEnumerator antiJamp()
    {
        yield return new WaitForSeconds(0.7f);
        rb.AddForce(new Vector2(0, -Jamp/3), ForceMode2D.Impulse);
    }

}
