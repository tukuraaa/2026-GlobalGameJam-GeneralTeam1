  using R3;
using System;
using UnityEngine;

public class PlayHitObj : MonoBehaviour
{
    public GameObject hit = null;
    
    void  OnTriggerEnter(Collider other)
    {        
        //Spawn hit effect on collision
        if (hit != null)
        {
            Vector3 pos = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - pos).normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, normal);
            var hitOffset = 0.1f;
            pos += normal * hitOffset;            
            var hitInstance = Instantiate(hit, pos, rot);
            hitInstance.transform.LookAt(pos + normal);

            //Destroy hit effects depending on particle Duration time
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }       
    }
}
