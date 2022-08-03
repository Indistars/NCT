using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Import : MonoBehaviour
{
    [SerializeField] private GameObject[] animalModels;
    [SerializeField] private Transform animalSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        DataBase.DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal �ҷ�����, �Ľ� �س��߿� ���� �ε�â���� ��� ������ */
        SpawnAnimal(0);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void SpawnAnimal(int index)
    {
        Instantiate(animalModels[index], animalSpawnPoint);
        Debug.Log("���� :"+ animalModels[index].name.ToString() + "�� ���� �Ǿ����ϴ�!");
    }
}

