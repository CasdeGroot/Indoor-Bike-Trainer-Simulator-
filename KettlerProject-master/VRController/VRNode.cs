using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VRController
{
    public class VRNode
    {
        private readonly VRConnector vr;

        public int counter;

        public VRNode(VRConnector vr)
        {
            this.vr = vr;
        }


        /// <summary>
        ///     Creates a node using the dynamic method
        ///     Booleans for component names set the optional functions.
        ///     JSONconverter from the NewtonsoftJson used to convert it to JSON.
        ///     starting with o- means its optional
        /// </summary>
        /// <param name="name">name of the node</param>
        /// <param name="parent">o- parent object</param>
        /// <param name="transform">decides if the transformation component is added.</param>
        /// <param name="position">o- array with x, y, z in doubles</param>
        /// <param name="scale">o- scaloing in double</param>
        /// <param name="rotation">o- rotation in double array over x-axis, y-axis, z-axis</param>
        /// <param name="model">boolean if true model has to be set.</param>
        /// <param name="file">name of the 3d model file (.obj)</param>
        /// <param name="cullbackfaces">must be true</param>
        /// <param name="animated">decides if its animated</param>
        /// <param name="animation">name of the animation</param>
        /// <param name="terrain">decides if node is a terrain </param>
        /// <param name="smoothnomals">always true</param>
        /// <param name="panel">defines if panel should be used</param>
        /// <param name="size">size of the panel double[] x,y</param>
        /// <param name="resolution">panel resolution, double[] x,y</param>
        /// <param name="background">background in double[] R,G,B,A</param>
        /// <param name="water">defines if the node is water</param>
        /// <param name="watersize">size of the water in double[] x,y</param>
        /// <param name="waterresolution">resolution of the water in double[] x,y</param>
        public void addNode(
            string name,
            int[] position,
            float scale,
            int[] rotation,
            string file,
            bool cullbackfaces,
            bool animated,
            string animation)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/node/add",
                            data = new
                            {
                                name,
                                components = new
                                {
                                    transform = new
                                    {
                                        position,
                                        scale,
                                        rotation
                                    },
                                    model = new
                                    {
                                        file,
                                        cullbackfaces,
                                        animated,
                                        animation
                                    }
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        public void addParentNode(
            string name,
            string parent,
            int[] position,
            float scale,
            int[] rotation,
            string file,
            bool cullbackfaces,
            bool animated,
            string animation)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/node/add",
                            data = new
                            {
                                name,
                                parent,
                                components = new
                                {
                                    transform = new
                                    {
                                        position,
                                        scale,
                                        rotation
                                    },
                                    model = new
                                    {
                                        file,
                                        cullbackfaces,
                                        animated,
                                        animation
                                    }
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }


        public void addWater(string name, int[] position, float scale, int[] rotation, int[] size, double resolution)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/node/add",
                            data = new
                            {
                                name,
                                components = new
                                {
                                    transform = new
                                    {
                                        position,
                                        scale,
                                        rotation
                                    },
                                    water = new
                                    {
                                        size,
                                        resolution
                                    }
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }


        /// <summary>
        ///     Get the UUID for a name
        /// </summary>
        /// <param name="name">string name</param>
        /// <returns>retuns a string UUID</returns>
        public string getUUID(string name)
        {
            string response = null;
            while (response == null)
                response = findNode(name);

            try
            {
                return vr.parseData(response, new List<string> {"data.data.data.components.0.uuid"}).ElementAt(0);
            }
            catch (Exception e)
            {
                try
                {
                    return vr.parseData(response, new List<string> {"data.data.data.uuid"}).ElementAt(0);
                }
                catch (Exception f)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     delete all nodes
        /// </summary>
        public void removeAllNodes()
        {
            var nodes = parseAllnodes();
            foreach (var node in nodes)
                deleteNode(node);
        }


        /// <summary>
        ///     get the position of a node
        /// </summary>
        /// <param name="name">string name : the name of the node not UUID</param>
        /// <returns>retuns the coordinats of the node</returns>
        public int[] getPosition(string name)
        {
            var response = findNode(name);
            if (response != null)
            {
                var posi = new int[2];
                try
                {
                    //Console.WriteLine("TRYING TO FIND POSITIO OF" + name);
                    var all = vr.getAll(response, "data.data.data.components.0.components.0.position.position.?",
                        new List<string> {""});
                    var x = int.Parse(all[0][0]);
                    var y = int.Parse(all[2][0]);
                    posi[0] = x;
                    posi[1] = y;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR GETTING POSITION OF BIKE " + response);
                }
                return posi;
            }
            return null;
        }


        /// <summary>
        ///     METHOD FOR FINDING ALL NODES IN THE SCENE
        /// </summary>
        public string getNodes()
        {
            dynamic packet =
                new {id = "tunnel/send", data = new {dest = vr.tunnelID, data = new {id = "scene/get"}}};

            string data = JsonConvert.SerializeObject(packet);
            vr.sendData(data);
            return vr.dataChecker();
        }


        /// <summary>
        ///     parse all nodes to strings
        /// </summary>
        /// <returns>retuns a string[] with all the nodes to string</returns>
        public string[] parseAllnodes()
        {
            var response = getNodes();


            var data = vr.getAll(response, "data.data.data.children.?", new List<string> {"name"});
            var first = new string[data.Length];
            for (var i = 0; i < first.Length; i++)
                first[i] = data[i][0];
            return first;
        }

        /// <summary>
        ///     delete a node
        /// </summary>
        /// <param name="uuid">string UUID : Not name</param>
        public void deleteNodeUUID(string uuid)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new {dest = vr.tunnelID, data = new {id = "scene/node/delete", data = new {id = uuid}}}
                };

            string data = JsonConvert.SerializeObject(packet);
            vr.sendData(data);
            vr.dataChecker();
        }

        /// <summary>
        ///     METHOD FOR DELETING A NODE
        /// </summary>
        /// <param name="name"> PARAMTER NAME IS THE NAME OF THE NODE </param>
        /// <returns> RETURNS THE JSON STRING OF THE FOUND NODE </returnS>
        public void deleteNode(string name)
        {
            var response = findNode(name);
            if (response != null)
            {
                var keys = new List<string>();
                keys.Add("data.data.data.components.uuid");
                var dict = vr.parseDataDict(response, keys);
                var id = string.Empty;
                dict.TryGetValue(keys.ElementAt(0), out id);

                deleteNodeUUID(id);
            }
        }

        /// <summary>
        ///     returns a node with same id as name
        /// </summary>
        /// <param name="name">name of the node which you want to find</param>
        public void deletePlane()
        {
            deleteNode("GroundPlane");
        }

        /// <summary>
        ///     Deletes (all) layers?
        /// </summary>
        public void delLayer()
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new {dest = vr.tunnelID, data = new {id = "scene/node/dellayer", data = new {}}}
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     METHOD FOR FINDING A NODE IN THE SCENE
        /// </summary>
        /// <param name="id"> PARAMTER NAME IS THE ID OF THE NODE </param>
        /// <returns> RETURNS THE JSON STRING OF THE FOUND NODE </returnS>
        public string findNode(string id)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new {dest = vr.tunnelID, data = new {id = "scene/node/find", data = new {name = id}}}
                };

            string dataPacket = JsonConvert.SerializeObject(packet);
            vr.sendData(dataPacket);
            var response = vr.dataChecker();
            if ((response == null) || ((response == "") && (counter > 50)))
            {
                counter++;
                return findNode(id);
            }
            if (counter > 50)
            {
                MessageBox.Show("Communication between PatientClient and VREngine is invalid", "Error",
                    MessageBoxButtons.OK);
                Environment.Exit(0);
            }
            counter = 0;
            return response;
        }

        /// <summary>
        ///     moves a node for a certain time or speed
        /// </summary>
        /// <param name="id">name of the node</param>
        /// <param name="stop">type stop if node needs to stop (else null)</param>
        /// <param name="position">int[] of x,y,z</param>
        /// <param name="rotate">int[] of rotation</param>
        /// <param name="interpolate">should be linear </param>
        /// <param name="followheight">should height be the same as following</param>
        /// <param name="speed">int speed</param>
        /// <param name="time">time the movement should take</param>
        public void moveNode(
            string id,
            string stop,
            double[] position,
            string rotate,
            string interpolate,
            bool followheight,
            double speed,
            double time)
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
                            id = "scene/node/moveto",
                            data = new
                            {
                                id,
                                stop,
                                position,
                                rotate,
                                interpolate,
                                followHeight = followheight,
                                speed,
                                time
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Updates an already created node
        ///     Only ID is required
        /// </summary>
        /// <param name="id">id of the node to move</param>
        /// <param name="position">o- array with x, y, z in doubles</param>
        /// <param name="scale">o- scaloing in double</param>
        /// <param name="rotation">o- rotation in double array over x-axis, y-axis, z-axis</param>
        /// <param name="name">name of the animation</param>
        /// <param name="speed">speed of the animation</param>
        public void updateNode(
            string id,
            int[] position,
            float scale,
            int[] rotation,
            string name,
            double speed)
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
                            id = "scene/node/update",
                            data = new
                            {
                                id,
                                transform = new
                                {
                                    position,
                                    scale,
                                    rotation
                                },
                                animation = new
                                {
                                    name,
                                    speed
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        public void updateSpeed(
            string id,
            double speed)
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
                            id = "scene/node/update",
                            data = new
                            {
                                id,
                                animation = new
                                {
                                    name = "Armature|Fietsen",
                                    speed
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        public void saveScene(string fileName)
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
                            id = "scene/save",
                            data = new
                            {
                                filename = fileName,
                                overwrite = false
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        public void loadScene(string fileName)
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
                            id = "scene/load",
                            data = new
                            {
                                filename = fileName
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Reset the scene in the VR world
        /// </summary>
        public void resetScene()
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
                            id = "scene/reset",
                            data = new
                            {
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Pause the scene in the VR world
        /// </summary>
        public void playScene()
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
                            id = "play",
                            data = new
                            {
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Pause the scene in the VR world
        /// </summary>
        public void pauseScene()
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
                            id = "pause",
                            data = new
                            {
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }
    }
}