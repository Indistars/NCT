using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    /// <summary>
    /// DataBaseManager - TD�����͵� �ҷ�����
    /// </summary>
    public class DataBaseManager : Singleton<DataBaseManager>
    {
        /* �Ľ̵� �����͵��� �ٸ� ������ ����ϱ� ���� ��ųʸ� */
        public Dictionary<int, TdAnimal> tdAnimalDict = new Dictionary<int, TdAnimal>();
        public Dictionary<int, TdFood> tdFoodDict = new Dictionary<int, TdFood>();
        public Dictionary<int, TdMaterial> tdMaterialDict = new Dictionary<int, TdMaterial>();

        /// <summary>
        /// ���� ������ �ҷ����� �� �Ľ� ����
        /// </summary>
        public void LoadAnimalTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Animal_Json"); // ���ҽ����� ���ο� �ִ� Json ���� �ҷ�����

            tdAnimalDict.Clear();

            JObject parseObj = JObject.Parse(jsonText.text); // �ҷ��� Json������ JObject�� ��ȯ�ϱ�
            /* JObject�� ��ȯ�� Json������ �Ľ��ϱ� */
            foreach (KeyValuePair<string, JToken> pair in parseObj)
            {
                TdAnimal tdAnimal = new TdAnimal();
                tdAnimal.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdAnimalDict.Add(tdAnimal.Key, tdAnimal); // tdAnimalDict ��ųʸ��� �Ľ̽�Ų ��ü �ֱ�
            }

            Debug.Log("���� ���̺� �Ϸ�");
        }

        /// <summary>
        /// �丮 ������ �ҷ����� �� �Ľ� ����
        /// </summary>
        public void LoadFoodTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Food_Json"); // ���ҽ����� ���ο� �ִ� Json ���� �ҷ�����

            tdFoodDict.Clear();

            JObject parseObj = JObject.Parse(jsonText.text); // �ҷ��� Json������ JObject�� ��ȯ�ϱ�
            /* JObject�� ��ȯ�� Json������ �Ľ��ϱ� */
            foreach (KeyValuePair<string, JToken> pair in parseObj)
            {
                TdFood tdFood = new TdFood();
                tdFood.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdFoodDict.Add(tdFood.Key, tdFood); // tdFoodDict ��ųʸ��� �Ľ̽�Ų ��ü �ֱ�
            }

            Debug.Log("���� ���̺� �Ϸ�");
        }

        /// <summary>
        /// ��� ������ �ҷ����� �� �Ľ�
        /// </summary>
        public void LoadMaterialTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Material_Json"); // ���ҽ����� ���ο� �ִ� Json ���� �ҷ�����

            tdMaterialDict.Clear();

            JObject parseObj = JObject.Parse(jsonText.text); // �ҷ��� Json������ JObject�� ��ȯ�ϱ�
            /* JObject�� ��ȯ�� Json������ �Ľ��ϱ� */
            foreach (KeyValuePair<string, JToken> pair in parseObj)
            {
                TdMaterial tdMaterial = new TdMaterial();
                tdMaterial.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdMaterialDict.Add(tdMaterial.Key, tdMaterial); // tdFoodDict ��ųʸ��� �Ľ̽�Ų ��ü �ֱ�
            }

            Debug.Log("��� ���̺� �Ϸ�");
        }

        /// <summary>
        /// Json ��ȯ�� �� ����� Ȯ���ϱ� ���� �޼ҵ� (���� ���� ����)
        /// </summary>
        public void CheckTable()
        {
            tdAnimalDict[10001].CheckTable();
            tdFoodDict[20001].CheckTable();
            tdMaterialDict[30001].CheckTable();
        }
    }
}