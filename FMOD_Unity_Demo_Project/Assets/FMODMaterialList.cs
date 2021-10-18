using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMaterialList : MonoBehaviour
{
    public string[] materialTagNames;
    public float[] materialValues;
    public string materialParameterName = "";

    public float GetTagValue(string tagName)
    {
        float tagValue = 0f;
        for (int i = 0; i < materialTagNames.Length; i++)
        {
            if(tagName == materialTagNames[i])
            {
                tagValue = materialValues[i];
                return tagValue;
            }
        }

        return tagValue;
    }
}
