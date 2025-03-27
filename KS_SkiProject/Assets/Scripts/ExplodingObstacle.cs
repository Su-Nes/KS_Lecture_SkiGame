using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingObstacle : Obstacle
{
    [SerializeField] private float explodeForce = 15f;
    
    protected override void PlayerCollision(GameObject otherObj)
    {
        player = otherObj.GetComponent<PlayerControl>();
        
        Explode();
    }

    private void Explode()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(explodeForce, player.transform.position, explodeForce);
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
