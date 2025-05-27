using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour, ITimeController
{
    private Transform treasure = null; // Ссылка на сундук
    public RectTransform indicator; // UI указатель
    public Camera mainCamera; // Основная камера
    public float edgeOffset = 0f; // Дополнительный отступ от края экрана
    private float currentTime = 0f;

    [SerializeField] private RectTransform canvasRectTransform;
    public void Timerred(float deltaTime)
    {

        currentTime += deltaTime;
        if (currentTime >= 0.05f)
        {
            UIUpdater();
            currentTime = 0f;
        }

        
    }
    private void UIUpdater()
    {
        if (treasure != null)
        {
            Vector2 screenPos = mainCamera.WorldToScreenPoint(treasure.position);
            Vector2 canvasSize = canvasRectTransform.sizeDelta; // Размеры Canvas

            // Проверка на видимость
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                // Указатель за пределами экрана, отображаем его
                indicator.gameObject.SetActive(true);

                // Нормализация позиции указателя для контейнера Canvas
                Vector2 newPos = new Vector2(
                    Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset),
                    Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset)
                );

                indicator.anchoredPosition = newPos;

                // Поворачиваем указатель к сундуку
                Vector2 direction = treasure.position - mainCamera.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                indicator.localEulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                indicator.gameObject.SetActive(false); // Скрыть указатель, если сундук на экране
            }
        }
        else indicator.gameObject.SetActive(false);
    }
    private void SetTreasure(Transform chest)
    {
        Debug.Log($"{chest}");
        treasure = chest;
    }
    private void OnEnable()
    {
        ChectAction.ChestTransform += SetTreasure;
    }
    private void OnDisable()
    {
        ChectAction.ChestTransform -= SetTreasure;
    }
}
