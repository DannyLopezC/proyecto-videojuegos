using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private Animator animator;

    public int healthPoints;

    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int FacingRight = Animator.StringToHash("FacingRight");
    private static readonly int FacingUp = Animator.StringToHash("FacingUp");
    private static readonly int FacingDown = Animator.StringToHash("FacingDown");
    private static readonly int FacingLeft = Animator.StringToHash("FacingLeft");

    private void Start()
    {
        healthPoints = 100;
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
        checkGameOver();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        PlayerAnimations();

        //Y movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //X movement
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void PlayerAnimations()
    {
        animator.SetFloat(Vertical, moveDelta.y);
        animator.SetFloat(Horizontal, moveDelta.x);
        animator.SetFloat(Speed, moveDelta.sqrMagnitude);

        PlayerRotation();
    }

    private void PlayerRotation()
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

    public void dealDamage(){

        this.healthPoints=this.healthPoints-40;
    }

    public int getDamage(){
        return this.healthPoints;
    }

    void checkGameOver(){
        if (this.healthPoints <= 0){
            SceneManager.LoadScene("gameOver");
        }
    }
}