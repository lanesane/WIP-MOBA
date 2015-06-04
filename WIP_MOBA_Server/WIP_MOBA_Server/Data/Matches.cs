using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scouting_2013_Control;
using System.Windows.Forms;

namespace Scouting_2013_Control.Data
{
    public class Matches
    {

        #region Variables and Constants
        public Excel excel;
        private Int32[][] matchesAndTeams;
        private Int32[][] teamsAndMatches;

        #endregion


        #region Initialization and Setup
        public Matches(Excel _excel)
        {
            excel = _excel;

            matchesAndTeams = new Int32[excel.GetNumberOfMatches()][];

            if (excel.GetIntValue("C3", 0) == 0)
            {
                MessageBox.Show("Records indicate that the teams have not been added to the matches. Click Okay to begin the conversion process");

                ConvertMatchList();
            }

            for (Int32 x = 0; x < matchesAndTeams.Length; x++)
            {
                matchesAndTeams[x] = new Int32[6];

                for (Int32 z = 0; z < 6; z++)
                {
                    matchesAndTeams[x][z] = excel.GetIntValue("C" + ( ( ( (x + 1) * 6) + 2) - (6 - (z + 1) ) ).ToString(), 0);
                }
            }

            teamsAndMatches = new Int32[excel.GetNumberOfTeams()][];

            for (Int32 j = 0; j < excel.GetNumberOfTeams(); j++)
            {
                teamsAndMatches[j] = new Int32[excel.GetMaxMatchesPerTeam() + 1];
                teamsAndMatches[j][0] = excel.GetIntValue("A" + (j + 3).ToString(), 1);
            }

            for (Int32 i = 0; i < matchesAndTeams.Length; i++)
            {
                if(i > 0)
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("[Data] [Matches] Progress: " + (Int32)((Double)((Double)i / (Double)matchesAndTeams.Length) * 100) + "% Complete");
                for (Int32 z = 0; z < 6; z++)
                {
                    for (Int32 y = 0; y < excel.GetNumberOfTeams(); y++)
                    {
                        if (teamsAndMatches[y][0] == matchesAndTeams[i][z])
                        {
                            for (Int32 q = 1; q < teamsAndMatches[y].Length; q++)
                            {
                                if (teamsAndMatches[y][q] == 0 || teamsAndMatches[y][q] < 1)
                                {
                                    teamsAndMatches[y][q] = i + 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion


        #region "Get" Functions
        public Int32[][] GetMatchesAndTeams()
        {
            return matchesAndTeams;
        }

        public Int32[] GetTeamsForMatch(Int32 match)
        {
            return matchesAndTeams[match - 1];
        }

        public Int32[][] GetTeamsAndMatches()
        {
            return teamsAndMatches;
        }

        public Int32[] GetMatchList(Int32 team)
        {
            for (Int32 z = 0; z < teamsAndMatches.Length; z++)
            {
                if (teamsAndMatches[z][0] == team)
                {
                    return teamsAndMatches[z];
                }
            }
            return null;
        }

        public Int32 GetTotalStatsForTeam(Int32 team, String column)
        {
            Int32[] matches = GetMatchList(team);
            Int32 tally = 0;

            for (Int32 i = 1; i < matches.Length; i++)
            {
                if (matches[i] == 0)
                    continue;
                Int32 index = Array.IndexOf(GetTeamsForMatch(matches[i]), team);
                Int32 rowNum = ((6 * matches[i]) - 3) + index;
                Int32 value = excel.GetIntValue(column + rowNum.ToString(), 0);

                if (value != 0)
                {
                    tally += value;
                }
            }

            return tally;
        }
        #endregion


        #region Create Match List

        private void ConvertMatchList()
        {
            Console.WriteLine("Please enter the name of the file that holds the matches (ie: matches.xlsx):");
            String fileName = Console.ReadLine();

            Excel matchList = new Excel("Matches Import\\" + fileName, 0, 0, 0);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = matchList.matches;

            for (int i = 1; i <= worksheet.UsedRange.Rows.Count; i++)
            {
                if (i > 0)
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("[Data] [Matches] Converting: " + (Int32)((Double)((Double)i / (Double)worksheet.UsedRange.Rows.Count) * 100) + "% Complete");
                excel.Write("C" + ((i * 6) - 3).ToString(), matchList.GetStringValue("C" + (i + 1).ToString(), 0), 0);
                excel.Write("C" + ((i * 6) - 2).ToString(), matchList.GetStringValue("D" + (i + 1).ToString(), 0), 0);
                excel.Write("C" + ((i * 6) - 1).ToString(), matchList.GetStringValue("E" + (i + 1).ToString(), 0), 0);
                excel.Write("C" + ((i * 6)).ToString(), matchList.GetStringValue("F" + (i + 1).ToString(), 0), 0);
                excel.Write("C" + ((i * 6) + 1).ToString(), matchList.GetStringValue("G" + (i + 1).ToString(), 0), 0);
                excel.Write("C" + ((i * 6) + 2).ToString(), matchList.GetStringValue("H" + (i + 1).ToString(), 0), 0);
            }

            matchList.Close();
        }

        #endregion


        #region Sync Stats with Matches

        public void SyncStats()
        {
            for (Int32 i = 0; i < excel.GetNumberOfTeams(); i++) // for loop for each team
            {
                Int32 teamNumber = excel.GetIntValue("A" + (i + 3).ToString(), 1);
                Int32[] teamMatches = GetMatchList(teamNumber);

                for (Int32 j = 0; j < teamMatches.Length; j++) // for loop for each match
                {
                    Int32 teamIndexForMatch = -1;

                    for (Int32 z = 0; z < 6; z++)
                    {
                        Console.WriteLine(matchesAndTeams[teamMatches[j] + 1][z]);
                        if (matchesAndTeams[teamMatches[j] + 1][z] == teamNumber)
                        {
                            teamIndexForMatch = z;
                        }
                    }

                    Console.WriteLine("Team #" + teamNumber + " for Match #" + teamMatches[j] + " is at" +
                        " index " + teamIndexForMatch);
                }
            }
        }

        #endregion
    
    }
}
