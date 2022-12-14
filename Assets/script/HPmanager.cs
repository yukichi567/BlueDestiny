using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPmanager : MonoBehaviour
{

    public Color color_1, color_2;
    public float maxHP = 100;
    [Range(0, 100)] public float hp = 100;
    private Image image_HPgauge;
    private float hp_ratio;

    // Start is called before the first frame update
    void Start()
    {
        image_HPgauge = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hp_ratio = hp / maxHP;

        image_HPgauge.color = Color.Lerp(color_2, color_1, hp_ratio);
        image_HPgauge.fillAmount = hp_ratio;
    }
}