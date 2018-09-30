using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntRange {
    //The minimum and maximum values in this range.
    public int m_Min;
    public int m_Max;

    //Constructer to set values
    public IntRange(int min, int max)
    {
        m_Min = min;
        m_Max = max;
    }

    //Get a random value from this range.
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}
