using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSimuation
{
    class Forest
    {

        private int rowsize, colsize;
        private string rowOutput;
        private char[,] grid;
        private List<Tuple<int, int>> burningTrees;
        private List<Tuple<int, int>> burntTrees;
        public Forest(int rs, int cs)
        {
            rowsize = rs;
            colsize = cs;
            rowOutput = "";
            grid = new char[rowsize, colsize];
            burningTrees = new List<Tuple<int, int>>();
            burntTrees = new List<Tuple<int, int>>();
            for (int row = 0; row < rowsize; row++)
            {
                for (int col = 0; col < colsize; col++)
                {
                    grid[row, col] = '&';
                }
            }
        }
        private Boolean burnProbability()
        {
            Random rd = new Random(); //generate random number 
            double result = rd.NextDouble(); //double random number 
            double prob= 0.5;
            Console.WriteLine(result);
            if (result < prob)
            {
                result = 0;
                return true;  
            }
            else
            {
                result = 0;
                return false;
            }
            
        }
        public void drawingForest()
        {
            for (int row = 0; row < rowsize; row++)
            {
                for (int col = 0; col < colsize; col++)
                {
                    rowOutput = rowOutput + grid[row, col].ToString() + " ";
                }
                Console.WriteLine(rowOutput);
                rowOutput = "";
            }
            if(burningTrees.Count == 0)
            {
                Console.WriteLine("No more trees are burning");
            }
            else
            {
                Console.WriteLine("There are " + burningTrees.Count + " tress burning");
            }
        } 

        public void spreadFire()
        {
            foreach(Tuple<int, int> tree in burningTrees)
            {
                int row = tree.Item1;
                int col = tree.Item2;
                burnNeighbours(row, col);
            }
            burningTrees.Clear();
            burningTrees.AddRange(burntTrees);
            burntTrees.Clear();
            
        }


        private void north(int row, int col)
        {
            if (grid[row - 1, col] == '&' & burnProbability())
            {
                grid[row - 1, col] = 'X';
                burntTrees.Add(new Tuple<int, int>(row - 1, col));
            }

        }

        private void south(int row, int col)
        {
            if (grid[row + 1, col] == '&' & burnProbability())
            {
                grid[row + 1, col] = 'X';
                burntTrees.Add(new Tuple<int, int>(row + 1, col));
            }
        }

        private void east(int row, int col)
        {
            if (grid[row, col + 1] == '&' & burnProbability())
            {
                grid[row, col + 1] = 'X';
                burntTrees.Add(new Tuple<int, int>(row , col + 1));
            }
        }

        private void west(int row, int col)
        {
            if (grid[row , col - 1] == '&' & burnProbability())
            {
                grid[row, col -1] = 'X';
                burntTrees.Add(new Tuple<int, int>(row , col -1));
            }
        }
        private void burnNeighbours(int row, int col)
        {
            if (row > 0)
            {
                north(row, col);
            }
            if (row < rowsize - 1 )
            {
                south(row, col);
            }
            if (col > 0)
            {
                west(row, col);
            }
            if (col < colsize - 1 )
            {
                east(row, col);
            }
        }
        public void burnTree(int row, int col)
        {
            grid[row, col] = 'X';
            burningTrees.Add(new Tuple<int, int>(row, col));
        }
       
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\n");
            Console.WriteLine("          FOREST FIRE SIMULATOR\n");
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\n");

            bool endProgram = false;
            string userInput = "";

            Forest forest = new Forest(10, 10);
            forest.drawingForest();
            forest.burnTree(0, 1);

            while(!endProgram)
            {
                Console.WriteLine("Press q to quit and press any key to continue.");
                userInput = Console.ReadLine();
                if (userInput == "q")
                {
                    endProgram = true;
                }
                forest.drawingForest();
                forest.spreadFire();

                Console.ReadKey();
            }

        }

    }
}
