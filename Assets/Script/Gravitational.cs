using UnityEngine;
using System.Collections.Generic;

public class Gravitational : MonoBehaviour
{
    public static List<Gravitational> otherGameObject;
    private Rigidbody rb;
    const float G = 0.006674f; 

    // --- ส่วนที่เพิ่มเข้ามาสำหรับระบบโคจร (Orbit) ---
    [SerializeField] bool planet = false; // ถ้าเป็นดาวศูนย์กลางให้ติ๊กถูก
    [SerializeField] int orbitSpeed = 1000; // ความเร็วในการโคจร

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherGameObject == null){ otherGameObject = new List<Gravitational>(); } 
        otherGameObject.Add(this); 

        // --- เพิ่มแรงผลักตอนเริ่มเกมให้ดาวเคลื่อนที่ไปด้านข้าง ---
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed); 
        }
    }

    void FixedUpdate()
    {
        foreach (Gravitational obj in otherGameObject)
        { 
            if (obj != this) 
            { 
                AttractionForce(obj); 
            } 
        } 
    }

    void AttractionForce(Gravitational other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 dir = rb.position - otherRb.position; 
        float dist = dir.magnitude; 
        if (dist == 0f) { return; } 
        
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(dist, 2));
        Vector3 gravitationalForce = forceMagnitude * dir.normalized; 
        otherRb.AddForce(gravitationalForce); 
    }
}