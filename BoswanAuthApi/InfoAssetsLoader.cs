using Newtonsoft.Json;
using SeamlessHr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamlessHr.Service.Services
{
    public class InfoAssetsLoader
    {
        public static string LoadAssets()
        {
            return ScriptLoader.GetString("InfoAssets.txt");
        }
        public static string LoadAssets2()
        {
            return ScriptLoader.GetString("InfoAssets2.txt");
        }

        public static InfoAsset GetAsset()
        {
            var assetString = LoadAssets();
            var assetObj = JsonConvert.DeserializeObject<InfoAssetObj>(assetString);
            return assetObj.Asset;
        }

        public static InfoAsset GetAsset2()
        {
            var assetString = LoadAssets2();
            var assetObj = JsonConvert.DeserializeObject<InfoAssetObj>(assetString);
            return assetObj.Asset;
        }


    }
}
