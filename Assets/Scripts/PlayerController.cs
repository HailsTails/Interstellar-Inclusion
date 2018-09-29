using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Unity Refs")]
    public Rigidbody2D rb;
    public LayerMask interactableObjects;
    public int energyLevel;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    [Header("Movement")]
    public float moveSpeed;
    public float speedBoost;
    private float baseSpeed;
    private Vector2 move;
    private Vector2 faceDirection;

    public SpriteRenderer sprite;

    private bool interacting;

    public int getEnergyLevel()
    {
        return energyLevel;
    }

    public RuntimeAnimatorController XAnimator;
    public RuntimeAnimatorController YAnimator;
    private Animator animator;

    [Header("Testing")]
    public float interactDistance;

    // Use this for initialization
    void Start()
    {
        faceDirection = Vector2.down;
        interacting = false;
        energyLevel = 100;
        interactDistance = 0.5f;
        animator = this.GetComponent<Animator>();
        if (GameManager.instance.playerType == AlienType.Y)
        {
            animator.runtimeAnimatorController = YAnimator;
        }
        else
        {
            animator.runtimeAnimatorController = XAnimator;
        }
        animator.speed = 0.2f;
    }

    private void Awake()
    {
        baseSpeed = moveSpeed;
    }

    void Update()
    {
        //Get player interaction key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            interacting = true;
        }

        //Disable speedBoost modifier
        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = baseSpeed;

        //Enable speedBoost modifier
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= speedBoost;

        updateBearing();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //Interact with object in front of player
        if (interacting)
        {
            interact();
        }
        
        //Update the players facing direction, for animation purposes
        updateBearing();

        //Apply player movement
        moveCharacter();

    }

    /*
     * Update the players facing direction, favour up/down
     */
    void updateBearing()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            animator.SetBool("IsWalking", true);
            faceDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            animator.SetBool("IsWalking", true);
            faceDirection = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (faceDirection.x < 0)
        {
            sprite.flipX = true;   
        } else
        {
            sprite.flipX = false;
        }

        animator.SetFloat("LastMoveX", faceDirection.x);
        animator.SetFloat("LastMoveY", faceDirection.y);
    }

    /*Get the input axis from the controller
     * and apply it to a Vector 2,
     * normalise to handle diagonal speedup
     * and set the players velocity.
     */
    void moveCharacter()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        move = move.normalized * Time.deltaTime * moveSpeed;

        if (move.x == 0 && move.y == 0)
        {
            animator.SetBool("IsWalking", false);
        }

        rb.velocity = move;
    }

    /*
     * Invoke the interact() function from an interactable object in front of the player
     * E.G. PowerCore - pickup(), Bed - sleep(), WaterReclaimer - refillWaterTank()
     */
    void interact()
    {
        interacting = false;
        Interactable interactableObject = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, faceDirection, interactDistance, interactableObjects);
        foreach (RaycastHit2D hit in hits)
        {
            if ((interactableObject = hit.collider.GetComponentInChildren<Interactable>()) != null)
            {
                interactableObject.interact();
            }
        }
    }

    public void useEnergy(int v)
    {
        energyLevel -= v;
        if (energyLevel <= 0)
        {
            GameManager.instance.EndGame();
        }
    }
}
