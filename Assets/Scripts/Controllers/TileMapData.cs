using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{

    

    public class TileMapData
    {
       

        public List<Bounds> collisionBoundsList = new List<Bounds>();
        public Bounds spawnBounds;
        public List<Bounds> objectBounds = new List<Bounds>();

        public List<string> objectStrings = new List<string>();

        public TileMapData(GameObject tileMapGameObject)
        {
            loadSpawn(tileMapGameObject);
            loadCollisionRectListFromPrefab(tileMapGameObject);
            loadObjectBounds(tileMapGameObject);
            loadObjectStringList();
        }

        private void loadObjectBounds(GameObject tileMapGameObject)
        {
            Transform objectChild = tileMapGameObject.transform.FindChild("objects");

            foreach (var box in objectChild.GetComponentsInChildren<BoxCollider2D>())
            {
                objectBounds.Add(box.bounds);
            }
        }

        private void loadObjectStringList()
        {
            objectStrings = new List<string>();
            objectStrings.Add("You found the chest!");
            objectStrings.Add("You are standing on the portal!");
        }

        private void loadSpawn(GameObject tileMapGameObject)
        {
            Transform spawnChild = tileMapGameObject.transform.FindChild("spawn");
            spawnBounds = spawnChild.GetComponentsInChildren<BoxCollider2D>().FirstOrDefault().bounds;
        }

        private void loadCollisionRectListFromPrefab(GameObject tileMapGameObject)
        {
            Transform collisionChild = tileMapGameObject.transform.FindChild("collision");

            foreach (var box in collisionChild.GetComponentsInChildren<BoxCollider2D>())
            {
                collisionBoundsList.Add(box.bounds);
            }
        }

        public bool checkCollision(Bounds testBounds)
        {
            foreach (var b in collisionBoundsList)
            {
                if (b.Intersects(testBounds))
                {
                    return true;
                }
            }
            return false;
        }

        public string checkObjectCollision(Bounds testBounds)
        {
            for(int i=0;i<objectBounds.Count;i++)
            {
                if (objectBounds[i].Intersects(testBounds))
                {
                    return objectStrings[i];
                }
            }
            return null;
        }

    }
}
