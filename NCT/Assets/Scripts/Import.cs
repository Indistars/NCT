using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Import : MonoBehaviour
{
    [SerializeField] private GameObject[] animalModels; // ���� ���� �������� ����
    [SerializeField] private Transform animalSpawnPoint; // ��ȯ�� ���� ��ġ�� ������ �ִ� ����

    // Start is called before the first frame update
    void Start()
    {
        DataBase.DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal �ҷ�����, �Ľ� �س��߿� ���� �ε�â���� ��� ������ */
        SpawnAnimal(0); // ���� ��ȯ �Լ�
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    /// <summary>
    /// ������ ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="index">���� �ε��� ��ȣ</param>
    public void SpawnAnimal(int index)
    {
        Instantiate(animalModels[index], animalSpawnPoint); // �ش��ϴ� �ε����� ������ ��ȯ
        Debug.Log("���� :"+ animalModels[index].name.ToString() + "�� ���� �Ǿ����ϴ�!"); // �˸�
    }
}

