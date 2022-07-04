using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10.0f;
    public Vector3 Direction = Vector3.up;
    public float Speed = 10.0f;
    public float TimeLimit = 3.0f;
    float timestamp = 0;

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;

        timestamp += Time.deltaTime;
        if (timestamp >= TimeLimit) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Meteoroid")
        {
            other.GetComponent<Meteoroid>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
