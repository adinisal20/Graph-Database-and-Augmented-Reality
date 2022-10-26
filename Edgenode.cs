
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class Edgenode : MonoBehaviour
{

    public float appliedforce = 2.0f;
    public float appliedforcedis = 2.0f;
    public float Desireddistance = 10;
    public float Desiredrepeldist = 10;
    float friction = 0.3f;
    float damping = 0;
    public LoadExcel L1;
    public int number = 10;
    public int indexelement=1;
    public int childno;
    public float spheresize=0.5f;
    public bool run_once=true;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<L1.GetComponent<LoadExcel>().itemDatabase.Count; i++)
        {
            L1.GetComponent<LoadExcel>().itemDatabase[i].Color = new Color(Random.value, Random.value, Random.value); 
        }

    }


    // Update is called once per frame
    void Update()
    {
        ApplyGraphForce();
        foreach (var node in L1.GetComponent<LoadExcel>().itemDatabase)
        {
            node.sphere.transform.position+=node.velocity*Time.deltaTime;
        }
        damping = Mathf.Sqrt(4*appliedforce);
        friction = Mathf.Sqrt(2 * appliedforcedis);
    }
    
    void OnDrawGizmos()
    {
        if(run_once){
            L1.LoadItemData();
            run_once=false;
        }
        
        var db = L1.GetComponent<LoadExcel>().itemDatabase;
        for (int i=0; i<db.Count; i++){
            for(int j=0;j<db[i].Child.Count;j++){
                    
                Gizmos.DrawLine(db[i].sphere.transform.position, db[i].Child[j].sphere.transform.position);
            }

            if(db[i].index>=2){
                Handles.Label(db[i].sphere.transform.position, db[i].name );
            }
        }         
        
    }


    private void ApplyGraphForce()
    {
       
        foreach (var inde in L1.GetComponent<LoadExcel>().itemDatabase)
        {
            Debug.Log(inde.index);
            foreach (var child in inde.Child)
            {
                var diff = child.sphere.transform.position - inde.sphere.transform.position;
                var dist = diff.magnitude;
                var force = appliedforce * diff.normalized * (Desireddistance-dist);
                var frictionforce = -child.velocity*damping;
                child.velocity += (force+ frictionforce) * Time.deltaTime;
            }
            foreach (var notchild in L1.GetComponent<LoadExcel>().itemDatabase.Except(inde.Child))
            {
                var diff = notchild.sphere.transform.position - inde.sphere.transform.position;
                var dist = diff.magnitude;
                if(dist>0.1)
                {
                    var repelforce = appliedforcedis * diff.normalized /Mathf.Pow(dist,4) ;
                    var frictionforce = -notchild.velocity * friction;
                    notchild.velocity += (repelforce + frictionforce) * Time.deltaTime;
                }
                
            }
        }
    }

}

