using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    [SerializeField][Range(0, 100)] float health = 100;
    [SerializeField] float full_y_pos = 50f; // y position when box is fully square
    [SerializeField] float max_y_pos = 32.1f; // y position at top of health
    [SerializeField] float min_y_pos = 16.3f; // y position at bottom of health
    [SerializeField] float max_height = 100f; // height when box is fully square
    [SerializeField] float y_height_ratio = 2f; // change the height this much for each 1 y is changed

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ScaleHealthbar(float health)
    {
        float percent = health / 100;
        float total_space = max_y_pos - min_y_pos;
        float target_y = min_y_pos + total_space * percent;
        float target_height = max_height - ((full_y_pos - target_y) * y_height_ratio);
        Vector2 position = rectTransform.anchoredPosition;
        position.y = target_y;
        rectTransform.anchoredPosition = position;
        Vector2 size = rectTransform.sizeDelta;
        size.y = target_height;
        rectTransform.sizeDelta = size;
    }

    private void FixedUpdate()
    {
        ScaleHealthbar(health);
    }
}
