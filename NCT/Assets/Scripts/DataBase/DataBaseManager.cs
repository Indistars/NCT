using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DataBaseManager - TD데이터들 불러오기
/// </summary>
public class DataBaseManager : Singleton<DataBaseManager>
{
    /* 파싱된 데이터들을 다른 곳에서 사용하기 위한 딕셔너리 */
    public Dictionary<int, TDAnimal> tdAnimalDict = new Dictionary<int, TDAnimal>(); 

    void Start()
    {
        
    }

    /// <summary>
    /// 동물 데이터 불러오기 및 파싱 진행
    /// </summary>
    public void LoadAnimalTable()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Animal_Json"); // 리소스파일 내부에 있는 Json 파일 불러오기

        tdAnimalDict.Clear();

        JObject parseObj = new JObject();
        parseObj = JObject.Parse(jsonText.text); // 불러온 Json파일을 JObject로 변환하기
        /* JObject로 변환된 Json파일을 파싱하기 */
        foreach(KeyValuePair<string,JToken> pair in parseObj)
        {
            TDAnimal tdAnimal = new TDAnimal();
            tdAnimal.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
            tdAnimalDict.Add(tdAnimal.Key, tdAnimal); // tdAnimalDict 딕셔너리에 파싱시킨 객체 넣기
        }

        Debug.Log("동물 테이블 완료");
    }

    /// <summary>
    /// Json 변환이 잘 됬는지 확인하기 위한 메소드 (추후 삭제 예정)
    /// </summary>
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
