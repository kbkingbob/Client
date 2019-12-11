using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public Transform m_sprite;
    public float m_Speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fMoveSpeed = m_Speed * UnityEngine.Time.deltaTime;
        m_sprite.Translate(Vector3.left * fMoveSpeed, Space.World);
    }
}
