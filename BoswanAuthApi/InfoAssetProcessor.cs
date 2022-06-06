using SeamlessHr.Core.Models;

namespace SeamlessHr.API.Helpers
{
    public class InfoAssetProcessor
    {
        public static InfoAsset GetSomeText(InfoAsset asset, int processor)
        {
            var newAsset = new InfoAsset();
            if(processor == 1)
            {
                newAsset.Asset1 = GetSometext1(asset.Asset1);
                newAsset.Asset2 = GetSometext2(asset.Asset2);
            }else if (processor == 2)
            {
                newAsset.Asset1 = GetSometext3(asset.Asset1);
                newAsset.Asset2 = GetSometext4(asset.Asset2);
            }else
            {
                throw new Exception("Unable to process the assets files");
            }
            return asset;
        }

        private static string GetSometext1(string inputText)
        {
            string result = string.Empty;
            for(int i = 0; i < inputText.Length; i++)
            {
                if(i % 2 == 0)
                    result += inputText[i];

            }
            return result;
        }

        private static string GetSometext2(string inputText)
        {
            string result = string.Empty;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (i % 2 == 1)
                    result += inputText[i];

            }
            return result;
        }

        private static string GetSometext3(string inputText)
        {
            string result = string.Empty;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (i % 3 == 0)
                    result += inputText[i];

            }
            return result;
        }

        private static string GetSometext4(string inputText)
        {
            string result = string.Empty;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (i % 4 == 1)
                    result += inputText[i];

            }
            return result;
        }
    }
}
