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

    private Animator animator;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int FacingRight = Animator.StringToHash("FacingRight");
    private static readonly int FacingUp = Animator.StringToHash("FacingUp");
    private static readonly int FacingDown = Animator.StringToHash("FacingDown");
    private static readonly int FacingLeft = Animator.StringToHash("FacingLeft");


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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

        moveDelta = new Vector3(x, y,0);

        //animations
        MonsterAnimation();
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

    private void MonsterAnimation()
    {
        animator.SetFloat(Vertical, moveDelta.y);
        animator.SetFloat(Horizontal, moveDelta.x);
        animator.SetFloat(Speed, moveDelta.sqrMagnitude);

        MonsterRotation();
    }

    private void MonsterRotation()
    {
        if (moveDelta.x > 0)
        {
            animator.SetBool(FacingRight, true);
            animator.SetBool(FacingUp, false);
            animator.SetBool(FacingDown, false);
            animator.SetBool(FacingLeft, false);
        }
        else if (moveDelta.x < 0)
        {
            animator.SetBool(FacingLeft, true);
            animator.SetBool(FacingRight, false);
            animator.SetBool(FacingUp, false);
            animator.SetBool(FacingDown, false);
        }
        else if (moveDelta.y < 0)
        {
            animator.SetBool(FacingDown, true);
            animator.SetBool(FacingLeft, false);
            animator.SetBool(FacingRight, false);
            animator.SetBool(FacingUp, false);
        }
        else if (moveDelta.y > 0)
        {
            animator.SetBool(FacingUp, true);
            animator.SetBool(FacingLeft, false);
            animator.SetBool(FacingRight, false);
            animator.SetBool(FacingDown, false);
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