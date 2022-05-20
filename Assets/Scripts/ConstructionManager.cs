using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    //This is a primitive implementation of the construction system. Everything here is subject to change.

    const int objects = 3;

    Vector3 worldPosition;

    readonly static int grdX = 50;
    readonly static int grdY = 50;

    int selectedID;

    [SerializeField] bool constructionMode = false;

    //It took me so fucking long to figure out how to use structs, but now that I know I've got to say it's
    //helping me out a lot.
    public struct ConstructableObject
    {
        public string name;
        public int id;

        public Sprite sprite;

        public bool collide;

        public List<int> destructableObjects;

        public ConstructableObject(string n, int iD, Sprite sprt, bool col, List<int> desObjs)
        {
            //I still don't really understand why structs are like this, but it works, so whatever.

            name = n;
            id = iD;
            sprite = sprt;
            collide = col;
            destructableObjects = desObjs;
        }
    }

    ConstructableObject[] objectIDs = new ConstructableObject[objects];
    public Sprite[] sprites = new Sprite[objects];

    int[,] gridIDs = new int[grdX, grdY];
    GameObject[,] gridObjects = new GameObject[grdX, grdY];

    public List<int>[] desObjsLists = new List<int>[objects] { 
        new List<int>() { 1, 2 },
        new List<int>() { 0, 2 },
        new List<int>() { 0 }
    };

    private void Awake()
    {
        AssignObjectIDs();
    }

    private void Start()
    {
        //Creates a starting grid of air tiles, 50x50. From there you can change whatever is on the grid.
        for (int a = 0; a < 50; a++)
        {
            ConstructNewObject(objectIDs[0], new Vector3(a, 0, 0));

            for (int b = 1; b < 50; b++)
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

        Vector3 worldPositionRnd = new Vector3(Mathf.Round(worldPosition.x), Mathf.Round(worldPosition.y), 0);

        bool inGrid = (worldPositionRnd.x < 50 && worldPositionRnd.x >= 0) && (worldPositionRnd.y < 50 && worldPositionRnd.y >= 0);

        if (Input.GetMouseButtonDown(0) && constructionMode && inGrid)
        {
            int x = (int)worldPositionRnd.x;
            int y = (int)worldPositionRnd.y;

            int currentObjectID = gridObjects[x, y].GetComponent<ObjectData>().id;

            bool canPlace = false;

            foreach (int i in desObjsLists[selectedID])
            {
                if (i == currentObjectID)
                {
                    canPlace = true;
                }
            }

            if (canPlace)
            {
                ChangeObjectID((int)worldPositionRnd.x, (int)worldPositionRnd.y, objectIDs[selectedID]);
            }
        }
    }

    void AssignObjectIDs()
    {
        objectIDs[0] = new ConstructableObject("air", 0, sprites[0], false, desObjsLists[0]);
        objectIDs[1] = new ConstructableObject("wall", 1, sprites[1], true, desObjsLists[1]);
        objectIDs[2] = new ConstructableObject("floor", 2, sprites[2], false , desObjsLists[2]);
    }

    void ConstructNewObject(ConstructableObject objectProperties, Vector3 position)
    {
        GameObject ConstructedObject = new GameObject();

        ConstructedObject.GetComponent<Transform>().position = position;

        ConstructedObject.AddComponent<SpriteRenderer>();
        ConstructedObject.AddComponent<BoxCollider2D>();
        ConstructedObject.AddComponent<ObjectData>();

        ConstructedObject.GetComponent<SpriteRenderer>().sprite = objectProperties.sprite;
        ConstructedObject.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        ConstructedObject.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);

        ConstructedObject.GetComponent<BoxCollider2D>().enabled = objectProperties.collide;
        ConstructedObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);

        ConstructedObject.GetComponent<ObjectData>().id = objectProperties.id;
        ConstructedObject.GetComponent<ObjectData>().idName = objectProperties.name;
        ConstructedObject.GetComponent<ObjectData>().x = (int)position.x;
        ConstructedObject.GetComponent<ObjectData>().y = (int)position.y;

        int x = ConstructedObject.GetComponent<ObjectData>().x;
        int y = ConstructedObject.GetComponent<ObjectData>().y;

        ConstructedObject.name = $"{x}:{y}";

        if (objectProperties.collide)
        {
            ConstructedObject.AddComponent<Rigidbody2D>();
            ConstructedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (objectProperties.id == 0)
        {
            ConstructedObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        gridIDs[x, y] = ConstructedObject.GetComponent<ObjectData>().id;

        gridObjects[x, y] = ConstructedObject;
    }

    void ChangeObjectID(int x, int y, ConstructableObject newObjectProperties)
    {
        GameObject targetObject = gridObjects[x, y];

        targetObject.GetComponent<ObjectData>().id = newObjectProperties.id;
        targetObject.GetComponent<ObjectData>().idName = newObjectProperties.name;

        targetObject.GetComponent<SpriteRenderer>().sprite = newObjectProperties.sprite;

        bool notAir = newObjectProperties.id != 0;

        targetObject.GetComponent<SpriteRenderer>().enabled = notAir;

        bool hasCollision = targetObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D);

        if (newObjectProperties.collide != hasCollision)
        {
            if (hasCollision)
            {
                Destroy(rb2D);
            }
            if (!hasCollision)
            {
                targetObject.AddComponent<Rigidbody2D>();

                targetObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        targetObject.GetComponent<BoxCollider2D>().enabled = newObjectProperties.collide;

        gridIDs[x, y] = targetObject.GetComponent<ObjectData>().id;

        Debug.LogWarning($"ID changed at {x}, {y}");
    }
}
