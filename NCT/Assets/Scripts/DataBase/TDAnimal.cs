using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    /// <summary>
    /// 동물 테이블 데이터
    /// </summary>
    public class TdAnimal : TableBase
    {
        public int Key;
        public string Name;
        public List<string> Prefer_Food = new List<string>();
        public List<string> Disprefer_Food = new List<string>();
        public List<string> Order_Comment = new List<string>();
        public List<string> Order_Refuse = new List<string>();
        public List<string> Order_Accept = new List<string>();
        public List<string> Failed_Comment = new List<string>();
        public List<string> Success_Comment = new List<string>();
        public string Description;

        /// <summary>
        /// 데이터 파싱하는 부분
        /// </summary>
        /// <param name="key">키값</param>
        /// <param name="info">키값을 제외한 나머지 값들</param>
        public override void SetJsonData(string key, JObject info)
        {
            base.SetJsonData(key, info);
            this.Key = Int32.Parse(key);

            this.Name = info["Name"].Value<string>();
            foreach (string str in info["Prefer_Food"].Value<string>().Split(','))
                this.Prefer_Food.Add(str);
            foreach (string str in info["Disprefer_Food"].Value<string>().Split(','))
                this.Disprefer_Food.Add(str);
            foreach (string str in info["Order_Comment"].Value<string>().Split(','))
                this.Order_Comment.Add(str);
            foreach (string str in info["Order_Refuse"].Value<string>().Split(','))
                this.Order_Refuse.Add(str);
            foreach (string str in info["Order_Accept"].Value<string>().Split(','))
                this.Order_Accept.Add(str);
            foreach (string str in info["Failed_Comment"].Value<string>().Split(','))
                this.Failed_Comment.Add(str);
            foreach (string str in info["Success_Comment"].Value<string>().Split(','))
                this.Success_Comment.Add(str);
            this.Description = info["Description"].Value<string>();
        }

        public override void CheckTable()
        {
            base.CheckTable();
            Debug.Log(
                $"\t\t_______Table Checker_______\n\t\tName : {this.Name} \n\t\tPrefer_Food(Count) : {this.Prefer_Food.Count}\n\t\tDisprefer_Food(Count) : {this.Disprefer_Food.Count}\n" +
                $"\t\tOrder_Comment(Count) : {this.Order_Comment.Count}\n\t\tOrder_Refuse(Count) : {this.Order_Refuse.Count}\n\t\tOrder_Accept(Count) : {this.Order_Accept.Count}\n\t\tFailed_Comment(Count) : {this.Failed_Comment.Count}\n\t\tSuccess_Comment(Count) : " +
                $"{this.Success_Comment.Count}\n\t\tDescription : {this.Description}\n\t\t________________________________");
        }
    }
}
