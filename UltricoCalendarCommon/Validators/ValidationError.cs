﻿using Newtonsoft.Json;

namespace Solomio.Api.Infrastructure.Validation
{
    #region

    #endregion

    public class ValidationError
    {
        public ValidationError(string name, string description)
        {
            Name = name != string.Empty ? name : null;
            Description = description;
        }

        public string Description { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; }
    }
}