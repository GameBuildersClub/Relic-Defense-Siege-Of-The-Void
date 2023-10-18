using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthController : MonoBehaviour
{
    float maxHealth = 100f;
    [SerializeField] float maxHeight = 64; // Python naming convention: max_height
    [SerializeField] float minHeight = 33; // Camelcase naming convention: maxHeight
    [SerializeField] RectTransform rectTransform;

    public void UpdateHealth(float health)
    {
        Vector2 size = rectTransform.sizeDelta;
        size.x = (health / 100) * (maxHeight - minHeight) + minHeight;
        rectTransform.sizeDelta = size;
    }
}
