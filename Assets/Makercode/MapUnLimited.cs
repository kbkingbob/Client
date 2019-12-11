using UnityEngine;

using System.Collections;
using System.Collections.Generic;
public class MapUnLimited : MonoBehaviour
{

    public float m_Speed = 2.0f;    //移动速度
    public Transform m_Camera;
    public Transform m_BackGround;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            MoveBackGround(UnityEngine.Time.deltaTime);
    }

    void MoveBackGround(float fDelta)
    {
        float fMoveSpeed = m_Speed * fDelta;
        m_Camera.Translate(Vector3.right * fMoveSpeed, Space.World);
        m_BackGround.Translate(Vector3.right * fMoveSpeed, Space.World);
    }
}