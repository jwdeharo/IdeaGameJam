using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : Bullet {

    public static readonly float TIME = 5f;

    protected override void Affect(Collider col)
    {
        //col.gameObject.transform.parent.GetComponent<EnemyController>().Slow(TIME);
    }
}
