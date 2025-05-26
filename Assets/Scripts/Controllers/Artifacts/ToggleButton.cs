using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    private Button button;
    private static ToggleButton activeButton;

    [SerializeField] int buttonNumber;
    [SerializeField] GameObject markers;
    [SerializeField] UI uI;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Если у нас есть активная кнопка, деактивируем её
        if (activeButton != null && activeButton != this)
        {
            activeButton.Deactivate();
        }
        uI.ShowInfo(buttonNumber);
        // Активируем текущую кнопку
        activeButton = this;
        Activate();
    }

    void Activate()
    {
        // Здесь вы можете изменить визуальное состояние кнопки
        // Например, изменить цвет или текст кнопки
        markers.SetActive(true);

        // Вы можете заблокировать дальнейшие клики
        button.interactable = false;
    }

    public void Deactivate()
    {
        // Сбросим визуальное состояние кнопки
        markers.SetActive(false);

        // Снова делаем кнопку кликабельной
        button.interactable = true;
    }
    
}