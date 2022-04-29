using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class patrol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private int destPoint = 0;
    [SerializeField] private Transform cam;
    private float smooth;
    [SerializeField] private Transform model;
    private bool notplay = true;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        
        if (notplay)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            // Si l'ennemi est quasiment arrivé à sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
            }
            
        }
        else if (!notplay)
        {
            Vector3 dir = (cam.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, cam.position) < 0.3f)
            {
                SceneManager.LoadScene(1);
            }
            
        }
        

    }


    public void onPlay()
    {
        notplay = false;
    }
}
