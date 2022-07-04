using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    public Player player;
    private TMP_Text levelText; 

    void Awake()
    {
        levelText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        levelText.text = $"Lv. {player.Level}";
    }
}
