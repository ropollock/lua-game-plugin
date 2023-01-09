using System;
using NLua;

namespace Lou.Controllers {
    [Serializable]
    public class GMPageRequest
    {
        public string RequestId { get; set; }
        public string UserId { get; set; }
        public string PlayerName{ get; set; }
        public string UserMessage{ get; set; }
        public string Time{ get; set; }
        public string Status{ get; set; }
        public string Email { get; set; }
        public string Discord { get; set; }
        public string Server { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }

        public static GMPageRequest LoadGMPageRequest(LuaTable postData)
        {
            GMPageRequest request = new GMPageRequest();
            var dict = postData.ToDictionary();
            
            foreach (var myField in dict)
            {
                string myKey = myField.Key; 
                string myValue = myField.Value;


                if (myKey == "RequestId") {
                    if (!string.IsNullOrEmpty(myValue)) request.RequestId = myValue;
                }
                else if (myKey == "Status") {
                    if (!string.IsNullOrEmpty(myValue)) request.Status = myValue;
                }
                else if (myKey == "PlayerName") {
                    if (!string.IsNullOrEmpty(myValue)) request.PlayerName = myValue;
                }
                else if (myKey == "UserId") {
                    if (!string.IsNullOrEmpty(myValue)) request.UserId = myValue;
                }
                else if (myKey == "UserMessage") {
                    if (!string.IsNullOrEmpty(myValue)) request.UserMessage = myValue;
                }
                else if (myKey == "Time") {
                    if (!string.IsNullOrEmpty(myValue)) request.Time = myValue;
                }
                else if (myKey == "Email") {
                    if (!string.IsNullOrEmpty(myValue)) request.Email = myValue;
                }
                else if (myKey == "Discord") {
                    if (!string.IsNullOrEmpty(myValue)) request.Discord = myValue;
                }
                else if (myKey == "Server") {
                    if (!string.IsNullOrEmpty(myValue)) request.Server = myValue;
                }
                else if (myKey == "Region") {
                    if (!string.IsNullOrEmpty(myValue)) request.Region = myValue;
                }
                else if (myKey == "Location") {
                    if (!string.IsNullOrEmpty(myValue)) request.Location = myValue;
                }
            }
            
            return request;
        }

        public override string ToString() {
            return $"{nameof(RequestId)}: {RequestId}, {nameof(UserId)}: {UserId}, {nameof(PlayerName)}: {PlayerName}, {nameof(UserMessage)}: {UserMessage}, {nameof(Time)}: {Time}, {nameof(Status)}: {Status}, {nameof(Email)}: {Email}, {nameof(Discord)}: {Discord}, {nameof(Server)}: {Server}, {nameof(Region)}: {Region}, {nameof(Location)}: {Location}";
        }
    }
}