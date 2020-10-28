using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWheelWidget : MonoBehaviour
{
    public GameObject wheelItem;
    public GameObject wheelRoot;
    public UIGrid grid;

    private List<Wheel> wheels = new List<Wheel>();
    private int wheelNum;
    private List<int> values;
    private List<int> carrys;

    private void Start()
    {
        int[] carrysTest = new int[3] { 24, 60, 60 };
        int[] valuesTest = new int[3] { 18, 43, 27 };
        List<int> CarryTest = new List<int>(carrysTest);
        List<int> ValueTest = new List<int>(valuesTest);
        StartRolling(CarryTest, ValueTest);
    }

    private void StartRolling(List<int> wheelCarrys, List<int> wheelValues)
    {
        values = wheelValues;
        carrys = wheelCarrys;
        wheelNum = carrys.Count;
        InitWheels();
        SetValues();
        StartCoroutine(StartRoll());
    }

    private void InitWheels()
    {
        for (int i = 0; i < wheelNum; i++)
        {
            wheels.Add(wheelRoot.AddChild(wheelItem).GetComponent<Wheel>());
            wheels[i].gameObject.SetActive(true);
            wheels[i].Carry = carrys[i];
            wheels[i].Value = values[i];
            wheels[i].gameObject.transform.localPosition = new Vector3(i * grid.cellWidth, 0, 0);
            wheels[i].Init();
        }
    }

    private void SetValues()
    {
        for (int i = 0; i < values.Count; i++)
        {
            wheels[i].Value = values[i];
        }
    }

    IEnumerator StartRoll()
    {
        ClearWheels();
        for (int i = 0; i < values.Count; i++)
        {
            wheels[i].StartRoll(values[i]);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ClearWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.SetWheel(0);
        }
    }

}
