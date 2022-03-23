namespace MgAPI.Business.JSONModels
{
    public class JSONMessage
    {
        public string Message { get; set; }

        public JSONMessage(string message)
        {
            Message = message;
        }
    }
}
