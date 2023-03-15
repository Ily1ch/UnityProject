using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class heromove : MonoBehaviour // - ������ �PlayerMove� ������ ���� ��� ����� �������
{
    //------- �������/�����, ����������� ��� ������� ���� ---------
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //-v- ��� ��������������� ������������ � ����������, ������� ���������� ������� �GroundCheck�
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- �������/�����, ����������� ������ ���� � ���� ---------
    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
        Dash();
        Somersault();
        Atack();
    }
    //------- �������/����� ��� ����������� ��������� �� ����������� ---------
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("MoveX", Mathf.Abs(moveVector.x));

    }
    //------- �������/����� ��� ��������� ��������� �� ����������� ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- �������/����� ��� ������ ---------
    public float jumpForce = 10f;
    private int jumpCount = 0;
    public int maxJumpValue = 2;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || (++jumpCount < maxJumpValue)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (onGround) { jumpCount = 0; }

    }

    //------- �������/����� ��� ����������� ����� ---------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    //-----------------------------------------------------------------

    public int dashForce = 1000;
    public float dashCooldown = 1f; // ����� ����������� � ��������
    private float lastDashTime = -Mathf.Infinity; // ����� ���������� ������������� �����������

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            anim.StopPlayback();
            anim.Play("dash");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * dashForce);
            }
            else
            {
                rb.AddForce(Vector2.right * dashForce);
            }

            lastDashTime = Time.time; // ������������� ����� ���������� ������������� �����������
        }
    }
    public int SomersaultForce = 1000;
    public float SomersaultCooldown = 1f; // ����� ����������� � ��������
    private float lastSomersaultTime = -Mathf.Infinity; // ����� ���������� ������������� �����������

    void Somersault()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > lastSomersaultTime + SomersaultCooldown)
        {
            anim.StopPlayback();
            anim.Play("Somersault");

            rb.velocity = Vector2.zero;

            if (!faceRight)
            {
                rb.AddForce(Vector2.left * SomersaultForce);
            }
            else
            {
                rb.AddForce(Vector2.right * SomersaultForce);
            }

            lastSomersaultTime = Time.time; // ������������� ����� ���������� ������������� �����������
        }
    }
    void Atack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.StopPlayback();
            anim.Play("atack1");
        }
    }
}
