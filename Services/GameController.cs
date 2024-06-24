
using System.Collections.Generic;
using System.IO;

using System.Numerics;

using CasseBrique.GameObjects;
using CasseBrique.Scenes;


namespace CasseBrique
{
    public class GameController
    {
        //Constantes à modifier également dans le reset:
        
        public float PaddleSpeed { get; set; } = 500f;
        public int PaddleSize { get; set; } = 50;

        public float BallSpeed { get; set; } = 300f;

        public int BallDamage { get; set; } = 1;

        public int MaxLifes { get; set; } = 3;
        public int FoodConsumption { get; set; } = 2;

        //variables 
        public int currentLevel { get; private set; } = 1;
        public List<Brique> CurrentBricksList;
        public int maxLevel { get; private set; } = 0;
        public int days { get; set; } = 1;
        public List<Resource> ListResources; 
        public bool LevelStarted = false;
        public bool GameStarted = false;


        public int currentLifes { get; set; } = 0; 


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
            BallSpeed = 300f;
            PaddleSize = 50;
            FoodConsumption= 2;
            BallDamage = 1;
            GameStarted = false;
        }

        public void MoveToNextLevel()
        {
            if (currentLevel < maxLevel)
            {
                currentLevel++;
                LevelStarted = false;
                currentLifes =MaxLifes;
                FoodConsumption += 1;
                BallSpeed += 30;
            }
            // add game completion condition
        }

        public void GameOver()
        {
            ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGameOver>();
        }

        public bool MoveToNextDay()
        {
            if (GetResourceQty("Food") >= FoodConsumption)
            {
                LooseResource("Food", FoodConsumption);
                days++;
                return true;
            }
            else return false;
            // add game completion condition
        }

        public void BallOut()
        {
            currentLifes--;
            if (currentLifes == 0)
            {
                if (GetResourceQty("Food") < FoodConsumption) GameOver();
                else               
                ServicesLocator.Get<IScenesManager>().ChangeScene<SceneVillage>();
            }
            
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

        public void LooseResource(string pResourceType,int pQuantity)
        {
            var typeOfResource = pResourceType;
            var amount = pQuantity;

            for (int i=ListResources.Count-1;i>=0;i--)
            {
                var item = ListResources[i];
                if(item.Type==typeOfResource)
                {
                    ListResources.Remove(item);
                    amount--;
                }
                if (amount <= 0) break;

            }
        }

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
