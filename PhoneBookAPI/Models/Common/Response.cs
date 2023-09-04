namespace PhoneBookAPI.Models.Common
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class ReferenceInfo
    {
        [Key]
        public string RequestId { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Response
    {
        /// <summary>
        /// API Status - Custom Response Code
        /// </summary>
        [JsonProperty(Order = 1)]
        public int Status { get; set; }
        /// <summary>
        /// Response Message 
        /// </summary>
        /// 
        [JsonProperty(Order = 2)]
        public string Message { get; set; }

        [JsonProperty(Order = 3)]
        public List<ReferenceInfo> Reference { get; set; }
        /// <summary>
        /// Version number of api consumed
        /// </summary>
        /// 
        [JsonProperty(Order = 4)]
        public string Version { get; set; }
        /// <summary>
        /// For Get APIs, JSON data will be inside this.
        /// TO DO, Change String to Object
        /// </summary>
        /// 
        [JsonProperty(Order = 5)]
        public object Data { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {

        /// <summary>
        /// For Get APIs, JSON data will be inside this.
        /// TO DO, Change String to Object
        /// </summary>
        /// 
        [JsonProperty(Order = 5)]
        public new T Data { get; set; }

    }
}
