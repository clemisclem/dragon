using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class patrol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private int destPoint = 0;
    [SerializeField] private Transform cam;
    private bool notplay = true;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        if(notplay)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            // Si l'ennemi est quasiment arrivé à sa destination
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
            }
        }
        else if (!notplay)
        {
            Vector3 dir = cam.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
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
