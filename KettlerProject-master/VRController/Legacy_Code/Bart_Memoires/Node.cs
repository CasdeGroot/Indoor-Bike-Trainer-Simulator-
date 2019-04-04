/*namespace VRController
{
    class Node : VRConnector
    {
        private String parent { get; set; }
        public Node(String parent) : base()
        {
            this.parent = parent;
        }



        /// <summary>
        /// creates a new node with the given parameters.
        /// Parameter parent can be null
        /// if no animation please set animated false
        /// </summary>
        /// <param name="name">name of the node</param>
        /// <param name="x">x-value of the posistion</param>
        /// <param name="y">y-value of the posistion</param>
        /// <param name="z">z-value of the posistion</param>
        /// <param name="scale">scaling applied to the object</param>
        /// <param name="r1">x-value of rotation</param>
        /// <param name="r2">y-value of rotation</param>
        /// <param name="r3">z-value of rotation</param>
        /// <param name="file">name of the file(filepath)</param>
        /// <param name="cullbackfaces">indicates the backfaces are not being drawn </param>
        /// <param name="animated">indicates the animation should be loaded </param>
        /// <param name="animation">default location of the annimation</param>
        /// <param name="smoothnormals">indicates terrain renderer</param>
        /// <param name="sx">x-value of the panel size</param>
        /// <param name="sy">y value of the panel size</param>
        /// <param name="rx">x resolution of the panel</param>
        /// <param name="ry">y resoultion of the panel</param>
        /// <param name="b1">background value r, should be smaller or equal 1 and >= 0</param>
        /// <param name="b2">background value g</param>
        /// <param name="b3">background value b</param>
        /// <param name="b4">background value transparncy</param>
        /// <param name="isPanel">indicats that node is/has a panel</param>
        /// <param name="isTerrain">indicates that node is a terrain renderer</param>
        public void addNode(string name, int x, int y, int z, int scale, int r1, int r2, int r3, String file, Boolean cullbackfaces, Boolean animated, string animation, Boolean smoothnormals, int sx, int sy, int rx, int ry, double b1, double b2, double b3, double b4, Boolean isPanel, Boolean isTerrain)
        {

            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/node/add");

            if (true)
            {
                Dictionary<string, object> nodeCommands = new Dictionary<string, object>();
                nodeCommands.Add("name", name);
                nodeCommands.Add("parent", parent);

                if (true)
                {
                    Dictionary<string, object> nodeComponents = new Dictionary<string, object>();

                    if (true)
                    {
                        Dictionary<string, object> nodeTransform = new Dictionary<string, object>();
                        nodeTransform.Add("position", new int[] { x, y, z });
                        nodeTransform.Add("scale", scale);
                        nodeTransform.Add("rotation", new int[] { r1, r2, r3 });
                        nodeComponents.Add("transform", nodeTransform);
                    }

                    if (true)
                    {
                        Dictionary<string, object> nodeModel = new Dictionary<string, object>();
                        nodeModel.Add("file", file);
                        nodeModel.Add("cullbackfaces", cullbackfaces);
                        nodeModel.Add("animated", animated);
                        nodeModel.Add("animation", animation);
                        nodeComponents.Add("model", nodeModel);
                    }

                    if (isTerrain)
                    {
                        Dictionary<string, object> nodeTerrain = new Dictionary<string, object>();
                        nodeTerrain.Add("smoothnormals", smoothnormals);
                        nodeComponents.Add("terrain", nodeTerrain);
                    }

                    if (isPanel)
                    {
                        Dictionary<string, object> nodePanel = new Dictionary<string, object>();
                        nodePanel.Add("size", new int[] { sx, sy });
                        nodePanel.Add("resolution", new int[] { rx, ry });
                        nodePanel.Add("background", new double[] { b1, b2, b3, b4 });
                        nodeComponents.Add("panel", nodePanel);
                    }
                    nodeCommands.Add("components", nodeComponents);

                }
                mainDict.Add("data", nodeCommands);
            }
            sendTunnelData(mainDict);
            write("node created");

        }
        public void addNode(string name, int x, int y, int z, int scale, int r1, int r2, int r3, String file)
        {

            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/node/add");
            write("started");
            if (true)
            {
                Dictionary<string, object> nodeData = new Dictionary<string, object>();
                nodeData.Add("name", name);
                
                if (true)
                {
                    Dictionary<string, object> nodeComponents = new Dictionary<string, object>();

                    if (true)
                    {
                        Dictionary<string, object> nodeTransform = new Dictionary<string, object>();
                        List<int> position = new List<int>();
                        position.Add(x);
                        position.Add(y);
                        position.Add(z);

                        nodeTransform.Add("position", position);
                        nodeTransform.Add("scale", scale);
                        List<int> rotation = new List<int>();
                        position.Add(r1);
                        position.Add(r2);
                        position.Add(r3);
                        nodeTransform.Add("rotation", rotation);
                        nodeComponents.Add("transform", nodeTransform);
                    }

                    if (true)
                    {
                        Dictionary<string, object> nodeModel = new Dictionary<string, object>();
                        nodeModel.Add("file", file);
                        nodeComponents.Add("model", nodeModel);
                    }
                    nodeData.Add("components", nodeComponents);
                    
                }
                mainDict.Add("data", nodeData);
                write(mainDict.ToString());
                sendTunnelData(mainDict);
            }
           

            write("node created");

        }

        /// <summary>
        /// Used to update the node. ID is mandatory rest is optional
        /// Note this is not the same as moving it will change the position at once.
        /// </summary>
        /// <param name="id">node that should be updated</param>
        /// <param name="x">new x</param>
        /// <param name="y">new y</param>
        /// <param name="z">new z </param>
        /// <param name="scale">new scaling</param>
        /// <param name="r1">new x rotation</param>
        /// <param name="r2">new y rotation</param>
        /// <param name="r3">new z rotation</param>
        /// <param name="name">name of animation</param>
        /// <param name="speed">speed of animation</param>
        /// <param name="isAnimated">animation is only used when true</param>
        public void updateNode(string id, int x, int y, int z, int scale, int r1, int r2, int r3, String name, double speed, Boolean isAnimated)
        {
            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/node/update");

            if (true)
            {
                Dictionary<string, object> nodeCommands = new Dictionary<string, object>();
                nodeCommands.Add("id", id);
                nodeCommands.Add("parent", parent);

                if (true)
                {
                    Dictionary<string, object> nodeComponents = new Dictionary<string, object>();

                    if (true)
                    {
                        Dictionary<string, object> nodeTransform = new Dictionary<string, object>();
                        nodeTransform.Add("position", new List<int> { x, y, z });
                        nodeTransform.Add("scale", scale);
                        nodeTransform.Add("rotation", new List<int> { r1, r2, r3 });
                        nodeComponents.Add("transform", nodeTransform);
                    }

                    if (isAnimated)
                    {
                        Dictionary<string, object> nodeAnimation = new Dictionary<string, object>();
                        nodeAnimation.Add("name", name);
                        nodeAnimation.Add("speed", speed);
                        nodeComponents.Add("animation", nodeAnimation);
                    }
                }
                mainDict.Add("data", nodeCommands);
            }
            sendTunnelData(mainDict);
        }

        /// <summary>
        /// moves node with ceratin speed.
        /// If time or speed is not set the node will stop.
        /// if stop is true there is no callback
        /// </summary>
        /// <param name="name">node id</param>
        /// <param name="stop">should the node stop whitout callback</param>
        /// <param name="x">x to move to</param>
        /// <param name="y">y to move to</param>
        /// <param name="z">z to move to</param>
        /// <param name="rotation">rotation to move to in "XY" string</param>
        /// <param name="interpolate">linear is defaut.</param>
        /// <param name="followheight">should the followheight be changed</param>
        /// <param name="speed">set speed of movement</param>
        /// <param name="time">time moving</param>
        public void moveNode(string name, Boolean stop, int x, int y, int z, string rotation, string interpolate, Boolean followheight, double speed, int time)
        {
            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/node/add");
            Dictionary<string, object> nodeComponents = new Dictionary<string, object>();
            nodeComponents.Add("id", name);
            if (stop)
            {
                nodeComponents.Add("stop", "stop");
            }
            nodeComponents.Add("position", new int[] { x, y, z });
            nodeComponents.Add("rotate", rotation);
            nodeComponents.Add("interpolate", interpolate);
            nodeComponents.Add("followheight", followheight);
            nodeComponents.Add("speed", speed);
            nodeComponents.Add("time", time);
            mainDict.Add("data", nodeComponents);
        }
    }
}*/

