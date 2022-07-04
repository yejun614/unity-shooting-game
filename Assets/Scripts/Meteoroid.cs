using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoroid : MonoBehaviour
{
    public float HeathPoint = 20.0f;
    public float AttackPoint = 0;
    public Vector3 Direction = Vector3.up;
    public float Angle = 0;
    public float Speed = 1.0f;
    public GameObject DropItem;

    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;
        transform.Rotate(0, 0, Angle * Time.deltaTime);

        if (HeathPoint <= 0) Destroy(gameObject);
    }

    void OnDestroy() {
        if (DropItem == null) return;
        
        Instantiate(DropItem, transform.position, Quaternion.identity);
    }

    IEnumerator Highlight()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.44f, 0.44f, 0.44f, 1);
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void Hit(float damage)
    {
        HeathPoint -= damage;

        StartCoroutine(Highlight());
    }

    public void SetRandomParams()
    {
        Direction.x = Random.Range(-1.0f, 1.0f);
        Direction.y = Random.Range(-1.0f, 1.0f);

        Angle = Random.Range(-180, 180);

        float scale = Random.Range(1, 5);
        transform.localScale = new Vector3(scale, scale, 1);
        AttackPoint = scale * 10;
    }
}
