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
        DataBase.DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal 불러오기, 파싱 ※나중에 게임 로딩창에서 사용 예정※ */
        SpawnAnimal(0);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void SpawnAnimal(int index)
    {
        Instantiate(animalModels[index], animalSpawnPoint);
        Debug.Log("동물 :"+ animalModels[index].name.ToString() + "이 스폰 되었습니다!");
    }
}

