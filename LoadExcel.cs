using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    public Item blankItem;
    public List<Item> itemDatabase = new List<Item>();
    public float spheresize=0.5f;
    public void LoadItemData(){    
        
        itemDatabase.Clear();

        List<Dictionary<string, object>> data = CSVReader.Read("ItemDatabase");
        for(var i = 0; i < data.Count; i++)
        {
            int id = int.Parse(data[i]["id"].ToString(), System.Globalization.NumberStyles.Integer);
            string name  = data[i]["name"].ToString();
            int index = int.Parse(data[i]["index"].ToString(), System.Globalization.NumberStyles.Integer);
            AddItem(id , name, index);        
        
        }
        List<Dictionary<string, object>> bata = CSVReader.Read("fromto");
        for(var i = 0; i < bata.Count; i++) {

            int from = int.Parse(bata[i]["from"].ToString(), System.Globalization.NumberStyles.Integer);
            int to = int.Parse(bata[i]["to"].ToString(), System.Globalization.NumberStyles.Integer);
            itemDatabase[from-1].Child.Add(itemDatabase[to-1]);   
        }
        
    } 
    void AddItem(int id, string name, int index)
    {
        Item tempItem = new Item(blankItem);

        tempItem.id = id;
        tempItem.name = name;
        tempItem.index = index;
        tempItem.Child = new List<Item>();
        tempItem.sphere = new GameObject();
        tempItem.sphere =GameObject.CreatePrimitive(PrimitiveType.Sphere);

        Vector3 pos = new Vector3(0,0,0)+Random.insideUnitSphere*3;

        tempItem.velocity = new Vector3(0,0,0);
        float icr = 2.0f;
        if (tempItem.index == 0){
            spheresize = 0.1f;
            tempItem.Color = Color.blue;
            pos = new Vector3(4.0f*icr,Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f) );
        }
        else if (tempItem.index == 1) {
            spheresize = 0.2f;
            tempItem.Color = Color.red;
            pos = new Vector3(3.0f*icr,Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f) );
        }
        else if (tempItem.index == 2) {
            spheresize = 0.3f;
            tempItem.Color = Color.green;
            pos = new Vector3(2.0f*icr,Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f) );
        }
        else if (tempItem.index == 3){
            spheresize = 0.4f;
            tempItem.Color = Color.yellow;
            pos = new Vector3(1.0f*icr,Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f) );
        }
        else {
            spheresize = 0.5f;
            tempItem.Color = Color.white;
            pos = new Vector3(0f*icr,Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f) );
        }    
        Debug.Log(spheresize);
        tempItem.sphere.transform.localScale = new Vector3(spheresize, spheresize, spheresize);
        tempItem.sphere.transform.position = pos;

        tempItem.sphere.GetComponent<Renderer>().material.color = tempItem.Color;
        tempItem.sphere.name = tempItem.name;
        itemDatabase.Add(tempItem);
        
    }
    void Start()
    {
        LoadItemData();
    }
}    