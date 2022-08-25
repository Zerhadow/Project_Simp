using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider health;
    public Slider super;

    // Start is called before the first frame update
    void Start()
    {
        //test to make sure its configed correctly
        SetMaxHealth(100);
        SetMaxSuper(100);
        
    }

    public void SetMaxHealth(int HP)
    {
        health.maxValue = HP;
        health.value = HP;
    }

    public void SetHealth(int hp)
    {
        health.value = hp;
    }

    public void SetMaxSuper(int SP)
    {
        super.maxValue = SP;
        super.value = SP;
    }

    public void SetSuper(int sp)
    {
        super.value = sp;
    }
    // Update is called once per frame
    void Update()
    {
        //get health funtion
    }
}
