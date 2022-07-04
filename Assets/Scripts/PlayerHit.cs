using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    Player player;

    void Awake() {
        player = transform.parent.GetComponent<Player>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Meteoroid")
        {
            float damage = other.gameObject.GetComponent<Meteoroid>().AttackPoint;
            player.Hit(damage);

            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Item")
        {
            int experiencePoint = other.gameObject.GetComponent<Item>().ExperiencePoint;
            player.ExperiencePoint += experiencePoint;

            Destroy(other.gameObject);
        }
    }
}
