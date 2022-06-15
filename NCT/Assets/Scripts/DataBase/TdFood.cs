using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DataBase
{
   /// <summary>
   /// 음식 테이블 데이터
   /// </summary>
   public class TdFood : TableBase
   {
      public int Key;
      public string Name;
      public string Cooking_Way;
      public List<string> Materials = new List<string>();
      public string Description;

      /// <summary>
      /// 데이터 파싱하는 부분
      /// </summary>
      /// <param name="key">키값</param>
      /// <param name="info">키값을 제외한 나머지 부분</param>
      public override void SetJsonData(string key, JObject info)
      {
         base.SetJsonData(key, info);
         this.Key = Int32.Parse(key);

         this.Name = info["Name"].Value<string>();
         this.Cooking_Way = info["Cooking_Way"].Value<string>();
         foreach (var str in info["Materials"].Value<string>().Split(','))
            this.Materials.Add(str);
         this.Description = info["Description"].Value<string>();
      }

      /// <summary>
      /// 테이블을 확인하기 위한 메서드
      /// </summary>
      public override void CheckTable()
      {
         base.CheckTable();
         Debug.Log(
            $"\t\t_______Table Checker_______\n\t\tName : {this.Name} \n\t\tCooking_Way : {this.Cooking_Way} \n" +
            $"\t\tMaterials(Count) : {this.Materials.Count}\n\t\tDescription : {this.Description}\n\t\t________________________________");
      }
   }
}