using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePLController : MonoBehaviour
{
    [SerializeField] StatsHolder globalStats;
    

    public float dirX, dirY;
    private float lastX, LastY;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;

    private Vector2 move;
    private Vector2 lastMove;
    [Header("Where he is Watching Now")]
    [SerializeField] Transform fase;
    private float lastAngle;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Animate();

    }
    private void FixedUpdate()
    {
       Movements();
        if (dirX != 0 || dirY != 0)
        {
            Turner(dirX, dirY);
        }
        else
        {
            fase.rotation = Quaternion.Euler(0, 0, lastAngle);
        }
    }
    private void Inputs()
    {
        dirX = joystick.Horizontal * globalStats.Speed;
        dirY = joystick.Vertical * globalStats.Speed;
        move = new Vector2(dirX, dirY);

        if ((dirX == 0 && dirY == 0) && move.x != 0 || move.y != 0)
        {
            lastMove = move;
        }

        

    }
    private void Movements()
    {
        body.velocity = move;
    }

    private void Turner(float dirX, float dirY)
    {
        // Вычисляем угол в радианах с помощью arctan2
        float angle = Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg;

        lastAngle = angle;
        // Устанавливаем угол вращения объекта
        fase.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Animate()
    {
        animator.SetFloat("MoveX", move.x); 
        animator.SetFloat("MoveY", move.y); 
        animator.SetFloat("MoveMagnitude", move.magnitude);
        animator.SetFloat("LastX", lastMove.x);
        animator.SetFloat("LastY", lastMove.y);
    }

}
