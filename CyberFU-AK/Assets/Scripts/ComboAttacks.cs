using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttacks : MonoBehaviour
{
    public Animator anim;
    public int NumberOfButtonsPressed = 0;
    float lastButtonPressed = 0;
    float maxComboDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastButtonPressed > maxComboDelay)
        {
            NumberOfButtonsPressed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            lastButtonPressed = Time.time;
            NumberOfButtonsPressed++;
            if(NumberOfButtonsPressed == 1)
            {
                anim.SetBool("Attack1", true);
            }
            NumberOfButtonsPressed = Mathf.Clamp(NumberOfButtonsPressed, 0, 3);
        }
    }

    public void return1()
    {
        if(NumberOfButtonsPressed >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
        }
    }

    public void return2()
    {
        if (NumberOfButtonsPressed >= 3)
        {
            anim.SetBool("Attack3", true);
        }
        else
        {
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack1", false);
        }
    }

    public void return3()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        NumberOfButtonsPressed = 0;
    }
}
