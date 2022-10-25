using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalImport : Singleton<AnimalImport>
{
    public int count;

    [SerializeField] private GameObject[] animalModels; // ���� ���� �������� ����
    [SerializeField] private Transform animalSpawnPoint; // ��ȯ�� ���� ��ġ�� ������ �ִ� ����

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); // ���� ���� �ʰ� ����
        DataBase.DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal �ҷ�����, �Ľ� �س��߿� ���� �ε�â���� ��� ������ */
        SpawnAnimal(count); // ���� ��ȯ �Լ�
    }

    public void SpawnAnimal()
    {
        Instantiate(animalModels[count], animalSpawnPoint); // �ش��ϴ� �ε����� ������ ��ȯ
        Debug.Log("���� :" + animalModels[count].name.ToString() + "�� ���� �Ǿ����ϴ�!"); // �˸�
        count++;
    }

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

