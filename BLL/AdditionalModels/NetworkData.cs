﻿using DAL.AdditionalModels;
using DAL.Models;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace BLL.AdditionalModels
{
    public class NetworkData
    {
        public string XPath { get; }
        public Scientist Scientist { get; set; }
        public string Value { get; set; }
        public SocialNetworkType NetworkType { get; set; }

        public NetworkData(Scientist scientist, SocialNetworkType networkType)
        {
            XPath = $"//td[contains(.,\"{scientist.Name}\")]/../td/a[contains(@href,'{networkType.ToString().ToLower()}')]";
            Scientist = scientist;
            NetworkType = networkType;
        }

        public async Task<List<ScientistSocialNetwork>> Convert()
        {
            var scientistSocialNetworks = new List<ScientistSocialNetwork>();
            if (!string.IsNullOrEmpty(Value))
            {
                scientistSocialNetworks.Add(new ScientistSocialNetwork()
                {
                    ScientistId = Scientist.Id,
                    Url = Value,
                    Type = NetworkType,
                    SocialNetworkScientistId = GetScientistSocialNetworkAccountId()
                });
            }
            return scientistSocialNetworks;
        }

        private string GetScientistSocialNetworkAccountId()
        {
            return NetworkType switch
            {
                SocialNetworkType.Google => new Uri(Value).Query.Split("&")
                    .FirstOrDefault(parameter => parameter.Split("=")[0].Equals("?user")).Split("=")[1],
                SocialNetworkType.Scopus => new Uri(Value).Query.Split("&")
                    .FirstOrDefault(parameter => parameter.Split("=")[0].Equals("?authorId")).Split("=")[1],
                SocialNetworkType.WOS => new Uri(Value).AbsolutePath.Split("/").Last(),
                SocialNetworkType.ORCID => new Uri(Value).AbsolutePath.Split("/").Last(),
                _ => throw new Exception(),
            };
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
