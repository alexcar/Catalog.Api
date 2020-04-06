using System.Collections.Generic;
using Catalog.Domain.Responses.Item;
using Newtonsoft.Json;
using RiskFirst.Hateoas.Models;

namespace Catalog.Api.Responses
{
    public class ItemHateoasResponse : ILinkContainer
    {
        public ItemResponse Data;
        private Dictionary<string, Link> _links;

        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, Link> Links
        {
            get => _links ??= new Dictionary<string, Link>();
            set => _links = value;
        }

        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }
    }
}
