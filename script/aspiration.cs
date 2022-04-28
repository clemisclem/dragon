using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aspiration : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    public bool aspired;
    [SerializeField] private float aspiForce;
    [SerializeField] private float addMasse;
    [SerializeField] public mouvement mouve;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        mouve = player.GetComponent<mouvement>();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) //aspiration
        {
            mouve.RegisterAspiredObject(this);
        }
        if (other.gameObject.layer == 10) //bouche
        {
            if(mouve.list.Count < 5)
            {
                mouve.addMass(addMasse);
                mouve.addList(this.gameObject);
                this.gameObject.SetActive(false);
                mouve.UnregisterAspiredObject(this);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)//aspiration
        {
            aspired = false;
            mouvement mouv = other.GetComponentInParent<mouvement>();
            mouv.script = null;
        }
    }

    private void Update()
    {
        if(aspired)
        {
            Vector3 dir = this.transform.position - player.transform.position;
            dir = dir.normalized;
            rb.AddForce( -dir * Time.deltaTime * aspiForce);
        }
    }

}
