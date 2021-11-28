using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Antwiwaa.ArchBit.Shared.Interfaces;
using Newtonsoft.Json;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public abstract class PayLoadObject : IPayLoadObject
    {
        public HttpContent GetHttpContent()
        {
            var inputJson = JsonConvert.SerializeObject(this);

            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");

            return inputContent;
        }

        public PayLoadObject GetUpdatedValues(Dictionary<string, object> updatedModel)
        {
            foreach (var (key, value) in updatedModel)
                try
                {
                    var modelProperty = GetType().GetProperty(key);
                    if (modelProperty == null) continue;
                    modelProperty.SetValue(this, value);
                }
                catch (Exception)
                {
                }

            return this;
        }
    }
}