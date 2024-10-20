using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    public GameObject[] substances;
    public float rotationSpeed = 1f;
    public float moveSpeed = 2f; // �������� ���������� ������
    public float moveRadius = 5f; // ����� ���� ���������� ������
    public GameObject arrow;
    public GameObject character;
    public Camera mainCamera;

    void Update()
    {
        bool allNull = true;
        foreach (var element in substances)
        {
            if (element != null)
            {
                allNull = false;
                break; // ��������� ��������, ���� �������� ���������� �������
            }
        }

        if (allNull)
        {
            arrow.SetActive(false);
        }
        if (substances.Length == 0 || arrow == null || character == null || mainCamera == null)
            return;

        GameObject targetSubstance = GetClosestSubstance();
        if (targetSubstance == null)
            return;

        // ���������� �������� �� ��������� �� ��������
        Vector3 direction = targetSubstance.transform.position - character.transform.position;

        // ��������� ���� ��������� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        arrow.transform.rotation = Quaternion.Slerp(arrow.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // ���������� ������ �� ������� �������� � ����� ������ ���� �� ����� ������� ������
        float distanceToSubstance = direction.magnitude;
        if (distanceToSubstance > moveRadius)
        {
            Vector3 newPosition = mainCamera.transform.position + direction.normalized * moveRadius;
            arrow.transform.position = new Vector3(newPosition.x, newPosition.y, arrow.transform.position.z);
        }
        else
        {
            arrow.transform.position = new Vector3(targetSubstance.transform.position.x, targetSubstance.transform.position.y, arrow.transform.position.z);
        }
    }

    GameObject GetClosestSubstance()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestSubstance = null;

        foreach (GameObject substance in substances)
        {
            if (substance != null)
            {
                float distance = Vector3.Distance(character.transform.position, substance.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSubstance = substance;
                }
            }
        }

        return closestSubstance;
    }
}
