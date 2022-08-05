using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perseguir : MonoBehaviour
{
    public Transform player;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direcao = player.position - this.transform.position;
        float angulo = Vector3.Angle(player.position, this.transform.position);
        if (Vector3.Distance(player.position, this.transform.position) < 10 && angulo < 30)
        {
            direcao.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direcao), 0.1f);
            anim.SetBool("isIdle", false);
            if (direcao.magnitude > 5)
            {
                anim.SetBool("isWalking", true);
                this.transform.Translate(0, 0, 0.02f);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
}
