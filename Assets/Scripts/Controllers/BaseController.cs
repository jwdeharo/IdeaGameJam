using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public void Shoot(GameObject bullet, Vector3 directionOfShooting)
    {
        GameObject instance = Instantiate(bullet, transform.position, Quaternion.identity);
        instance.GetComponent<Bullet>().direction = directionOfShooting;
        Physics.IgnoreCollision(GetComponentInChildren<SphereCollider>(),
            instance.GetComponent<Collider>());
    }
}
