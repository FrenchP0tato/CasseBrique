
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using CasseBrique.GameObjects;


namespace CasseBrique
{
    public class GameController
    {
        //Constantes à modifier également dans le reset:
        
        public float PaddleSpeed { get; set; } = 500f;
        public Vector2 PaddleSizeMult { get; set; } = new Vector2 (3,1);

        public float BallSpeed { get; set; } = 300f;
        public int MaxLifes { get; set; } = 3;
        public int FoodConsumption { get; set; } = 2;

        //variables 
        public int currentLevel { get; private set; } = 1;
        public List<Brique> CurrentBricksList;
        public int maxLevel { get; private set; } = 0;
        public int days { get; set; } = 1;
        public List<Resource> ListResources; 
        public bool LevelStarted = false;

        
        public int currentLifes { get; private set; } = 0; // replace with current level life - OR with FOOD, but replace also BallOut Method! 


        public GameController()
        {
            ServicesLocator.Register<GameController>(this);
            maxLevel = CountLevels();
            ListResources = new List<Resource>();
            currentLifes = MaxLifes;
        }

        public void Reset()
        {
            currentLevel = 1;
            ListResources.Clear();
            GainResource("Food", 4); 
            MaxLifes = 3; 
            currentLifes = MaxLifes;
            days = 1;
            LevelStarted = false;
            CurrentBricksList.Clear();
            PaddleSpeed = 500f;
            BallSpeed = 500f;
            PaddleSizeMult= new Vector2(3, 1);
            FoodConsumption= 2;
        }

        public void MoveToNextLevel()
        {
            if (currentLevel < maxLevel)
            {
                currentLevel++;
                LevelStarted = false;
                currentLifes =MaxLifes;
            }
            // add game completion condition
        }

        public void GameOver()
        { 
        // add number of days survived,create GameOverScene
        }

        public bool MoveToNextDay()
        {
            if (GetResourceQty("Food") >= FoodConsumption)
            {
                //LooseResource("Food", FoodConsumption);
                LooseResourceQty("Food",FoodConsumption);
                days++;
                return true;
            }
            else return false;
            // add game completion condition
        }

        public void BallOut()
        {
            currentLifes--;
            // add change Scene (retour au village) if currentLevelLifes <= 0
        }

        public void GainResource(string pResourceType,int pQuantity)
        {
            if (ResourceData.Data.ContainsKey(pResourceType))

            { for (int i=1;i<=pQuantity;i++)
                {
                    Resource resource = new Resource(ResourceData.Data[pResourceType]);
                    ListResources.Add(resource);  
                }
            }
        }
/*
        public void LooseResource(string pResourceType,int pQuantity)
        {
            for (int i = 1; i <= pQuantity; i++)
            {
                var resourceToRemove=ListResources.Where(r=>r.Type==pResourceType);
                ListResources.Remove(resourceToRemove);
            }
        }*/


        public void LooseResourceQty(string type, int pQuantity)
        { 
            int count= 0;
                foreach (Resource resource in ListResources)
            { 
             if (count > pQuantity) return;
             if (resource.Type == type)
                {
                    ListResources.Remove(resource);
                    count++;
                }
            }
        }


        //public List<Resource>GetResourceList() { return ListResources; }

        public int GetResourceQty(string type)
        {
            int count = 0;
            foreach (Resource resource in ListResources)
            { 
                if (resource.Type == type)
                    count= count+1;
            }
            return count;
        }
       


        public int[,] GetBricksLayout()
        {
            string filePath = $"Levels/Level{currentLevel}.txt";
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while((line=sr.ReadLine()) != null) 
                {
                    lines.Add(line); 
                }
            }

            int rows=lines.Count; 
            int columns = lines[0].Length;
            int[,] bricksLayout = new int[columns, rows];

            for (int row = 0; row < rows; row++)
                for (int column = 0; column < columns; column++)
                {
                    if (lines[row][column] == '0') bricksLayout[column,row] = 0;
                    if (lines[row][column] == '1') bricksLayout[column, row] = 1;
                    if (lines[row][column] == '2') bricksLayout[column, row] = 2;
                    if (lines[row][column] == '3') bricksLayout[column, row] = 3;
                    if (lines[row][column] == '4') bricksLayout[column, row] = 4;
                    if (lines[row][column] == '5') bricksLayout[column, row] = 5;
                    //bricksLayout[column, row] = lines[row][column] =='1' ? 1:0; (rappel: Synthaxe condition ternaire) 
                }
            return bricksLayout;
        }

        private int CountLevels()
        {
            string path = "Levels";
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "Level*.txt");
                return files.Length;
            }
            else
            {
                throw new DirectoryNotFoundException($"The directory with {path} doesn't exist)");
            }
        }

        public string GetLevel()
        {
            string level = $"Level{currentLevel}";
            return level;

        }
    }
}
