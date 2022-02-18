namespace mp_lab1;

public class Task2
{
    static void Main(string[] args)
    {
        int ignoring = 100;
        int curPage = 0; 
        int[] wordsCount = new int[1]; 
        char letter; 
        int length = 0, index; 
        int lines = 45;
        string[] words = new string[1];
        int i = 0, j = 0; 
        int[][] contentOfPage = new int[1][];
        string word = ""; 
        int linesOnPage = 0; 
        string curLine = ""; 
        
        using (StreamReader streamReader =
               new StreamReader(@"C:\Users\Ivan\RiderProjects\mp_lab1\mp_lab1\task2_input.txt"))
        {
            //Зчитуємо текст посимвольно
            reading:
            {
                if (!streamReader.EndOfStream)
                {
                    curLine = streamReader.ReadLine();
                    linesOnPage = linesOnPage + 1;
                }
                else
                {
                    streamReader.Close();
                }

                if (linesOnPage == lines)
                {
                    linesOnPage = 0;
                    curPage = curPage + 1;
                }

                i = 0;
                iter:
                {
                    if (i == curLine.Length)
                    {
                        goto nextChar;
                    }

                    letter = curLine[i];

                    if ('a' <= letter && letter <= 'z')
                    {
                        word = word + letter;
                        if (i + 1 < curLine.Length)
                            goto nextChar;
                    }
                    else if ('A' <= letter && letter <= 'Z')
                    {
                        word = word + (char) (letter + 32);
                        if (i + 1 < curLine.Length)
                            goto nextChar;
                    }
                    if (word != "")
                    {
                        index = 0;
                        checking:
                        {
                            if (index == length)
                            {
                                goto newWord;
                            }

                            if (word ==
                                words[index])
                            {
                                word = "";
                                wordsCount[index] = wordsCount[index] + 1;
                                if (contentOfPage[index].Length <
                                    wordsCount[index])
                                {
                                    j = 0;
                                    int[] newPages = new int[wordsCount[index] * 2];
                                    nextCopy:
                                    {
                                        newPages[j] = contentOfPage[index][j];
                                        j = j + 1;
                                        if (j < wordsCount[index] - 1)
                                            goto nextCopy;
                                    }
                                    contentOfPage[index] = newPages;
                                    contentOfPage[index][wordsCount[index] - 1] = curPage;
                                }

                                contentOfPage[index][wordsCount[index] - 1] = curPage;
                                goto nextChar;
                            }

                            index++;
                            goto checking;
                        }

                        newWord:
                        if (length ==
                            words.Length)
                        {
                            string[] nw = new string[words.Length * 2];
                            int[] nwca = new int[words.Length * 2];
                            int[][] npa = new int[words.Length * 2][];
                            index = 0;
                            copyTheNextElement:
                            {
                                if (index == length)
                                {
                                    words = nw;
                                    wordsCount = nwca;
                                    contentOfPage = npa;
                                    goto addTheLastWord;
                                }

                                nw[index] = words[index];
                                nwca[index] = wordsCount[index];
                                npa[index] = contentOfPage[index];
                                index++;
                                goto copyTheNextElement;
                            }
                        }

                        addTheLastWord:
                        {
                            words[length] = word;
                            wordsCount[length] = 1;
                            contentOfPage[length] = new int[1];
                            contentOfPage[length][0] = curPage;
                            length++;
                            word = "";
                        }
                    }

                    nextChar:
                    {
                        i = i + 1;
                        if (i >= curLine.Length)
                        {
                            goto readNextLine;
                        }

                        goto iter;
                    }
                }

                readNextLine:
                if (!streamReader.EndOfStream)
                {
                    goto reading;
                }
            }
        }

        //сортування бульбашкою
        i = length - 1;
        loop1:
        {
            j = 0;
            if (i >= 1)
            {
                loop2:
                {
                    if (j < i)
                    {
                        int letterIndex = 0;
                        checkTheNextChar:
                        {
                            if (words[j][letterIndex] == words[j + 1][letterIndex]
                                && letterIndex + 1 != words[j].Length &&
                                letterIndex + 1 != words[j + 1].Length)
                            {
                                letterIndex += 1;
                                goto checkTheNextChar;
                            }
                        }

                        if (letterIndex + 1 == words[j].Length && letterIndex + 1 !=
                                                                         words[j + 1].Length
                                                                         && words[j][letterIndex] ==
                                                                         words[j + 1][letterIndex])
                        {
                            goto compareNextWord;
                        }
                        else if (letterIndex + 1 == words[j + 1].Length &&
                                 letterIndex + 1 != words[j].Length
                                 && words[j][letterIndex] == words[j + 1][letterIndex])
                        {
                            goto changeWords;
                        }

                        if (words[j][letterIndex] > words[j + 1][letterIndex])
                        {
                            goto changeWords;
                        }
                        else
                        {
                            goto compareNextWord;
                        }

                        changeWords:
                        {
                            int tempCount = wordsCount[j];
                            string tempWord = words[j];
                            var tempPages = contentOfPage[j];
                            wordsCount[j] = wordsCount[j + 1];
                            words[j] = words[j + 1];
                            contentOfPage[j] = contentOfPage[j + 1];
                            wordsCount[j + 1] = tempCount;
                            words[j + 1] = tempWord;
                            contentOfPage[j + 1] = tempPages;
                        }
                        compareNextWord:
                        j = j + 1;
                        goto loop2;
                    }
                }
                i = i - 1;
                goto loop1;
            }
        }
        
        index = 0;
        writing:
        {
            if (wordsCount[index] < ignoring)
            {
                curPage = 0;
                outp:
                {
                    if (curPage == 0)
                    {
                        Console.Write(
                            $"{words[index]} - {contentOfPage[index][curPage]}");
                    }

                    curPage = curPage + 1;
                    if (wordsCount[index] == curPage)
                    {
                        Console.WriteLine();
                        goto endoutp;
                    }

                    if (contentOfPage[index][curPage] !=
                        contentOfPage[index][curPage - 1])
                    {
                        Console.Write($", {contentOfPage[index][curPage]}");
                    }

                    goto outp;
                }
            }
            endoutp:
            {
                index = index + 1;
                if (index < length)
                {
                    goto writing;
                }
            }
        }
        
    }
}