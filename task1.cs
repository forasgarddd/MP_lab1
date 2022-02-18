namespace mp_lab1;

public class Task1
{
    static void Main(string[] args)
        {
            string[] ignoreList = { "the", "for", "a", "in", "and", "at"};
            int max = 25;
            string word = "";
            string[] words = Array.Empty<string>();
            int length = 0;
            int[] counts = Array.Empty<int>();
            int i = 0;

            using (StreamReader reader = new StreamReader(@"C:\Users\Ivan\RiderProjects\mp_lab1\mp_lab1\task1_input.txt"))
            {
                //Зчитуємо текст посимвольно
                reading:
                {
                    if (reader.EndOfStream == true)
                        reader.Close();
                    char letter = (char) reader.Read();
                    //слова з маленькими літерами
                    if (letter is >= 'a' and <= 'z')
                    {
                        word = word + letter;
                        if (reader.EndOfStream == false)
                        {
                            goto reading;
                        }
                    }
                    //Нормалізація великих літер
                    else if (letter is >= 'A' and <= 'Z')
                    {
                        word = word + ((char) (letter + 32)).ToString();
                        if (reader.EndOfStream == false)
                        {
                            goto reading;
                        }
                    }
                    if (word != "")
                    {
                        i = 0;
                        // Перевірка на попадання у ігнор-ліст
                        ignoreCheck:
                        {
                            if (word == ignoreList[i])
                            {
                                word = "";
                                if (reader.EndOfStream)
                                    reader.Close();
                                goto reading;
                            }
                            i++;
                            if (i < ignoreList.Length)
                                goto ignoreCheck;
                        }
                        i = 0;
                        // Додаємо нове слово, або збільшуємо лічильник для вже доданого
                        newWord:
                        {
                            if (i == length)
                            {
                                goto isNew;
                            }
                            if (word == words[i])
                            {
                                counts[i]++;
                                word = "";
                                if (reader.EndOfStream)
                                    reader.Close();
                                goto reading;
                            }
                            i++;
                            goto newWord;
                        }
                        //При додаванні нового слова, збільшуємо розмір масиву
                        isNew:
                        if (length >= words.Length * 0.9)
                        {
                            string[] newWords = new string[(length + 2) * 2];
                            int[] newCounts = new int[(length + 2) * 2];
                            i = 0;
                            startCopying:
                            {
                                if (i == length)
                                {
                                    words = newWords;
                                    counts = newCounts;
                                    goto finishCopying;
                                }
                                newWords[i] = words[i];
                                newCounts[i] = counts[i];
                                i++;
                                goto startCopying;
                            }
                        }
                        finishCopying:
                        words[length] = word;
                        counts[length] = 1;
                        word = "";
                        length++;
                    }
                    if (reader.EndOfStream == false)
                        goto reading;
                }
            }
            
            //сортування бульбашкою
            int x = length - 1;
            loop1:
            {
                int y = 0;
                if (x >= 1)
                {
                    loop2:
                    {
                        if (y < i)
                        {
                            if (counts[y] < counts[y+1])
                            {
                                int tempCounts = counts[y];
                                string tempWords = words[y];
                                counts[y] = counts[y + 1];
                                words[y] = words[y + 1];
                                counts[y + 1] = tempCounts;
                                words[y + 1] = tempWords;
                            }
                            y = y + 1;
                            goto loop2;
                        }
                    }
                    x = x - 1;
                    goto loop1;
                }
            }
            
            i = 0;
            writing:
            {
                Console.WriteLine(words[i] + " - " + counts[i]);
                i++;
                if (i < max && i < length)
                    goto writing;
            }
            
        }
    }