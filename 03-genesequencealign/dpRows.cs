using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticsLab
{
    class dpRows
    {
        double[] results = null;
        double[] prev = null;

        public int width = 0;
        public int height = 0;
        public string X = "";
        public string Y = "";

        public int currentRow = 0;

        private int InsertDeleteCost = 5;
        private int MaxCharactersToAlign = 5001;

        public dpRows(GeneSequence aSequence, GeneSequence bSequence)
        {
            X = aSequence.Sequence;
            Y = bSequence.Sequence;

            setDimensions();

            prev = new double[width];
            results = new double[width];

            for (int i = 1; i < width; ++i)
                this.SetCell(i, InsertDeleteCost * i);

            currentRow++;
            this.SwapArrays();
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

        public void SwapArrays() 
        {
            double[] temp = null;

            temp = prev;
            prev = results;
            results = temp;

            this.SetCell(0, InsertDeleteCost * currentRow);
        }

        public void SetCell(int x, double value)
        {
           results[x] = value;
        }

        public double GetCell(int x, int? y = null)
        {
            if (y == null)
                return results[x];
            else
                return prev[x];
        }

        public int GetFinal() 
        {
            return (int)prev[width - 1];
        }
    }
}
