using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Points")]
    public int Level = 1;
    public float HeathPoint = 100;
    public int ExperiencePoint = 0;
    public int StepExperiencePoint = 5;
    
    [Header("Movement")]
    public float moveSpeed = 5.0f;
    public GameObject PlayerObject;

    [Header("Target")]
    public GameObject TargetObject;
    public Sprite idleTargetSprite;
    public Sprite lockTargetSprite;

    [Header("Bullet")]
    public GameObject BulletObject;

    [Header("GameOver")]
    public GameObject GameOverPanel;
    private bool isMovable = true;


    void Start()
    {
        NewGame();
    }

    void Update()
    {
        if (isMovable)
        {
            Movement();
            RotateFollowMouse();
            if (Input.GetButtonDown("Fire1")) FireReady();
            if (Input.GetButtonUp("Fire1")) Fire();
            LevelCheck();
        }

        GameOverCheck();
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector2.up * moveSpeed * Time.deltaTime);
        transform.Translate(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed * Time.deltaTime);
    }

    void RotateFollowMouse()
    {
        // [Unity Nuggets: How to "Look At" Mouse in 2D Game]
        // https://www.youtube.com/watch?v=Geb_PnF1wOk
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(PlayerObject.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
        PlayerObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the target sprite
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 1;
        TargetObject.transform.position = position;
    }

    void FireReady()
    {
        TargetObject.GetComponent<SpriteRenderer>().sprite = lockTargetSprite;
    }

    void Fire()
    {
        // Set target object scale
        TargetObject.GetComponent<SpriteRenderer>().sprite = idleTargetSprite;

        // Fire a bullet
        Vector3 position = transform.position;
        position.z = 1;
        Quaternion rotation = PlayerObject.transform.rotation;

        GameObject bullet = Instantiate(BulletObject, position, rotation);
        bullet.GetComponent<Bullet>().Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
    }

    void LevelCheck()
    {
        if (ExperiencePoint >= StepExperiencePoint * Level)
        {
            // TODO: Select screen

            Level ++;
            ExperiencePoint = 0;
        }
    }

    void GameOverCheck()
    {
        if (HeathPoint <= 0)
        {
            isMovable = false;
            Cursor.visible = true;
            GameOverPanel.SetActive(true);
            GetComponent<MeteoroidGenerator>().RemoveAll();
        }
    }

    public void Hit(float damage)
    {
        HeathPoint -= damage;
    }

    public void NewGame()
    {
        // Remove All Itesm
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i ++) Destroy(items[i]);

        // Reset Parameters
        HeathPoint = 100;
        Level = 1;
        ExperiencePoint = 0;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        // Set movable
        isMovable = true;
        Cursor.visible = false;
        GameOverPanel.SetActive(false);
    }
}
