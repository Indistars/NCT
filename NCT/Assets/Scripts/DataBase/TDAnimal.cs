using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TDAnimal Ŭ����
/// </summary>
public class TDAnimal : TableBase
{
    public int Key;
    public string Name;
    public List<string> Prefer_Food = new List<string>();
    public List<string> Order_Comment = new List<string>();
    public List<string> Failed_Comment = new List<string>();
    public List<string> Success_Comment = new List<string>();
    public string Description;

    /// <summary>
    /// ������ �Ľ��ϴ� �κ�
    /// </summary>
    /// <param name="key">Ű��</param>
    /// <param name="info">Ű���� ������ ������ ����</param>
    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        this.Key = Int32.Parse(key);

        this.Name = info["Name"].Value<string>();
        foreach (string str in info["Prefer_Food"].Value<string>().Split(','))
            this.Prefer_Food.Add(str);
        foreach (string str in info["Order_Comment"].Value<string>().Split(','))
            this.Order_Comment.Add(str);
        foreach (string str in info["Failed_Comment"].Value<string>().Split(','))
            this.Failed_Comment.Add(str);
        foreach (string str in info["Success_Comment"].Value<string>().Split(','))
            this.Success_Comment.Add(str);
        this.Description = info["Description"].Value<string>();
    }
}
