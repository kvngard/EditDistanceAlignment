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
        public int Align(GeneSequence sequenceA, GeneSequence sequenceB, ResultTable resultTableSoFar, int rowInTable, int columnInTable)
        {
            dpRows table = new dpRows(sequenceA, sequenceB);

            for (int i = 1; i < table.height; i++)
            {
                for (int j = 1; j < table.width; j++)
                {
                    double topCell = table.GetCell(j, -1) + InsertDeleteCost;
                    double leftCell = table.GetCell(j - 1) + InsertDeleteCost;
                    double diagCell = table.GetCell(j - 1, -1) + diff(table.X[j - 1], table.Y[i - 1]);

                    double min = Math.Min(topCell, Math.Min(diagCell, leftCell));

                    table.SetCell(j, min);
                }

                table.currentRow++;
                table.SwapArrays();
            }

            return table.GetFinal();
        }

        public int diff(char letterA, char letterB)
        {
            if (letterA == letterB)
                return CharsMatchCost;
            else
                return SubstitutionCost;
        }
    }
}
