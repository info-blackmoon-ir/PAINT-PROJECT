using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match_Details : MonoBehaviour
{
    
    public List<string> Players;
    public static Match_Details instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance == this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
