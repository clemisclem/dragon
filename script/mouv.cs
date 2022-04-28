using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouv : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(this.transform.forward * force);
    }

}
