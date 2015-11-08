using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticsLab
{
    class dpTable
    {
        double[,] results = null;

        public int width = 0;
        public int height = 0;
        public string X = "";
        public string Y = "";
        private int InsertDeleteCost = 5;
        private int MaxCharactersToAlign = 5001;

        public dpTable(GeneSequence aSequence, GeneSequence bSequence)
        {
            X = aSequence.Sequence;
            Y = bSequence.Sequence;

            setDimensions();

            results = new double[width, height];

            this.SetCell(0, 0, 0);

            for (int i = 1; i < width; ++i)
                this.SetCell(i, 0, InsertDeleteCost * i);

            for (int j = 1; j < height; ++j)
                this.SetCell(0, j, InsertDeleteCost * j);
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

        public void SetCell(int x, int y, double value)
        {
           results[x, y] = value;
        }

        public double GetCell(int x, int y)
        {
            return (results[x, y]);
        }
    }
}
