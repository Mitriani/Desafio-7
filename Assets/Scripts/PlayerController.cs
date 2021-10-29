using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject pared;
    private Rigidbody rbPlayer;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float triggerTime = 0.04f;
    [SerializeField] private float collisionTime = 0;
    private bool flag = true;
    private bool collisionFlag = true;
    void Start()
    {
        pared = GameObject.Find("Wall");
        rbPlayer = GetComponent<Rigidbody>(); 
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float vAxis = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, vAxis) * playerSpeed * Time.deltaTime);

        float hAxis = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(hAxis, 0, 0) * playerSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && collisionFlag == true)
        {
            collisionTime += Time.deltaTime;
        }
        if (collisionTime > 2 && collisionFlag == true)
        {
            pared.transform.position = new Vector3(-2, 2, -2);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
            collisionTime = 0;
            collisionFlag = false;
        }
        if (collision.gameObject.CompareTag("Wall") && collisionFlag == false)
        {
            collisionTime += Time.deltaTime;
        }
        if (collisionTime > 2 && collisionFlag == false)
        {
            pared.transform.position = new Vector3(-3, 2, 3);
            pared.transform.rotation = Quaternion.Euler(0, -45, 0);
            collisionTime = 0;
            collisionFlag = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerTime -= Time.deltaTime;
        if (flag)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (triggerTime < 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
        }
        if(triggerTime < 0)
        {
            triggerTime = 0.04f;
        }
    }
}
