using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using CasseBrique.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    public class GameController
    {
        public int currentLevel { get; private set; } = 1;
        public int maxLevel { get; private set; } = 0;
        public int score { get; private set; } = 0; //replace with nb of days? 

        public List<Resource> Resources; // replace with resources // start with individuals, then add list

        public int[] ResourceTable;


        public int lifes { get; private set; } = 3; // replace with current level life - OR with FOOD, but replace also BallOut Method! 

        public GameController()
        {
            ServicesLocator.Register<GameController>(this);
            maxLevel = CountLevels();
            ResourceTable = new int[5] { 1, 3, 0, 0, 0 }; // arrive pas à l'afficher...? 
        }

        public void Reset()
        {
            currentLevel = 1;
            Resources.Clear();
            lifes = 3;
            ResourceTable = new int[5] { 0, 0, 0, 0, 0 };  // ATTENTION, utiliser resourceTable[0] pour acceder au premier item de la liste
            score = 0;
        }

        public void MoveToNextLevel()
        {
            if (currentLevel < maxLevel)
            {
                currentLevel++;
            }
            // add game completion condition
        }

        public void BallOut()
        {
            lifes--;
            // add change Scene (retour au village) if currentLevelLifes <= 0
        }

        public void GainResource(Resource resource,int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                Resources.Add(resource); // Liste ou Tableau??? 
            }
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
