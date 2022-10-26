using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
        public int id;
        public string name;
        public int index;
        public List<Item> Child;
        public GameObject sphere;
        public Vector3 velocity;
        public Color Color;

        public Item(Item d)
        {
            id = d.id;
            name = d.name;
            Child = d.Child;
            sphere = d.sphere;
            velocity = d.velocity;
            Color = d.Color;
            index = d.index;
        }
        

}
