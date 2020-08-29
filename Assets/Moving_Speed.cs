using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Speed : MonoBehaviour

{
    private Transform Position;  
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    public float MoveSpeed = 3.5f;
    public float jumpForce = 750.0f;
    private Vector2 moveDir;
    public bool isGrounded=false;
    public GameObject groundedObject;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.Find("Sprite").transform;
        m_Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    IEnumerator Jump_movement()
    {
        float j=jumpForce;  /*加速度的值*/
        float g=-10;   /*向下的力*/
        Position.localPosition += Vector3.up*0.0001f;
        isGrounded = false;
        while (Position.localPosition.y>0)
        {
            j=j+g*Time.deltaTime;
            this.Position.localPosition += Vector3.up*j*Time.deltaTime;
            yield return null;
        }
        isGrounded = true;
        Vector3 Old_Position = Position.localPosition;
        Old_Position.y=0;
        Position.localPosition = Old_Position;
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("IsGrounded", isGrounded);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = MoveSpeed;
            m_Animator.SetFloat("MoveSpeed",1);
            m_SpriteRenderer.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir.x = 0;
            m_Animator.SetFloat("MoveSpeed",0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir.y = MoveSpeed;
            m_Animator.SetFloat("MoveSpeed",1);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            moveDir.y = 0;
            m_Animator.SetFloat("MoveSpeed",0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir.y = -MoveSpeed;
            m_Animator.SetFloat("MoveSpeed",1);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            moveDir.y = 0;
            m_Animator.SetFloat("MoveSpeed",0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x=-MoveSpeed;
            m_Animator.SetFloat("MoveSpeed",1);
            m_SpriteRenderer.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
            m_Animator.SetFloat("MoveSpeed",0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Animator.SetTrigger("Jump");
            StartCoroutine("Jump_movement");
        }

        transform.position += (Vector3)moveDir*Time.deltaTime;
    }
    /*void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                if (element.normal.y >0.25f)
                {
                    groundedObject = other.gameObject;
                    isGrounded = true;
                    break;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject == groundedObject)
        {
            groundedObject = null;
            isGrounded = false;
        }   
    }*/
}
