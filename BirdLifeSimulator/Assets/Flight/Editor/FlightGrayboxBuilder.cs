using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BirdLife.Flight.Editor
{
    // One-time authoring tool. The saved scene, not this recipe, owns subsequent layout edits.
    public static class FlightGrayboxBuilder
    {
        private const string ScenePath = "Assets/Scenes/FlightPrototype.unity";
        private const string MaterialFolder = "Assets/Flight/Materials";

        [MenuItem("BirdLife/Create Static Flight Graybox")]
        public static void Create()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
                throw new InvalidOperationException("Create the graybox only in stable Edit Mode.");
            if (File.Exists(ScenePath) || AssetDatabase.IsValidFolder(MaterialFolder))
                throw new InvalidOperationException("Graybox output already exists. Edit the saved scene; generation will not overwrite it.");
            for (int i = 0; i < SceneManager.sceneCount; i++)
                if (SceneManager.GetSceneAt(i).isDirty)
                    throw new InvalidOperationException("Save or resolve unsaved scene changes before creating the graybox.");

            var shader = Shader.Find("Universal Render Pipeline/Lit");
            if (!shader) throw new InvalidOperationException("The installed URP/Lit shader is unavailable.");
            AssetDatabase.CreateFolder("Assets/Flight", "Materials");
            var ground = Material("Ground", new Color(.34f, .36f, .31f), shader);
            var clearing = Material("Clearing", new Color(.55f, .55f, .47f), shader);
            var foliage = Material("Foliage", new Color(.30f, .36f, .32f), shader);
            var wood = Material("Wood", new Color(.36f, .30f, .25f), shader);
            var stone = Material("Stone", new Color(.47f, .49f, .50f), shader);
            var marker = Material("Markers", new Color(.72f, .59f, .28f), shader);

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var environment = Group("WoodlandGraybox", null);
            var terrain = Group("GroundAndClearing", environment);
            Shape("Ground_60x60m", PrimitiveType.Cube, terrain, new Vector3(0,-.2f,0), new Vector3(60,.4f,60), ground);
            Shape("Clearing_20x20m", PrimitiveType.Cube, terrain, new Vector3(0,.01f,0), new Vector3(20,.02f,20), clearing, false);

            var trees = Group("Trees", environment);
            Vector3[] positions = {
                new Vector3(-23,0,-22), new Vector3(-22,0,-8), new Vector3(-24,0,16),
                new Vector3(-12,0,24), new Vector3(3,0,25), new Vector3(22,0,22),
                new Vector3(23,0,1), new Vector3(22,0,-22)
            };
            for (int i = 0; i < positions.Length; i++)
                Tree("Tree_" + (i+1).ToString("00"), trees, positions[i], i == 3 ? 9 : 6 + i%3, wood, foliage);
            var shrubs = Group("Shrubs", environment);
            Vector3[] shrubPositions = {
                new Vector3(-18,0,-16), new Vector3(-23,0,7), new Vector3(-16,0,20),
                new Vector3(10,0,24), new Vector3(24,0,12), new Vector3(18,0,-15)
            };
            for (int i = 0; i < shrubPositions.Length; i++)
                Shape("Shrub_"+(i+1).ToString("00"), PrimitiveType.Sphere, shrubs,
                    shrubPositions[i]+Vector3.up*.65f, new Vector3(2.8f,1.3f,2.2f), foliage);

            var landmarks = Group("Landmarks", environment);
            Tree("LargeTreeLandmark", landmarks, new Vector3(-18,0,10), 10, wood, foliage);
            Shape("StandingStoneLandmark", PrimitiveType.Cube, landmarks, new Vector3(18,2.5f,17), new Vector3(1.5f,5,1.5f), stone);
            var obstacles = Group("Obstacles", environment);
            Shape("Boulder", PrimitiveType.Sphere, obstacles, new Vector3(-8,1,13), new Vector3(3,2,3), stone);
            Shape("FallenLog", PrimitiveType.Cube, obstacles, new Vector3(11,.4f,-7), new Vector3(5,.8f,1.4f), wood);

            var refuge = Group("StartingRefuge", environment);
            refuge.localPosition = new Vector3(0,0,-18);
            Shape("RefugeLandingSurface", PrimitiveType.Cube, refuge, new Vector3(0,.2f,0), new Vector3(4,.4f,3), wood);
            var nest = Group("NestPlaceholder", refuge);
            nest.localPosition = new Vector3(0,.4f,0);
            Shape("NestBase", PrimitiveType.Cube, nest, new Vector3(0,.05f,0), new Vector3(1.4f,.1f,1.4f), marker);
            Shape("NestLeft", PrimitiveType.Cube, nest, new Vector3(-.65f,.15f,0), new Vector3(.1f,.2f,1.4f), wood);
            Shape("NestRight", PrimitiveType.Cube, nest, new Vector3(.65f,.15f,0), new Vector3(.1f,.2f,1.4f), wood);
            Shape("NestBack", PrimitiveType.Cube, nest, new Vector3(0,.15f,-.65f), new Vector3(1.2f,.2f,.1f), wood);
            Group("FutureBirdStart", refuge).localPosition = new Vector3(0,.4f,1);

            var rests = Group("RestingSurfaces", environment);
            Shape("RestStump", PrimitiveType.Cylinder, rests, new Vector3(-14,.6f,1), new Vector3(2,.6f,2), wood);
            Shape("RestRockShelf", PrimitiveType.Cube, rests, new Vector3(15,1,8), new Vector3(3,2,2), stone);
            var foods = Group("FoodLocations", environment);
            Vector3[] foodPositions = {new Vector3(-13,0,-7), new Vector3(12,0,13), new Vector3(-9,0,20)};
            for (int i=0; i<foodPositions.Length; i++)
            {
                var location = Group("FoodLocation_"+(i+1).ToString("00"), foods);
                location.localPosition = foodPositions[i];
                Shape("FeedingPatch", PrimitiveType.Cylinder, location, new Vector3(0,.025f,0), new Vector3(2,.025f,2), clearing, false);
                Shape("FoodPlaceholder", PrimitiveType.Cube, location, new Vector3(0,.2f,0), new Vector3(.4f,.4f,.4f), marker, false);
            }

            var bounds = Group("BoundaryMarkers_60x60x12m", environment);
            Shape("NorthEdge", PrimitiveType.Cube, bounds, new Vector3(0,.2f,29.85f), new Vector3(60,.4f,.3f), stone);
            Shape("SouthEdge", PrimitiveType.Cube, bounds, new Vector3(0,.2f,-29.85f), new Vector3(60,.4f,.3f), stone);
            Shape("EastEdge", PrimitiveType.Cube, bounds, new Vector3(29.85f,.2f,0), new Vector3(.3f,.4f,60), stone);
            Shape("WestEdge", PrimitiveType.Cube, bounds, new Vector3(-29.85f,.2f,0), new Vector3(.3f,.4f,60), stone);
            foreach (int x in new[]{-1,1}) foreach (int z in new[]{-1,1})
                Shape("HeightMarker_"+x+"_"+z, PrimitiveType.Cube, bounds, new Vector3(x*29.8f,6,z*29.8f), new Vector3(.15f,12,.15f), marker, false);

            var lightObject = new GameObject("Directional Light");
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            light.shadows = LightShadows.Soft;
            lightObject.transform.rotation = Quaternion.Euler(50,-30,0);
            RenderSettings.sun = light;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(.65f,.68f,.72f);
            RenderSettings.fog = false;
            if (!EditorSceneManager.SaveScene(scene, ScenePath))
                throw new IOException("Could not save FlightPrototype. Inspect the open scene before retrying.");
            AssetDatabase.SaveAssets();
            Selection.activeGameObject = environment.gameObject;
            if (SceneView.lastActiveSceneView)
                SceneView.lastActiveSceneView.LookAt(new Vector3(0,1,0), Quaternion.Euler(45,0,0), 48);
        }

        private static Material Material(string name, Color color, Shader shader)
        {
            var material = new Material(shader) { name = "Graybox"+name };
            material.SetColor("_BaseColor",color);
            material.SetFloat("_Smoothness",0);
            AssetDatabase.CreateAsset(material, MaterialFolder+"/"+material.name+".mat");
            return material;
        }

        private static Transform Group(string name, Transform parent)
        {
            var group = new GameObject(name).transform;
            group.SetParent(parent,false);
            return group;
        }

        private static void Shape(string name, PrimitiveType type, Transform parent,
            Vector3 position, Vector3 size, Material material, bool solid = true)
        {
            var shape = GameObject.CreatePrimitive(type);
            shape.name = name;
            shape.transform.SetParent(parent,false);
            shape.transform.localPosition = position;
            shape.transform.localScale = size;
            shape.GetComponent<Renderer>().sharedMaterial = material;
            if (!solid) UnityEngine.Object.DestroyImmediate(shape.GetComponent<Collider>());
        }

        private static void Tree(string name, Transform parent, Vector3 position, float height,
            Material wood, Material foliage)
        {
            var tree = Group(name,parent);
            tree.localPosition = position;
            Shape("Trunk", PrimitiveType.Cylinder, tree, new Vector3(0,height*.3f,0),
                new Vector3(.6f,height*.3f,.6f),wood);
            Shape("Crown", PrimitiveType.Sphere, tree, new Vector3(0,height*.7f,0),
                new Vector3(height*.6f,height*.6f,height*.6f),foliage);
        }
    }
}
