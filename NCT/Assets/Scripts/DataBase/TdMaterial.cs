using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DataBase
{
    /// <summary>
    /// 재료 테이블 데이터
    /// </summary>
    public class TdMaterial : TableBase
    {
        public int Key;
        public string Name;
        public string Description;

        public override void SetJsonData(string key, JObject info)
        {
            base.SetJsonData(key, info);
            this.Key = Int32.Parse(key);

            this.Name = info["Name"].Value<string>();
            this.Description = info["Description"].Value<string>();
        }

        public override void CheckTable()
        {
            base.CheckTable();
            Debug.Log($"\t\t_______Table Checker_______\n\t\tName : {this.Name} \n" +
                      $"\t\tDescription : {this.Description}\n\t\t________________________________");
        }
    }
}