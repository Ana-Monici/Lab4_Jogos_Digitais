using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator anim;       // Armazena o controlador da animação
    bool isWalkingFlag;         // Armazena o estado do parâmetro "isWalking"
    bool isRunningFlag;         // Armazena o estado do parâmetro "isRunning"
    bool isAttackingFlag;         // Armazena o estado do parâmetro "isAttacking"

    PlayerControls input;       // Armazena o script de entradas (inputs)

    Vector2 Movimento = new Vector2();  // Armazena os controles de direção
    bool movimentoPressionado;          // Armazena o estado de mover
    bool runPressionado;                // Armazena o estado de correr

    private void Awake()
    {
        input = new PlayerControls();

        input.Player.Move.performed += ctx =>
        {
            Movimento = ctx.ReadValue<Vector2>();
            movimentoPressionado = Movimento.x != 0 || Movimento.y != 0;
        };
        input.Player.Run.performed += ctx => runPressionado = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetBool("isWalking", isWalkingFlag);
        anim.SetBool("isRunning", isRunningFlag);
        anim.SetBool("isAttacking", isAttackingFlag);
    }

    void Mover()
    {
        if (movimentoPressionado && !isWalkingFlag)
        {
            isWalkingFlag = true;
            isAttackingFlag = false;
            anim.SetBool("isWalking", isWalkingFlag);
            anim.SetBool("isAttacking", isAttackingFlag);
        }
        if (!movimentoPressionado && isWalkingFlag)
        {
            isWalkingFlag = false;
            anim.SetBool("isWalking", isWalkingFlag);
        }
        if ((movimentoPressionado && runPressionado) && !isRunningFlag)
        {
            isRunningFlag = true;
            isAttackingFlag = false;
            anim.SetBool("isRunning", isRunningFlag);
            anim.SetBool("isAttacking", isAttackingFlag);
            runPressionado = true;
        }
        if ((!movimentoPressionado || !isRunningFlag) && isRunningFlag)
        {
            isRunningFlag = false;
            anim.SetBool("isRunning", isRunningFlag);
            runPressionado = false;
        }
    }

    void Rotacionar()
    {
        Vector3 atualPosition = transform.position;         // Armazena a posição atual
        Vector3 novaPosition = new Vector3(Movimento.x, 0f, Movimento.y);   // Mudança de posição
        Vector3 positionToLookAt = atualPosition + novaPosition;            // Atualização da posição
        transform.LookAt(positionToLookAt);
    }

    void Attack()
    {
        if (!isAttackingFlag)
        {
            isAttackingFlag = true;
            anim.SetBool("isAttacking", isAttackingFlag);
        }
        else
        {
            isAttackingFlag = false;
            anim.SetBool("isAttacking", isAttackingFlag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        input.Player.Attack.performed += ctx => Attack();
        Mover();
        Rotacionar();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
