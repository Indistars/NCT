using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDataExtraction : InteractionIdentification
{
    /// <summary>
    /// 상호작용할 오브젝트의 데이터 값을 받아오는 작업
    /// </summary>
    /// <param name="interactiveObject">상호작용할 오브젝트</param>
    protected void DataExtract(GameObject interactiveObject)
    {
        Debug.Log(interactiveObject);
    }
}
