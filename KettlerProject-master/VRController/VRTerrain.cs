using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;

namespace VRController
{
    public class VRTerrain
    {
        private readonly double length;
        private readonly VRConnector vr;
        private readonly double width;


        /// <summary>
        ///     constructor of VRTerrain
        /// </summary>
        /// <param name="vr">VRConnector vr</param>
        /// <param name="node">VRNode node</param>
        /// <param name="width">int width : the width of the map !must be the same width as heightPath.width() </param>
        /// <param name="length">int length : the height of the map !must be the same width as heightPath.height()</param>
        /// <param name="heightPath">
        ///     string heightPath : the filepath to the image from where the height of the map can be
        ///     determent
        /// </param>
        /// <param name="terrainPath">string terainPath : the filepath to the terrain file</param>
        /// <param name="normalPath">string normalPath : the filepath to the the normal terein texture</param>
        /// <param name="minHeight">int minHeight : the minimum height from where the texture should be applied</param>
        /// <param name="maxHeight">int maxHeight : the maximum height from where the texture should be applied</param>
        public VRTerrain(VRConnector vr, VRNode node, int width, int length, string heightPath, string terrainPath,
            string normalPath, int minHeight, int maxHeight)
        {
            node.deletePlane();
            this.vr = vr;
            this.width = width;
            this.length = length;
            addTerrain(generateHeights(heightPath));
            addTerrainRenderer();


            //Thread.Sleep(2000);
            //Console.WriteLine(string.Join(",",node.parseAllnodes()));
            //Thread.Sleep(2000);
            addLayer(node.getUUID("terrainrenderer"), terrainPath, normalPath, minHeight, maxHeight);
        }

        /// <summary>
        ///     Add terrain to the VR world
        /// </summary>
        /// <param name="heights">Lit<foat> heights : for every point on the map a height value.</param>
        public void addTerrain(List<float> heights)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/terrain/add",
                            data = new
                            {
                                size = new[] {width, length},
                                heights
                            }
                        }
                    }
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            //Console.WriteLine(this.vr.dataChecker());
            vr.dataChecker();
        }

        /// <summary>
        ///     get all the heights from a black and white image and put them in a list
        /// </summary>
        /// <param name="path">string path : where the image can be found</param>
        /// <returns>List<float> a list with all the image read heights</returns>
        public List<float> generateHeights(string path)
        {
            //Console.WriteLine("PATH " + path);
            var heights = new List<float>();
            try
            {
                var bitmap = new Bitmap(path);
                var index = 0;
                for (var y = 0; y < length; y++)
                    for (var x = 0; x < width; x++)
                    {
                        var color = bitmap.GetPixel(x, y);
                        heights.Add(color.R/10.0f);
                        index++;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return heights;
        }

        /// <summary>
        ///     send to the VR world a terain renderer with hardcoded data about position, scale, rotation and smoothnormols
        /// </summary>
        public void addTerrainRenderer()
        {
            int[] position = {-127, 0, -127};
            int[] rotation = {0, 0, 0};
            var t = "true";
            dynamic packet = new
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
                            name = "terrainrenderer",
                            components = new
                            {
                                transform = new
                                {
                                    position,
                                    scale = 1,
                                    rotation
                                },
                                terrain = new
                                {
                                    smoothnormals = t
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
        ///     Adds a texture on top of the terein within specific heights
        /// </summary>
        /// <param name="uuid">string UUID</param>
        /// <param name="path">string path : the path of the different texture</param>
        /// <param name="path2">string path2 : the path of the normal texture</param>
        /// <param name="minHeight">int minHeight : the minimum height from where path1 texture should be applied</param>
        /// <param name="maxHeight">int maxheight : the maximum height to where path1 texture should be applied</param>
        public void addLayer(string uuid, string path, string path2, int minHeight, int maxHeight)
        {
            dynamic packet = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = vr.tunnelID,
                    data = new
                    {
                        id = "scene/node/addlayer",
                        data = new
                        {
                            id = uuid,
                            diffuse = path,
                            normal = path2,
                            minHeight,
                            maxHeight,
                            fadeDist = 10.0f
                        }
                    }
                }
            };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Get the height of a specific point in the VR world
        /// </summary>
        /// <param name="x">int x : the x coordinant</param>
        /// <param name="y">int y : the y coordinant</param>
        /// <returns> returns the height of the specified position in the VR world</returns>
        public int getHeight(int x, int y)
        {
            var height = 0;
            double[] position = {x, y};

            dynamic packet = new
            {
                id = "tunnel/send",
                data = new
                {
                    dest = vr.tunnelID,
                    data = new
                    {
                        id = "scene/terrain/getheight",
                        data = new
                        {
                            position
                        }
                    }
                }
            };
            string packetString2 = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString2);
            var response = vr.dataChecker();
            try
            {
                var values = vr.parseData(response, new List<string> {"data.data.data.height"}).ElementAt(0);

                var h = Convert.ToDouble(values);
                height = (int) h;
            }
            catch (Exception e)
            {
                Console.WriteLine("HEIGHT DIEDED " + response);
            }

            return height;
        }
    }
}