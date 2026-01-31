  using R3;
using System;
using UnityEngine;

public class PlayHitObj : MonoBehaviour
{
    public float Lifetime = 3f;
    public GameObject hit = null;

    public event Action OnHitPlayer;

    void Awake()
    {
        if (Lifetime > 0)
            Destroy(this.gameObject, Lifetime);
    }
    
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

            if (other.CompareTag("Player"))
            {
                OnHitPlayer?.Invoke();
            }

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
