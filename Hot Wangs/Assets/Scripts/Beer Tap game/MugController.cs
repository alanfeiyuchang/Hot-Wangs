using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float speed;

    float tilt;

    private void Start()
    {
        rb2d.centerOfMass = Vector2.zero;
        rb2d.inertia = 2;
    }

    private void Update()
    {
        tilt = Input.GetAxis("Horizontal");
    }

    private void LateUpdate()
    {
        rb2d.AddTorque(tilt * speed * Time.deltaTime);
    }

    public void ResetMug()
    {
        rb2d.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
        tilt = 0f;
    }
}
