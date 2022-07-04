using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public Player player;
    public float MaxHealthPoint = 100;

    void Awake()
    {
        MaxHealthPoint = player.HeathPoint;
    }

    void Update()
    {
        if (player.HeathPoint > MaxHealthPoint) MaxHealthPoint = player.HeathPoint;
        UpdateBattery();
    }

    void UpdateBattery()
    {
        // Get Image of Battery cells
        // A first Image that method of GetComponentsInChildren is a parent self,
        // so skip first image and then convert to array.
        Image[] cells = gameObject.GetComponentsInChildren<Image>().Skip(1).ToArray<Image>();

        float step = MaxHealthPoint / cells.Length;
        float current = 0;

        for (int i = 0; i < cells.Length; i ++)
        {
            cells[i].enabled = current < player.HeathPoint;
            current += step;
        }
    }
}
