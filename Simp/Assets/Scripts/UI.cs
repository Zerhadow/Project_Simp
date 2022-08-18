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
        health.value = 50;
        super.value = 50;
        
    }

    // Update is called once per frame
    void Update()
    {
        //get health funtion
    }
}
