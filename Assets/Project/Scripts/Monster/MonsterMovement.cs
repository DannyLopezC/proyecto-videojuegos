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
    private Vector3 moveDelta;
    private Animator animator;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int FacingRight = Animator.StringToHash("FacingRight");
    private static readonly int FacingUp = Animator.StringToHash("FacingUp");
    private static readonly int FacingDown = Animator.StringToHash("FacingDown");
    private static readonly int FacingLeft = Animator.StringToHash("FacingLeft");
    private Vector3 lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float moveAlone = 0.0f;
    private bool moveLeft = true;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;

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
        float x = 0;
        float y = 0;
        if (lookAtPlayer)
        {
            x = playerPos.x > monsterPos.x ? 1 : -1;
            y = playerPos.y > monsterPos.y ? 1 : -1;
        }
        else
        {
            float moveTime = 50.0f;
            if(moveAlone > moveTime) {
                if(moveLeft)
                {
                    x = -1;
                    if (lastPosition == monsterPos)
                    {
                        moveLeft = false;
                        moveDown = false;
                        moveRight = false;
                        moveUp = true;
                        x = 0;
                        lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }

                if (moveUp)
                {
                    y = 1;
                    if (lastPosition == monsterPos)
                    {
                        moveLeft = false;
                        moveDown = false;
                        moveRight = true;
                        moveUp = false;
                        y = 0;
                        lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }

                if (moveRight)
                {
                    x = 1;
                    if (lastPosition == monsterPos)
                    {
                        moveLeft = false;
                        moveDown = true;
                        moveRight = false;
                        moveUp = false;
                        x = 0;
                        lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }

                if (moveDown)
                {
                    y = -1;
                    if (lastPosition == monsterPos)
                    {
                        moveLeft = true;
                        moveDown = false;
                        moveRight = false;
                        moveUp = false;
                        y = 0;
                        lastPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    }
                }

                if(moveAlone > moveTime + 20.0f)
                {
                    moveAlone = 0.0f;
                }

            }
            else
            {
                x = 0;
                y = 0;
            }
            moveAlone += 1.0f;
        }
        moveDelta = new Vector3(x, y,0);

        //animations
        MonsterAnimation();
        //Y movement
        hit = Physics2D.BoxCast(monsterPos, boxCollider2D.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, (moveDelta.y * Time.deltaTime) / 2,0);
        }
        else
        {
            lastPosition = monsterPos;
        }
        //X movement
        hit = Physics2D.BoxCast(monsterPos, boxCollider2D.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate((moveDelta.x * Time.deltaTime) / 2,0,0);
        }
        else { 
            lastPosition = monsterPos; 
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