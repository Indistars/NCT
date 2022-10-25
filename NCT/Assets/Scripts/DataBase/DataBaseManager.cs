using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    /// <summary>
    /// DataBaseManager - TD데이터들 불러오기
    /// </summary>
    public class DataBaseManager : Singleton<DataBaseManager>
    {
        /* 파싱된 데이터들을 다른 곳에서 사용하기 위한 딕셔너리 */
        public Dictionary<int, TdAnimal> tdAnimalDict = new Dictionary<int, TdAnimal>();
        public Dictionary<int, TdFood> tdFoodDict = new Dictionary<int, TdFood>();
        public Dictionary<int, TdMaterial> tdMaterialDict = new Dictionary<int, TdMaterial>();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 동물 데이터 불러오기 및 파싱 진행
        /// </summary>
        public void LoadAnimalTable()
        {
            try
            {
                Debug.Log("동물 테이블 파싱 시작");
                TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Animal_Json"); // 리소스파일 내부에 있는 Json 파일 불러오기

                tdAnimalDict.Clear();

                JObject parseObj = JObject.Parse(jsonText.text); // 불러온 Json파일을 JObject로 변환하기
                /* JObject로 변환된 Json파일을 파싱하기 */
                foreach (KeyValuePair<string, JToken> pair in parseObj)
                {
                    TDAnimal tdAnimal = new TDAnimal();
                    tdAnimal.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                    tdAnimalDict.Add(tdAnimal.Key, tdAnimal); // tdAnimalDict 딕셔너리에 파싱시킨 객체 넣기
                }

                Debug.Log("동물 테이블 완료");
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        /// <summary>
        /// 요리 데이터 불러오기 및 파싱 진행
        /// </summary>
        public void LoadFoodTable()
        {
            Debug.Log("음식 테이블 파싱 시작");
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Food_Json"); // 리소스파일 내부에 있는 Json 파일 불러오기

            tdFoodDict.Clear();

            JObject parseObj = JObject.Parse(jsonText.text); // 불러온 Json파일을 JObject로 변환하기
            /* JObject로 변환된 Json파일을 파싱하기 */
            foreach (KeyValuePair<string, JToken> pair in parseObj)
            {
                TdFood tdFood = new TdFood();
                tdFood.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdFoodDict.Add(tdFood.Key, tdFood); // tdFoodDict 딕셔너리에 파싱시킨 객체 넣기
            }

            Debug.Log("음식 테이블 완료");
        }

        /// <summary>
        /// 재료 데이터 불러오기 및 파싱
        /// </summary>
        public void LoadMaterialTable()
        {
            Debug.Log("재료 테이블 완료");
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Material_Json"); // 리소스파일 내부에 있는 Json 파일 불러오기

            tdMaterialDict.Clear();

            JObject parseObj = JObject.Parse(jsonText.text); // 불러온 Json파일을 JObject로 변환하기
            /* JObject로 변환된 Json파일을 파싱하기 */
            foreach (KeyValuePair<string, JToken> pair in parseObj)
            {
                TdMaterial tdMaterial = new TdMaterial();
                tdMaterial.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdMaterialDict.Add(tdMaterial.Key, tdMaterial); // tdFoodDict 딕셔너리에 파싱시킨 객체 넣기
            }

            Debug.Log("재료 테이블 완료");
        }

        /// <summary>
        /// Json 변환이 잘 榮쩝 확인하기 위한 메소드 (추후 삭제 예정)
        /// </summary>
        public void CheckTable()
        {
            tdAnimalDict[10001].CheckTable();
            tdFoodDict[20001].CheckTable();
            tdMaterialDict[30001].CheckTable();
        }
    }
}