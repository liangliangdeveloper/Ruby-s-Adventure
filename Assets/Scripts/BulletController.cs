using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveBullet(Vector2 Direction, float force)
    {
        rbody.AddForce(Direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnermyController ec = collision.gameObject.GetComponent<EnermyController>();
        if (ec != null)
        {
            ec.Fixed();
        }
        Destroy(this.gameObject);
    }
}
