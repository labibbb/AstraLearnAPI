using Microsoft.AspNetCore.Mvc;

namespace AstraLearnAPI.Model
{
    public class ResponseModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }
}
