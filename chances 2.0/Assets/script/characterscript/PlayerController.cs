using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    //raycast
    public float groundDist;
    public LayerMask terrainLayer;

    //movement
    public float speed = 5f;
    public Animator anim;
    private Vector3 movement;
    public Rigidbody rb;
    private const string lastVertical = "LastVertical";
    private const string lastHorizontal = "LastHorizontal";

    //camera
    [SerializeField] CinemachineVirtualCamera overworldCam;
    [SerializeField] CinemachineVirtualCamera closeCam;
    private Camera cameraChange;

    //Bool
    public bool Walking = true;

    void Start()
    {
        cameraChange = Camera.main;

        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Walking)
        {
            walk();
        }
    }
    
    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }
    private void OnEnable()
    {
        CameraSwitcher.Register(overworldCam);
        CameraSwitcher.Register(closeCam);
        CameraSwitcher.SwitchCamera(overworldCam);
    }
    private void OnDisable()
    {
        CameraSwitcher.Unregister(overworldCam);
        CameraSwitcher.Unregister(closeCam);
    }

   void walk()
   {
    //Raycast
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y +=1;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if(hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
        
    //pause movement
        if(dialogueManager.IsActive == true)
        return; 

    //player movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, 0f, y);
        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);
        
        
        if(movement != Vector3.zero)
        {
            anim.SetFloat(lastHorizontal, movement.x);
            anim.SetFloat(lastVertical, movement.z);
        }

    //camera angle changer
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CameraSwitcher.IsActiveCamera(overworldCam))
            {
                CameraSwitcher.SwitchCamera(closeCam);
            }
            else if(CameraSwitcher.IsActiveCamera(closeCam))
            {
                CameraSwitcher.SwitchCamera(overworldCam);
            }
        } 

   }
    private void LateUpdate()
    {
        if (cameraChange != null)
        {
            cameraChange.farClipPlane = 10000f; // force override every frame
        }
    }
}
