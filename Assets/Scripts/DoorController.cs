using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public Transform player;

    private void Update()
    {
        DoorLayerPosition();
    }

    private void DoorLayerPosition()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteRenderer player_sr = player.GetComponent<SpriteRenderer>();
        if (player.position.y > transform.position.y)
        {
            sr.sortingOrder = player_sr.sortingOrder + 1;
        }
        else
        {
            sr.sortingOrder = player_sr.sortingOrder - 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("player entra");
            Animator anim = GetComponent<Animator>();
            anim.SetBool("open", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("player sale");
            Animator anim = GetComponent<Animator>();
            anim.SetBool("open", false);
        }
    }


}
