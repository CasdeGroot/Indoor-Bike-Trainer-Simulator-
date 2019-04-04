using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace VRController
{
    public class VRRoute
    {
        private string routeID;
        private readonly VRConnector vr;

        public VRRoute(VRConnector vr)
        {
            this.vr = vr;
            routeID = null;
            routeNodes = new List<Dictionary<string, object>>();
        }

        public List<Dictionary<string, object>> routeNodes { get; }


        /// <summary>
        ///     create a route
        /// </summary>
        /// <param name="standardRoute">
        ///     boolean standardRoute : if true a standerd route gets created and send instead of the given
        ///     route
        /// </param>
        /// <param name="nodes">List<Dictionary<string, object>> nodes all the nodes the route is layed out over</param>
        public void createRoute(List<Dictionary<string, object>> nodes)
        {
            string id = null;
            while (id == null)
                id = createRoutew(nodes);
            routeID = id;
        }

        public string createRoutew(List<Dictionary<string, object>> nodes)
        {
            if (nodes == null)
            {
                nodes = new List<Dictionary<string, object>>();
                // dictionaries with one position and direction
                var dict1 = new Dictionary<string, object>();
                dict1.Add("pos", new List<int> {0, 0, 0});
                dict1.Add("dir", new List<int> {10, 0, -10});
                var dict2 = new Dictionary<string, object>();
                dict2.Add("pos", new List<int> {100, 0, 0});
                dict2.Add("dir", new List<int> {10, 0, 10});
                var dict3 = new Dictionary<string, object>();
                dict3.Add("pos", new List<int> {100, 0, 100});
                dict3.Add("dir", new List<int> {-10, 0, 10});
                var dict4 = new Dictionary<string, object>();
                dict4.Add("pos", new List<int> {0, 0, 100});
                dict4.Add("dir", new List<int> {-10, 0, -10});
                nodes.Add(dict1);
                nodes.Add(dict2);
                nodes.Add(dict3);
                nodes.Add(dict4);
            }
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new {dest = vr.tunnelID, data = new {id = "route/add", data = new {nodes}}}
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);

            var response = vr.dataChecker();
            Console.WriteLine("RESONSE ROAD CREATION " + response);
            var keys = new List<string> {"data.data.data.uuid"};
            var uuidDict = vr.parseDataDict(response, keys);
            routeID = string.Empty;
            uuidDict.TryGetValue(keys.ElementAt(0), out routeID);
            return routeID;
        }

        public void addRoutePoint(int pos1, int pos2, int pos3, int dir1, int dir2, int dir3)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("pos", new List<int> {pos1, pos2, pos3});
            dict.Add("dir", new List<int> {dir1, dir2, dir3});

            routeNodes.Add(dict);
        }

        /// <summary>
        ///     create a road
        /// </summary>
        public void createRoad()
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data = new {id = "scene/road/add", data = new {route = routeID}}
                    }
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }


        /// <summary>
        ///     METHOD FOR LETTING A NODE FOLLOW A ROUTE
        /// </summary>
        /// <param name="nodename"> PARAMTER NAME IS THE NAME OF THE NODE </param>
        /// <param name="speed"> PARAMETER SPEED IS THE SPEED OF THE NODE FOLLOWING THE ROUTE </param>
        /// <param name="offset"> PARAMETER OFFSET IS THE OFFSET OF THE NODE ON THE ROUTE  </param>
        /// <param name="rotate"> PARAMETER ROTATE IS THE ROTATION OF THE NODE ON THE ROUTE IN NONE, XY OR XYZ </param>
        /// <param name="followHeight"> PARAMETER FOLLOWHEIGHT INDICATES IF THERE ARE HEIGHT DIFFERENCES ON THE ROUTE </param>
        /// <param name="rotateOffset"> PARAMETER ROTATEOFFSET IS THE OFFSET OF THE ROTATION ON THE ROUTE </param>
        /// <param name="positionOffset"> PARAMTER POSITIONOFFSET IS THE OFFSET OF THE LOCATION ON THE ROUTE</param>
        public void nodeFollow(
            string nodeID,
            double speed,
            double offset,
            string rotate,
            int[] rotateOffset,
            int[] positionOffset)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data =
                        new
                        {
                            id = "route/follow",
                            data =
                            new
                            {
                                route = routeID,
                                node = nodeID,
                                speed,
                                offset,
                                rotate,
                                followHeight = true,
                                rotateOffset,
                                positionOffset
                            }
                        }
                    }
                };

            string data = JsonConvert.SerializeObject(packet);
            vr.sendData(data);
            vr.dataChecker();
        }


        /// <summary>
        ///     set the speed of the route
        /// </summary>
        /// <param name="nodeID">string node : UUID</param>
        /// <param name="speed">double speed</param>
        public void routeSpeed(string nodeID, double speed)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data =
                        new
                        {
                            id = "route/follow/speed",
                            data =
                            new
                            {
                                node = nodeID,
                                speed
                            }
                        }
                    }
                };

            string data = JsonConvert.SerializeObject(packet);
            vr.sendData(data);
            vr.dataChecker();
        }
    }
}