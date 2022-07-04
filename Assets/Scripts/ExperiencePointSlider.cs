using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperiencePointSlider : MonoBehaviour
{
    public Player player;
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = (float)player.ExperiencePoint / (float)(player.StepExperiencePoint * player.Level);
    }
}
