using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour, ITimeController
{
    private Transform treasure = null; // ������ �� ������
    public RectTransform indicator; // UI ���������
    public Camera mainCamera; // �������� ������
    public float edgeOffset = 0f; // �������������� ������ �� ���� ������
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
            Vector2 canvasSize = canvasRectTransform.sizeDelta; // ������� Canvas

            // �������� �� ���������
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                // ��������� �� ��������� ������, ���������� ���
                indicator.gameObject.SetActive(true);

                // ������������ ������� ��������� ��� ���������� Canvas
                Vector2 newPos = new Vector2(
                    Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset),
                    Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset)
                );

                indicator.anchoredPosition = newPos;

                // ������������ ��������� � �������
                Vector2 direction = treasure.position - mainCamera.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                indicator.localEulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                indicator.gameObject.SetActive(false); // ������ ���������, ���� ������ �� ������
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
