using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float autoDestroy;
    GameObject gunner;


    private void Start()
    {
        Invoke("Explode", autoDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if (go.tag == "Player" && go != gunner)
        {
            DealDamage(go);
        }

        Explode();
    }
    
    public void SetGunner(GameObject go)
    {
        gunner = go;
    }
    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(this);
    }
    void DealDamage(GameObject go)
    {

    }
}
