using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : Bullet {

    protected override void Affect(Collider col)
    {
        Destroy(col.gameObject.transform.parent.gameObject);
    }
}
