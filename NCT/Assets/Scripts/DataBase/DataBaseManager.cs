using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : Singleton<DataBaseManager>
{
    public Dictionary<int, TDAnimal> tdAnimalDict = new Dictionary<int, TDAnimal>();

    void Start()
    {
        
    }

    public void LoadAnimalTable()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Animal_Json");

        tdAnimalDict.Clear();

        JObject parseObj = new JObject();
        parseObj = JObject.Parse(jsonText.text);
        foreach(KeyValuePair<string,JToken> pair in parseObj)
        {
            TDAnimal tdAnimal = new TDAnimal();
            tdAnimal.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
            tdAnimalDict.Add(tdAnimal.Key, tdAnimal);
        }

        Debug.Log("동물 테이블 완료");
    }

    public void CheckTable()
    {
        var td = tdAnimalDict[10001];
        Debug.Log($"key : {td.Key}");
        Debug.Log($"Name : {td.Name}");
        foreach (string str in td.Prefer_Food)
            Debug.Log($"Prefer_Food : {str}");
        foreach (string str in td.Order_Comment)
            Debug.Log($"Order_Comment : {str}");
        foreach (string str in td.Failed_Comment)
            Debug.Log($"Failed_Comment : {str}");
        foreach (string str in td.Success_Comment)
            Debug.Log($"Success_Comment : {str}");
        Debug.Log($"Description : {td.Description}");
    }
}
