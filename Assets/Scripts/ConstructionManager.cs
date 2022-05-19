using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    //It took me so fucking long to figure out how to use structs, but now that I know I've got to say it's
    //helping me out a lot.
    public struct ConstructableObject
    {
        public string name;

        public Vector2 position;

        public SpriteRenderer spRd;

        public BoxCollider2D collider;
        public Rigidbody2D rb2D;

        public bool collide;

        public ConstructableObject(string n, Vector2 pos, SpriteRenderer spRen, BoxCollider2D coldr, Rigidbody2D rb2Dim, bool col)
        {
            //I still don't really understand why structs are like this, but it works, so whatever.

            name = n;
            position = pos;
            spRd = spRen;
            collider = coldr;
            rb2D = rb2Dim;
            collide = col;

            collider.enabled = collide;

            if (collide)
            {
                rb2D.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb2D.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    [SerializeField] BoxCollider2D coldr;
    [SerializeField] Rigidbody2D rgdBdy;

    public List<ConstructableObject> objectIDs = new List<ConstructableObject>();
    public List<Sprite> sprites;

    void AssignObjectIDs()
    {
        objectIDs[0] = new ConstructableObject("air", Input.mousePosition, new SpriteRenderer(), coldr, rgdBdy, false);
    }
}
