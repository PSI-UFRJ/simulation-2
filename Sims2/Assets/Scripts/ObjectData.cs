using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData 
{
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public List<string> curiosity { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        curiosity = new List<string>();
    }

    #region Setters
    public void SetId(string id_)
    {
        id = id_;
    }

    public void SetName(string name_)
    {
        name = name_;
    }

    public void SetType(string type_)
    {
        type = type_;
    }

    public void SetCuriosity(List<string> curiosity_)
    {
        curiosity = curiosity_;
    }
    #endregion

    #region Getters
    public string GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public string GetType()
    {
        return type;
    }

    public List<string> GetCuriosity()
    {
        return curiosity;
    }
    #endregion

}
