using System.Collections.Generic;

namespace AzureDevOpsDashBoard.Dtos.AzureDevOps
{
    public class Response<T>
    {
        public List<T> value { get; set; }
    }

}
