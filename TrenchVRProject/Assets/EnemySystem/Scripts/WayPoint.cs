using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public class WayPoint : MonoBehaviour
    {
        public bool isProtected;
        public bool isOccupied;

        public List<WayPoint> neighbors = new List<WayPoint>();

        private void OnDrawGizmos()
        {
            if (neighbors.Count == 0)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(transform.position + Vector3.up * 2, Vector3.one / 2);
            }

            foreach (var neighbor in neighbors)
            {
                Gizmos.color = Color.green;

                foreach (var otherNeighbor in neighbor.neighbors)
                {
                    if (otherNeighbor.Equals(this))
                    {
                        Gizmos.color = Color.red;
                        break;
                    }
                }

                Gizmos.DrawLine(transform.position, neighbor.transform.position);

                Gizmos.DrawCube(neighbor.transform.position, Vector3.one / 2);
            }
        }
    }
}
