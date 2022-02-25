using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public float chaseLength = 5;
    private bool lookAtPlayer;

    private PlayerMovement player;
    private BoxCollider2D boxCollider2D;
    private RaycastHit2D hit;
    private Vector2 moveDelta;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        MoveEnemy();

        var position = player.transform.position;
        lookAtPlayer = Vector3.Distance(position, transform.position) < chaseLength && player.isActiveAndEnabled;
    }

    private void MoveEnemy()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 monsterPos = transform.position;
        float x, y;
        if (lookAtPlayer)
        {
            x = playerPos.x > monsterPos.x ? 1 : -1;
            y = playerPos.y > monsterPos.y ? 1 : -1;
        }
        else
        {
            x = 0;
            y = 0;
        }

        moveDelta = new Vector2(x, y);

        //animations


        //Y movement
        hit = Physics2D.BoxCast(monsterPos, boxCollider2D.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider != null)
        {
            return;
        }

        //X movement
        hit = Physics2D.BoxCast(monsterPos, boxCollider2D.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate((moveDelta * Time.deltaTime) / 2);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.name == "Player"){
            player.dealDamage();
        }
    }

    
}