using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public GameObject snowBall;
    public float throwDistance;
    public int throwSpeed;
    private bool justThown;
    private Vector3 throwAngle = new(0, 0.33f, 0);
    [SerializeField] private int fireDelay = 12;
    
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    { 
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % fireDelay != 0) // this is neat
            return;
        
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget < throwDistance && justThown==false)
        {
            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        justThown = true;
        GameObject tempSnowBall = Instantiate(snowBall,transform.position,transform.rotation);
        Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
        Vector3 targetDirection =  Vector3.Normalize(target.transform.position-transform.position);
            
        //Add a small throw angle
        targetDirection += throwAngle;
        tempRb.AddForce(targetDirection * throwSpeed);
        Invoke("ThrowOver", 0.1f);
    }
    
    void ThrowOver()
    {
        justThown = false;
    }
}
