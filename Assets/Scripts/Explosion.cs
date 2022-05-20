using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask waterLayerMask;
    [SerializeField] private Rigidbody2D rb2D;

    [SerializeField] private float coolDown = 2f;
    [SerializeField] private ParticleSystem fireParticleSystem;
    private bool psIsOn = false;

    private bool reactionStart = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || coolDown <= 0f)
        {
            Explode();
        }

        if (coolDown <= 1.3f && !psIsOn)
        {
            if(fireParticleSystem != null) 
                fireParticleSystem.Play();
            psIsOn = true;
        }


        if (coolDown > 0f && reactionStart)
        {
            coolDown -= Time.deltaTime;
            rb2D.AddForce(new Vector2(700,0)*Random.Range(-1,2));
        }
            
        
    }


    public void Explode()
    {
        var collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius,  layerMask);
        foreach (var collider2D in collider2Ds)
        {
            
            Vector2 direction = collider2D.transform.position - transform.position;
            
            Rigidbody2D rb = collider2D.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * force);
                //Debug.Log(collider2D + " ; " + direction + " ; " + force * direction);
            }
        }
        Destroy(transform.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((1 << other.gameObject.layer) & waterLayerMask) != 0)
            reactionStart = true;
    }
}
