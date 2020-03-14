using System;
using System.Linq;
using System.Collections.Generic;

namespace Lorem_Ipsum_Generator
{
    public class Generator
    {
        string separators = ".,:?!";

        public string[] words;

        Random rand;

        string originalText = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque " +
            "laudantium totam rem aperiam eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae " +
            "vitae dicta sunt explicabo Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit " +
            "sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt neque porro quisquam est " +
            "qui dolorem ipsum quia dolor sit amet consectetur adipisci velit sed quia non numquam eius modi tempora " +
            "incidunt ut labore et dolore magnam aliquam quaerat voluptatem Ut enim ad minima veniam quis nostrum " +
            "exercitationem ullam corporis suscipit laboriosam nisi ut aliquid ex ea commodi consequatur Quis autem " +
            "vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur " +
            "vel illum, qui dolorem eum fugiat quo voluptas nulla pariatur At vero eos et accusamus et iusto odio " +
            "dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas " +
            "molestias excepturi sint obcaecati cupiditate non provident similique sunt in culpa qui officia deserunt " +
            "mollitia animi id est laborum et dolorum fuga Et harum quidem rerum facilis est et expedita distinctio " +
            "Nam libero tempore cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime " +
            "placeat facere possimus omnis voluptas assumenda est omnis dolor repellendus Temporibus autem quibusdam " +
            "et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et " +
            "molestiae non recusandae Itaque earum rerum hic tenetur a sapiente delectus ut aut reiciendis voluptatibus " +
            "maiores alias consequatur aut perferendis doloribus asperiores repellat";

        public Generator()
        {
            words = originalText.ToLower().Split(' ');
            rand = new Random();
        }

        public string Generate(int wordsCount, int paragraphsCount)
        {
            string genText = "";
            bool wordOrSeparator = true, upper = true;
            int wordPerParagraphCount = 0, wordsPerParagraphLimit = rand.Next(4, 10);

            List<string> paragraphs = new List<string>();

            for (int x = 0; x < paragraphsCount; x++)
            {
                for (int i = 0; i < wordsCount; i++)
                {                   
                    genText += AddWordOrSeparator(ref wordOrSeparator, ref upper, ref wordPerParagraphCount,
                                                  ref wordsPerParagraphLimit);

                    wordOrSeparator = rand.Next(0, 1000) > 500 ? true : false;
                }         

                genText = CheckAndAddLastChar(genText, ref upper, ref wordOrSeparator);

                paragraphs.Add(genText.Remove(0, 1) + "\n");

                genText = "";
            }

            foreach (string paragraph in paragraphs)
            {
                genText += paragraph;
            }

            return genText;
        }

        string AddWordOrSeparator(ref bool wordOrSeparator, ref bool upper, ref int wordPerParagraphCount,
                    ref int wordsPerParagraphLimit)
        {
            string result = "";

            if (wordOrSeparator)
            {
                string curWord = words[rand.Next(0, words.Length)];
                if (upper)
                {
                    curWord = Char.ToUpper(curWord[0]) + curWord.Substring(1);
                    upper = false;
                }
                result += " " + curWord;
                wordPerParagraphCount++;
            }
            else
            {
                if (wordPerParagraphCount >= wordsPerParagraphLimit)
                {
                    char sep = separators[rand.Next(0, separators.Length)];
                    if (sep == '.' || sep == '?' || sep == '!')
                    {
                        upper = true;
                    }
                    result += sep;
                    wordsPerParagraphLimit = rand.Next(4, 10);
                    wordPerParagraphCount = 0;
                }
            }

            return result;
        }

        string CheckAndAddLastChar(string paragraph, ref bool upper, ref bool wordOrSeparator)
        {
            string result = paragraph;

            char lastChar = paragraph.Last();
            if (lastChar != '.' && lastChar != '?' && lastChar != '!')
            {
                result += ".";
                upper = true;
                wordOrSeparator = true;
            }
            
            return result;
        }
    }
}
