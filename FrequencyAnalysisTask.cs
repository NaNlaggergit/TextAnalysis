namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static void FillNgram(Dictionary<string, Dictionary<string, int>> nGram, List<List<string>> text)
    {
        for(int i = 0; i < text.Count; i++)
        {
            for(int j = 0, lastBi = 1, lastTri = 2; lastBi < text[i].Count ; j++, lastBi++, lastTri++)
            {
                if (!nGram.ContainsKey(text[i][j]))
                {
                    nGram.Add(text[i][j], new Dictionary<string, int>());
                }
                var innerDict = nGram[text[i][j]];
                if (!innerDict.ContainsKey(text[i][lastBi]))
                {
                    innerDict.Add(text[i][lastBi],0);
                }
                innerDict[text[i][lastBi]]++;
                string keyTriGram = text[i][j]+" " + text[i][j+1];
                if (lastTri < text[i].Count)
                {
                    if (!nGram.ContainsKey(keyTriGram))
                    {
                        nGram.Add(keyTriGram, new Dictionary<string, int>());
                    }
                    innerDict = nGram[keyTriGram];
                    if (!innerDict.ContainsKey(text[i][lastTri]))
                    {
                        innerDict.Add(text[i][lastTri], 0);
                    }
                    innerDict[text[i][lastTri]]++;
                }
            }
        }
    }
    public static string FindMax(Dictionary<string,int> innerDict)
    {
        int maxValue = 0;
        string key=null;
        foreach(var keyValue in innerDict)
        {
            if (keyValue.Value > maxValue)
            {
                maxValue = keyValue.Value;
                key = keyValue.Key;
            }
            else if(keyValue.Value == maxValue)
            {
                if (string.CompareOrdinal(keyValue.Key, key) < 0)
                {
                    key = keyValue.Key;
                }
            }
        }
        return key;
    }
    public static void FillResult(Dictionary<string, Dictionary<string, int>> nGram, Dictionary<string, string> result)
    {
        foreach(var keyValue in nGram)
        {
            var innerDict=keyValue.Value;
            result.Add(keyValue.Key, FindMax(innerDict));
        }
    }
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var nGram=new Dictionary<string, Dictionary<string,int>>();
        FillNgram(nGram,text);
        FillResult(nGram,result);
        return result;
    }
}