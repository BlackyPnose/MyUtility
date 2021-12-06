using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Reflection;

namespace MyUtility
{
    //I need to set this class public and abstract to every other class can access and inherit
    public abstract class Entity
    {
        //Properties
        public int Id { get; set; }

        //Constructor
        public Entity() { }

        public Entity(int id)
        {
            Id = id;
        }

        //Methods
        public override string ToString()
        {
            string ris = "";

            foreach (PropertyInfo property in this.GetType().GetProperties())
                ris += $"{property.Name}: {property.GetValue(this)}\n";

            return ris;
        }
        public void FromDictionary(Dictionary<string, string> riga)
        {

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (riga.ContainsKey(property.Name.ToLower()))
                {
                    object valore = riga[property.Name.ToLower()];

                    switch (property.PropertyType.Name.ToLower())
                    {
                        case "datetime":
                            valore = DateTime.Parse(riga[property.Name.ToLower()]);
                            break;
                        case "int32":
                            valore = int.Parse(riga[property.Name.ToLower()]);
                            break;
                        case "double":
                            valore = double.Parse(riga[property.Name.ToLower()]);
                            break;
                        case "boolean":
                            valore = bool.Parse(riga[property.Name.ToLower()]);
                            break;
                    }
                    property.SetValue(this, valore);
                }
            }


        }
    }
}
