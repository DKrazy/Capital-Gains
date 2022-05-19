using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    //This is a primitive implementation of the construction system. Everything here is subject to change.

    public static int objects = 3;

    Vector3 worldPosition;

    public int selectedID;

    [SerializeField] bool constructionMode = false;

    //It took me so fucking long to figure out how to use structs, but now that I know I've got to say it's
    //helping me out a lot.
    public struct ConstructableObject
    {
        public string name;
        public int id;

        public Sprite spRd;

        public bool collide;

        public ConstructableObject(string n, int iD, Sprite sprt, bool col)
        {
            //I still don't really understand why structs are like this, but it works, so whatever.

            name = n;
            id = iD;
            spRd = sprt;
            collide = col;
        }
    }

    ConstructableObject[] objectIDs = new ConstructableObject[objects];
    public Sprite[] sprites = new Sprite[objects];

    ConstructableObject[,] grid = new ConstructableObject[100, 100];

    private void Awake()
    {
        AssignObjectIDs();
    }

    private void Start()
    {
        //Creates a starting grid of air tiles, 100x100. From there you can change whatever is on the grid.
        for (int a = -50; a <= 50; a++)
        {
            ConstructNewObject(objectIDs[0], new Vector3(a, 0, 0));

            for (int b = -50; b <= 50; b++)
            {
                ConstructNewObject(objectIDs[0], new Vector3(a, b, 0));
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //This cycles through the different object IDs you can place. Just a placeholder while I develop a better
        //system.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedID++;

            if (selectedID >= objects)
            {
                selectedID = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && constructionMode)
        {
            ConstructNewObject(objectIDs[selectedID], worldPosition);
        }

        Debug.Log(worldPosition);
    }

    void AssignObjectIDs()
    {
        objectIDs[0] = new ConstructableObject("air", 0, sprites[0], false);
        objectIDs[1] = new ConstructableObject("wall", 1, sprites[1], true);
        objectIDs[2] = new ConstructableObject("floor", 2, sprites[2], false);
    }

    void ConstructNewObject(ConstructableObject objectProperties, Vector3 position)
    {
        int objectID = objectProperties.id;

        GameObject ConstructedObject = new GameObject();

        ConstructedObject.GetComponent<Transform>().position = position;

        ConstructedObject.AddComponent<SpriteRenderer>();
        ConstructedObject.AddComponent<BoxCollider2D>();

        ConstructedObject.GetComponent<SpriteRenderer>().sprite = sprites[objectID];
        ConstructedObject.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        ConstructedObject.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);

        ConstructedObject.GetComponent<BoxCollider2D>().enabled = objectProperties.collide;
        ConstructedObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

        if (objectProperties.collide)
        {
            ConstructedObject.AddComponent<Rigidbody2D>();
            ConstructedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (objectProperties.id == 0)
        {
            ConstructedObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
