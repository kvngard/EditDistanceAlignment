using System;
using System.Text;

namespace GeneticsLab
{
    class dpTable
    {
        public node[,] results = null;
        private int indel = 5;
        private int subst = 1;
        private int match = -3;

        public int width = 0;
        public int height = 0;
        public string X = "";
        public string Y = "";
        private int MaxCharactersToAlign = 101;

        public struct node
        {
            public double value;
            public string type;

            public node(double v, string t)
            {
                value = v;
                type = t;
            }
        }

        public dpTable(GeneSequence aSequence, GeneSequence bSequence)
        {
            X = aSequence.Sequence;
            Y = bSequence.Sequence;

            setDimensions();

            results = new node[width, height];

            this.SetCell(0, 0, 0, "start");

            for (int i = 1; i < width; ++i)
                this.SetCell(i, 0, indel * i, "left");

            for (int j = 1; j < height; ++j)
                this.SetCell(0, j, indel * j, "top");
        }

        private void setDimensions()
        {
            if (X.Length < MaxCharactersToAlign)
                width = X.Length + 1;
            else
                width = MaxCharactersToAlign;

            if (Y.Length < MaxCharactersToAlign)
                height = Y.Length + 1;
            else
                height = MaxCharactersToAlign;
        }

        public string[] Align()
        {
            for (int i = 1; i < this.height; i++)
            {
                for (int j = 1; j < this.width; j++)
                {
                    int diagCellCost = 0;
                    if (this.X[j - 1] == this.Y[i - 1])
                        diagCellCost = match;
                    else
                        diagCellCost = subst;

                    double topCell = this.GetCellValue(j, i - 1) + indel;
                    double leftCell = this.GetCellValue(j - 1, i) + indel;
                    double diagCell = this.GetCellValue(j - 1, i - 1) + diagCellCost;

                    double min = Math.Min(topCell, Math.Min(diagCell, leftCell));

                    if (min == diagCell)
                        this.SetCell(j, i, diagCell, "diag");
                    else if (min == topCell)
                        this.SetCell(j, i, topCell, "top");
                    else if (min == leftCell)
                        this.SetCell(j, i, diagCell, "left");
                }
            }
            return this.TracePath();
        }

        public string[] TracePath()
        {
            StringBuilder A = new StringBuilder();
            StringBuilder B = new StringBuilder();
            int x = width - 1;
            int y = height - 1;
            dpTable.node current = this.GetCell(x, y);

            while (x > 0 || y > 0)
            {
                switch (current.type)
                {
                    case "left":
                        A.Insert(0, this.X[x - 1]);
                        B.Insert(0, "-");
                        x--;
                        break;
                    case "top":
                        A.Insert(0, "-");
                        B.Insert(0, this.Y[y - 1]);
                        y--;
                        break;
                    default:
                        A.Insert(0, this.X[x - 1]);
                        B.Insert(0, this.Y[y - 1]);
                        x--;
                        y--;
                        break;
                }
                current = this.GetCell(x, y);
            }
            return new string[2] { A.ToString(), B.ToString() };
        }

        public void SetCell(int x, int y, double value, string type)
        {
           results[x, y] = new node(value, type);
        }

        public node GetCell(int x, int y)
        {
            return results[x, y];
        }

        public double GetCellValue(int x, int y)
        {
            return results[x, y].value;
        }
    }
}
