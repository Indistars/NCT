using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalImport : Singleton<AnimalImport>
{
    public int count;

    [SerializeField] private GameObject[] animalModels; // 동물 모델을 가져오는 변수
    [SerializeField] private Transform animalSpawnPoint; // 소환할 동물 위치를 가지고 있는 변수

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); // 삭제 되지 않게 설정
        DataBase.DataBaseManager.Instance.LoadAnimalTable(); /* tdAnimal 불러오기, 파싱 ※나중에 게임 로딩창에서 사용 예정※ */
        SpawnAnimal(count); // 동물 소환 함수
    }

    public void SpawnAnimal()
    {
        Instantiate(animalModels[count], animalSpawnPoint); // 해당하는 인덱스의 동물을 소환
        Debug.Log("동물 :" + animalModels[count].name.ToString() + "이 스폰 되었습니다!"); // 알림
        count++;
    }

    /// <summary>
    /// 동물을 소환하는 함수
    /// </summary>
    /// <param name="index">동물 인덱스 번호</param>
    public void SpawnAnimal(int index)
    {
        Instantiate(animalModels[index], animalSpawnPoint); // 해당하는 인덱스의 동물을 소환
        Debug.Log("동물 :"+ animalModels[index].name.ToString() + "이 스폰 되었습니다!"); // 알림
    }
}

