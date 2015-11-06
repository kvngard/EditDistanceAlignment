using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticsLab
{
    class dpTable
    {
        node[,] results = null;

        public int width = 0;
        public int height = 0;
        public string X = "";
        public string Y = "";
        private int InsertDeleteCost = 5;
        private int MaxCharactersToAlign = 5000;

        public dpTable(GeneSequence aSequence, GeneSequence bSequence)
        {
            X = aSequence.Sequence;
            Y = bSequence.Sequence;

            setDimensions();

            results = new node[width, height];

            this.SetCell(0, 0, -3, null, "start");

            for (int i = 1; i < width; ++i)
                this.SetCell(i, 0, InsertDeleteCost * i, GetCell(i - 1, 0), "insert");

            for (int j = 1; j < height; ++j)
                this.SetCell(0, j, InsertDeleteCost * j, GetCell(0, j - 1), "insert");
        }

        private void setDimensions()
        {
            if (X.Length < MaxCharactersToAlign)
                width = X.Length;
            else
                width = MaxCharactersToAlign;

            if (Y.Length < MaxCharactersToAlign)
                height = Y.Length;
            else
                height = MaxCharactersToAlign;
        }

        public void SetCell(int x, int y, double value, node prev, string type)
        {
            if (results[x, y] == null)
                results[x, y] = new node(value, prev, type);
            else
                results[x, y].value = value;
        }

        public double GetCellValue(int x, int y)
        {
            return (results[x, y].value);
        }

        public node GetCell(int x, int y)
        {
            return results[x, y];
        }
    }
}
