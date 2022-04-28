using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouvement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float mouvHor;
    [SerializeField] private float mouvVer;
    [SerializeField] private float current;
    [SerializeField] private float start;
    [Tooltip("Force add when player jump")]
    [SerializeField] private float jumpForce;
    [Tooltip("Force add when player is floating")]
    [SerializeField] private float floatingForce;
    [SerializeField] private float speed;
    [Tooltip("Force when the player shoot")]
    [SerializeField] private float force;
    [Tooltip("The gravity add when the player is falling (not when he is floating)")]
    [SerializeField] private float gravity;
    [Tooltip("Time you have to stay on the buttom to spawn the big ball")]
    [SerializeField] private float timer;
    [Tooltip("Masse add when eating")]
    [SerializeField] private float poid;
    [Tooltip("distance where the food is spwaned")]
    [SerializeField] private float distance;
    [Tooltip("How many things the player can eat")]
    [SerializeField] private float stomachMax;
    [SerializeField] private bool isJump = false;
    [SerializeField] private bool isFloating = false;
    [SerializeField] private GameObject mouth;
    [SerializeField] public List<GameObject> list;
    [SerializeField] public aspiration script;
    [SerializeField] public Transform cam;
    [SerializeField] private Quaternion rotateCam;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject boule;
    [SerializeField] private Slider slider;
    private float smooth;
    private bool conc = false;
    private bool needTime = false;

    [SerializeField] private List<aspiration> aspiredObjects;
    private void Start()
    {

    }
    private void Awake()
    {
        rotateCam = cam.rotation;
    }



    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                rb.AddForce(0, jumpForce, 0);
                isJump = true;
            }
            else
            {
                isFloating = true;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            isFloating = false;
            rb.useGravity = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            mouth.SetActive(true);

        }

        if (Input.GetButtonUp("Fire1"))
        {
            mouth.SetActive(false);

            foreach (aspiration asp in aspiredObjects)
                asp.aspired = false;

            aspiredObjects.Clear();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (list.Count > 0)
            {
                GameObject obj = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                obj.SetActive(true);
                obj.transform.position = model.transform.position + model.transform.forward * distance;
                addMass(-poid);
                Rigidbody objRb = obj.GetComponent<Rigidbody>();
                objRb.AddForce(model.transform.forward * force);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (list.Count > 0)
            {
                GameObject obj = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
                obj.SetActive(true);
                obj.transform.position = model.transform.position + model.transform.forward * distance;
                addMass(-poid);
            }

        }

        if (Input.GetButtonDown("X"))
        {
            if (list.Count == stomachMax)
            {
                conc = true;
                if (!needTime)
                {
                    start = Time.timeSinceLevelLoad;
                    needTime = true;

                }

            }
            

        }
        if (Input.GetButtonUp("X"))
        {
            needTime = false;
            conc = false;
            current = 0;
        }
        if (isJump)
        {
            rb.AddForce(0, -gravity, 0);
        }
        if (isFloating)
        {
            rb.useGravity = false;
            rb.AddForce(0, -floatingForce, 0);
        }
        mouvHor = Input.GetAxis("Horizontal");
        mouvVer = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(mouvHor, mouvVer).normalized;
        if (inputDir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            model.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(model.transform.eulerAngles.y, rotation, ref smooth, 0.2f);
        }
        rb.AddForce(mouvHor * speed, 0, mouvVer * speed);
        sliderTime();
        cam.rotation = rotateCam;
    }
    private void FixedUpdate()
    {
        //rb.velocity = new Vector3(mouvHor, 0, mouvVer) * Time.fixedDeltaTime * speed;
    }


    public void RegisterAspiredObject(aspiration aspired)
    {
        aspired.aspired = true;
        aspiredObjects.Add(aspired);
    }

    public void UnregisterAspiredObject(aspiration aspired)
    {
        aspired.aspired = false;
        aspiredObjects.Remove(aspired);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJump = false;
            isFloating = false;
            rb.useGravity = true;
        }
    }

    public void addMass(float masse)
    {
        rb.mass += masse;
    }

    public void addList(GameObject obj)
    {
        list.Add(obj);
    }

    public void Jump()
    {
        if (!isJump)
        {
            rb.AddForce(0, jumpForce, 0);
            isJump = true;
        }
        else
        {
            isFloating = true;
        }
    }
    public void stopJump()
    {
        isFloating = false;
        rb.useGravity = true;
    }

    public void concentration()
    {
        if (conc)
        {
            
            GameObject newboule = Instantiate(boule);
            newboule.transform.position = model.transform.forward + model.transform.position;
            newboule.transform.rotation = model.transform.rotation;
            foreach (GameObject obj in list)
            {
                Destroy(obj);
            }
            list.Clear();
            needTime = false;
            current = 0;
        }
        
    }

    public void sliderTime()
    {
        if(needTime)
        {
            current = Time.timeSinceLevelLoad - start;
        }
        slider.value = current;
        if (slider.value == slider.maxValue)
            concentration();


    }


}
