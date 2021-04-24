using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Rigidbody2D rb;
    float force = 100;
    private Animator enemyAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y<=-3)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Map")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameController.Instance.score += 20;
            enemyAnim = other.GetComponent<Animator>();
            enemyAnim.SetBool("Hurt", true);
            Destroy(other.gameObject,0.5f);
            Destroy(this.gameObject);
        }
    }
}
