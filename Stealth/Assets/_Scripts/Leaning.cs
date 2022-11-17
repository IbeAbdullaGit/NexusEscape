using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;

public class Leaning : MonoBehaviour
{
    public KeyCode leanLeftKey = KeyCode.Q;
    public KeyCode leanRightKey = KeyCode.E;

    [SerializeField] private float m_Amount = 10.0f;
    [SerializeField] private float m_LeaningSpeed = 2f;

    //private FirstPersonController m_FPSController;
    private Transform m_CameraTransform;

    private Vector3 initial_Pos;
    private Quaternion initial_Rot;

    private bool is_leaningLeft = false;
    private bool is_leaningRight = false;

    void Start() {
       // m_FPSController = GetComponent<FirstPersonController>();
        m_CameraTransform = transform.GetChild(0);

        initial_Pos = m_CameraTransform.position;
        initial_Rot = m_CameraTransform.rotation;
    }

   void Update() {
        if (Input.GetKey(leanLeftKey))
        {
            is_leaningLeft = true;
        }
        else{
            is_leaningLeft = false;
        }
        if (Input.GetKey(leanRightKey))
        {
            is_leaningRight = true;
        }
        else{
            is_leaningRight = false;
        }

        CheckCanLeanLeft();
        CheckCanLeanRight();
        CheckLeaning();
    }

    void CheckCanLeanLeft()
    {
        RaycastHit hit;
        //shoot ray on left camera side and check any obstacles can hit
        if (Physics.Raycast(m_CameraTransform.position, m_CameraTransform.TransformDirection(Vector3.left *0.5f), out hit, 0.5f))
        {
            is_leaningLeft = false;
        }
    }
    void CheckCanLeanRight()
    {
RaycastHit hit;
        //shoot ray on left camera side and check any obstacles can hit
        if (Physics.Raycast(m_CameraTransform.position, m_CameraTransform.TransformDirection(Vector3.right *0.5f), out hit, 0.5f))
        {
            is_leaningRight = false;
        }
    }

    void CheckLeaning()
    {
        if (is_leaningLeft)
        {
            //m_FPSController.SetRotateZ(m_Amount);

            //Vector3 newPos = new Vector3(initial_Pos.x - 0.5f, initial_Pos.y,initial_Pos.z);
            //m_CameraTransform.localPosition = Vector3.Lerp(m_CameraTransform.localPosition, newPos, Time.deltaTime *m_LeaningSpeed);

            Vector3 newRot = new Vector3(m_CameraTransform.eulerAngles.x, m_CameraTransform.eulerAngles.y,-m_Amount);
            //newRot.z = Mathf.Clamp(newRot.z, -90f, 90f);
            m_CameraTransform.eulerAngles = Vector3.Lerp(newRot, m_CameraTransform.eulerAngles, Time.deltaTime*-m_LeaningSpeed);

           
        }
        else if (is_leaningRight)
        {
            //m_FPSController.SetRotateZ(-m_Amount);
           //Vector3 newPos = new Vector3(initial_Pos.x + 0.5f, initial_Pos.y,initial_Pos.z);
           // m_CameraTransform.localPosition = Vector3.Lerp(m_CameraTransform.localPosition, newPos, Time.deltaTime *m_LeaningSpeed);

            Vector3 newRot = new Vector3(m_CameraTransform.eulerAngles.x, m_CameraTransform.eulerAngles.y,m_Amount);
            m_CameraTransform.eulerAngles = Vector3.Lerp(m_CameraTransform.eulerAngles, newRot, Time.deltaTime*m_LeaningSpeed);
        }
        else{
            //m_FPSController.SetRotateZ(initial_Rot.eulerAngles.z);
            //m_CameraTransform.localPosition = Vector3.Lerp(m_CameraTransform.localPosition, initial_Pos, Time.deltaTime*m_LeaningSpeed);
            //m_CameraTransform.localPosition = initial_Pos;

            //set to initial
            Vector3 newRot = new Vector3(m_CameraTransform.eulerAngles.x, m_CameraTransform.eulerAngles.y,initial_Rot.z);
            m_CameraTransform.eulerAngles = Vector3.Lerp(newRot, m_CameraTransform.eulerAngles, Time.deltaTime*m_LeaningSpeed);

            //m_CameraTransform.eulerAngles = new Vector3(m_CameraTransform.eulerAngles.x, m_CameraTransform.eulerAngles.y,initial_Rot.eulerAngles.z);
        }

       
    }
}


