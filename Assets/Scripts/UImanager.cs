using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance { get; private set; }

    public Image bar;

    private void Awake()
    {
        instance = this;
    }

    public void updateHealthbar(int curAmount, int nowAmount)
    {
        bar.fillAmount = (float)curAmount / (float)nowAmount;
    }
}
