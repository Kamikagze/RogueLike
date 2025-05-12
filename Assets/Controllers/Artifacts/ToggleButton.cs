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
        // ���� � ��� ���� �������� ������, ������������ �
        if (activeButton != null && activeButton != this)
        {
            activeButton.Deactivate();
        }
        uI.ShowInfo(buttonNumber);
        // ���������� ������� ������
        activeButton = this;
        Activate();
    }

    void Activate()
    {
        // ����� �� ������ �������� ���������� ��������� ������
        // ��������, �������� ���� ��� ����� ������
        markers.SetActive(true);

        // �� ������ ������������� ���������� �����
        button.interactable = false;
    }

    public void Deactivate()
    {
        // ������� ���������� ��������� ������
        markers.SetActive(false);

        // ����� ������ ������ ������������
        button.interactable = true;
    }
    
}