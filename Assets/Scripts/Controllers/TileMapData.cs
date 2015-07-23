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
          
            loadCollisionRectListFromPrefab(tileMapGameObject);
            loadObjectBounds(tileMapGameObject);
            loadSpawn(tileMapGameObject);
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
            //for now, default to the first object as the spawn point
            if (objectBounds.Count > 0)
            {
                spawnBounds = objectBounds[0];
            }
            /*
            Transform spawnChild = tileMapGameObject.transform.FindChild("spawn");
            spawnBounds = spawnChild.GetComponentsInChildren<BoxCollider2D>().FirstOrDefault().bounds;
             */
        }

        //spawn point is the current node location we are on, or defaults to object 1
        public Bounds getSpawnPoint(int objectIndex)
        {
            if(objectBounds.Count > objectIndex){
                return objectBounds[objectIndex];
            }
            else
            {
                return objectBounds[0];
            }
           
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

        public int checkObjectCollision(Bounds testBounds)
        {
            for(int i=0;i<objectBounds.Count;i++)
            {
                if (objectBounds[i].Intersects(testBounds))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
