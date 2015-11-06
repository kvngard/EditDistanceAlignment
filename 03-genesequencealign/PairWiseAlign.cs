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
        private int MaxCharactersToAlign = 5000;
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
            dpTable table = new dpTable(sequenceA, sequenceB);

            for (int i = 1; i < table.width && i < MaxCharactersToAlign; i++)
            {
                for (int j = 1; j < table.height && j < MaxCharactersToAlign; j++)
                {
                    double topCell = table.GetCellValue(i, j - 1) + InsertDeleteCost;
                    double leftCell = table.GetCellValue(i - 1, j) + InsertDeleteCost;
                    double diagCell = table.GetCellValue(i - 1, j - 1) + diff(table.X[i - 1], table.Y[j - 1]);

                    double min = Math.Min(topCell, Math.Min(diagCell, leftCell));

                    if(min == topCell)
                        table.SetCell(i, j, topCell, table.GetCell(i, j - 1), "insert");
                    else if (min == leftCell)
                        table.SetCell(i, j, leftCell, table.GetCell(i - 1, j), "insert");
                    else if (min == diagCell)
                    {
                        string type;

                        if (table.X[i - 1] == table.Y[j - 1])
                            type = "match";
                        else
                            type = "substitution";

                        table.SetCell(i, j, diagCell, table.GetCell(i - 1, j - 1), type);
                    }
                }
            }

            return (int)table.GetCellValue(table.width - 1, table.height - 1);
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
