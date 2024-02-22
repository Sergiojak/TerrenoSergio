using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject m_Player;
    Vector3 m_PlayerMovement;

    [SerializeField]
    float m_PlayerSpeed = 5f;

    [SerializeField]
    float m_IncrementPlayerSpeed = 5f;

    [SerializeField]
    float m_DecrementPlayerSpeed = 5f;

    Rigidbody m_Rigidbody;

    public Vector2 m_Turn;
    public float m_TurnSensitivity = 0.5f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.position += transform.forward * m_PlayerSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
        {
            m_PlayerSpeed = m_PlayerSpeed + m_IncrementPlayerSpeed;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            m_PlayerSpeed = m_PlayerSpeed - m_IncrementPlayerSpeed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_PlayerSpeed = m_PlayerSpeed - m_DecrementPlayerSpeed;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_PlayerSpeed = m_PlayerSpeed + m_DecrementPlayerSpeed;
        }

        m_Turn.x += Input.GetAxis("Mouse X") * m_TurnSensitivity;
        m_Turn.y += Input.GetAxis("Mouse Y") * m_TurnSensitivity;
        transform.localRotation = Quaternion.Euler(-m_Turn.y, m_Turn.x, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chocaste");

        Object.Destroy(m_Player);
        Debug.Log("Has perdido");
    }

}



