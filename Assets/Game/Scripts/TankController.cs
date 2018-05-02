using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

    [Header("Gun")]
    [SerializeField] float shotSpeed;
    [SerializeField] Transform shotSpawnSpot;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrel;
    //[SerializeField] Transform gun2;
    [SerializeField] float gunMaxAngle;
    [SerializeField] float gunMinAngle;
    [SerializeField] float fireRate;



    [Header("Fx")]
    [SerializeField] Animator beltAnim;
    [SerializeField] Animator armourAnim;
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem fireParticles;


    [Header("Tank")]
    [SerializeField] float speed;
    Rigidbody2D rigidbody;

    [Header("Ground check")]
    [SerializeField] LayerMask mask;
    [SerializeField] Transform[] RayCastSpots;

    [Header("Flip settings")]
    [SerializeField] float maxRot;
    [SerializeField] float minRot;
    [SerializeField] float flipCheckInterval;
    [SerializeField] float minVelocity;


    float fireThreshold;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("FlipCheck", flipCheckInterval, flipCheckInterval);
        Cursor.lockState = CursorLockMode.Confined;

        dustParticles.emissionRate = 0;

    }

    bool grounded;

    // Update is called once per frame
    void Update() {
        if (grounded)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.AddForce((transform.right * -1) * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigidbody.AddForce(transform.right * speed * Time.deltaTime);
            }
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            beltAnim.SetBool("Idle", true);
            dustParticles.emissionRate = 0;

        }
        else
        { 
            beltAnim.SetBool("Idle", false);
            dustParticles.emissionRate = 25;

        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > fireThreshold)
        {
            armourAnim.SetTrigger("Shoot");
            fireThreshold = Time.time + fireRate;

            GameObject shot = Instantiate(bullet, shotSpawnSpot.position, shotSpawnSpot.rotation);
            var rb = shot.GetComponent<Rigidbody2D>();
            rb.AddForce(shot.transform.right * Time.fixedDeltaTime * 1000 * shotSpeed);
            var pr = shot.GetComponent<Projectile>();
            pr.SetGunner(gameObject);
            fireParticles.Emit(30);
            
        }


        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(barrel.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        //Get the angle between the points
        float angle = 180 + AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        
        float clampedAngle;

        if (180 <= angle && angle <= 360)
            angle = angle - 360;

        if (gunMaxAngle < angle)
            clampedAngle = gunMaxAngle;
        else if (gunMinAngle > angle)
            clampedAngle = gunMinAngle;
        else
            clampedAngle = angle;

        Debug.DrawLine(barrel.position, barrel.right * 10, Color.cyan);
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), barrel.right * Vector3.Distance(barrel.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), Color.green);
        Debug.DrawLine(barrel.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);

        barrel.localRotation = Quaternion.Euler(new Vector3(0f, 0f, clampedAngle));    

        bool b = false;
        foreach (var s in RayCastSpots)
        {
            if(Physics2D.Linecast(s.position, new Vector2(s.position.x, s.position.y - .25f), mask))
            {
                b = true;
            }
        }

        grounded = b;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    float lastRot;
    bool rotStrike;
    void FlipCheck()
    {
        if(lastRot > maxRot)
        {
            if (rotStrike && Mathf.Abs(rigidbody.velocity.magnitude) < minVelocity)
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            rotStrike = true;
        }
        else
        {
            rotStrike = false;
        }
   
        lastRot = Mathf.Abs(transform.rotation.z);
    }
}
