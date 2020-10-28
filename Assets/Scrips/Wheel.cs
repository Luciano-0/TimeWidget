using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private TweenPosition tweenPosition;
    private UIPlayTween playTween;
    private UIGrid grid;
    private List<UILabel> numbers = new List<UILabel>();
    private Vector3 position;
    private int carry;
    private int num;
    public int Carry
    {
        get
        {
            return carry;
        }
        set
        {
            if (value < 2) carry = 10;
            else carry = value;
        }
    }
    public int Value
    {
        get
        {
            return num;
        }
        set
        {
            if (value >= carry) num = value % carry;
            else if (value < 0) num = 0;
            else num = value;
        }
    }

    private UIReference r;

    public void Init()
    {
        r = GetComponent<UIReference>();
        tweenPosition = r.Get<TweenPosition>(0);
        playTween = r.Get<UIPlayTween>(1);
        grid = r.Get<UIGrid>(2);
        for (int i = 3; i <= 7; i++)
            numbers.Add(r.Get<UILabel>(i));
        position = transform.localPosition;
    }

    public void StartRoll(int goal)
    {
        if (goal == 0) return;
        SetVisiable(true);
        StartCoroutine(RollToNum(goal));
    }

    private IEnumerator RollToNum(int value)
    {
        int times = 150;
        float step = grid.cellHeight * value / times;
        float stepValue = value * 1.0f / times;
        float now = 0;
        int middle = 0;
        Vector3 tempPos;
        for (int i = 0; i < times; i++)
        {
            tempPos = transform.localPosition;
            tempPos.y += GetCoefficient(times, i) * step;
            grid.transform.localPosition = tempPos;
            now += GetCoefficient(times, i) * stepValue;
            if (now - middle >= 1)
            {
                tempPos = grid.transform.localPosition;
                tempPos.y -= grid.cellHeight*(int)(now-middle);
                grid.transform.localPosition = tempPos;
                SetWheel((int)now);
                middle = (int)now;
            }
            yield return 0;
        }
        SetWheel(value);
        SetVisiable(false);
        grid.transform.localPosition = position;
    }

    private float GetCoefficient(int times, int i)
    {
        if (i < times / 2) return (4.0f * i) / times;
        else return (4.0f * (times - i)) / times;
    }

    public void SetWheel(int value)
    {
        numbers[0].text = (value % carry).ToString("00");
        numbers[1].text = ((value + carry - 2) % carry).ToString("00");
        numbers[2].text = ((value + carry - 1) % carry).ToString("00");
        numbers[3].text = ((value + carry + 1) % carry).ToString("00");
        numbers[4].text = ((value + carry + 2) % carry).ToString("00");
    }

    private void SetVisiable(bool value)
    {
        for(int i = 1; i <= 4; i++)
        {
            numbers[i].gameObject.SetActive(value);
        }
    }
}
