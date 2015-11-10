using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticsLab
{
    class PairWiseAlign
    {
        
        /// <summary>
        /// Align only 5000 characters in each sequence.
        /// </summary>
        private int SubstitutionCost = 1;
        private int InsertDeleteCost = 5;
        private int CharsMatchCost = -3;

        private int MaxCharactersToAlign = 5001;

        double[] results = null;
        double[] prev = null;

        public int width = 0;
        public int height = 0;
        public string X = "";
        public string Y = "";

        public int currentRow = 0;
        
        /// <summary>
        /// this is the function you implement.
        /// </summary>
        /// <param name="sequenceA">the first sequence</param>
        /// <param name="sequenceB">the second sequence, may have length not equal to the length of the first seq.</param>
        /// <param name="resultTableSoFar">the table of alignment results that has been generated so far using pair-wise alignment</param>
        /// <param name="rowInTable">this particular alignment problem will occupy a cell in this row the result table.</param>
        /// <param name="columnInTable">this particular alignment will occupy a cell in this column of the result table.</param>
        /// <returns>the alignment score for sequenceA and sequenceB.  The calling function places the result in entry rowInTable,columnInTable
        /// of the ResultTable</returns>
        public int Score(GeneSequence sequenceA, GeneSequence sequenceB, ResultTable resultTableSoFar, int rowInTable, int columnInTable)
        {
            initialize(sequenceA, sequenceB);

            for (int i = 1; i < height; i++)
            {
                for (int j = 1; j < width; j++)
                {
                    int diagCellCost = 0;
                    if (X[j - 1] == Y[i - 1])
                        diagCellCost = CharsMatchCost;
                    else
                        diagCellCost = SubstitutionCost;

                    double topCell = prev[j] + InsertDeleteCost;
                    double leftCell = results[j - 1] + InsertDeleteCost;
                    double diagCell = prev[j - 1] + diagCellCost;

                    double min = Math.Min(topCell, Math.Min(diagCell, leftCell));

                    results[j] = min;
                }

                currentRow++;
                SwapArrays();
            }

            return (int)prev[width - 1];
        }

        public void initialize(GeneSequence aSequence, GeneSequence bSequence)
        {
            X = aSequence.Sequence;
            Y = bSequence.Sequence;

            setDimensions();

            prev = new double[width];
            results = new double[width];

            for (int i = 1; i < width; ++i)
                results[i] = InsertDeleteCost * i;

            currentRow++;
            SwapArrays();
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

            results[0] = InsertDeleteCost * currentRow;
        }
    }
}
