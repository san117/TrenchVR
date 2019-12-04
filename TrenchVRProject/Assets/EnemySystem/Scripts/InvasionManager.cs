using UnityEngine;
using System.Collections;

namespace EnemySystem
{
    public class InvasionManager : MonoBehaviour
    {
        private WayPoint[] waypoints;

        public WayPoint GetClosestWaypoint(Vector3 pos)
        {
            float closestDistance = float.PositiveInfinity;
            int closestIndex = 0;

            if(waypoints == null)
                waypoints = FindObjectsOfType<WayPoint>();

            for (int i = 0; i < waypoints.Length; i++)
            {
                if (!waypoints[i].isOccupied)
                {
                    var distance = Vector3.Distance(pos, waypoints[i].transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestIndex = i;
                    }
                }
            }

            return waypoints[closestIndex];
        }

        public WayPoint GetClosestWaypoint(WayPoint current)
        {
            foreach (var waypoint in current.neighbors)
            {
                if (!waypoint.isOccupied)
                {
                    return waypoint;
                }
            }

            return current;
        }
    }

}