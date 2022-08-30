using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    public float gravityForce, rotationForce;

    public Text textGravity, textRingMass;

    Vector3 direction = Vector3.down;

    public static float ringMass = 1;

    public Transform rings;

    private void Start()
    {
        //SetGravityForce();
        //ChangeGravity(0);
        //ChangeMass(0);
    }

    void SetGravityForce()
    {
        Physics.gravity = new Vector3(0, gravityForce, 0);
    }
    
    void FixedUpdate()
    {
        direction.x = Input.acceleration.x * rotationForce;
        Physics.gravity = direction * gravityForce;
    }
    
    public void ChangeGravity(int value)
    {
        
        gravityForce += value;
        textGravity.text = "Gravity " + ((int)gravityForce).ToString();
    }

    public void ChangeMass(int value)
    {
        ringMass += value;
        textRingMass.text = "Rings mass " + ringMass;
        foreach (Transform go in rings.transform)
        {
            
            go.GetComponent<Rigidbody>().mass = ringMass;
            
        }
    }
}
