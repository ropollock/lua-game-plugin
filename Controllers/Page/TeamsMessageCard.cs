namespace Lou.Controllers {
    public static class TeamsMessageCard {
        public static string BuildJSON(GMPageRequest request) {
            return $@"{{
    ""@type"": ""MessageCard"",
    ""@context"": ""https://schema.org/extensions"",
    ""themeColor"": ""0078D7"",
    ""summary"": ""GM Page Request"",
    ""title"": ""GM Page Request: [{request.RequestId}] - {request.Status}"",
    ""sections"": [
        {{
            ""activityTitle"": ""{request.PlayerName} [{request.UserId}] has Requested Help"",
            ""activitySubtitle"": ""Discord: {request.Discord} |   Email: {request.Email}"",
            ""activityImage"": ""http://icons.iconarchive.com/icons/3xhumed/mega-games-pack-27/128/Ultima-Collection-1-icon.png"",
            ""facts"": [
                {{
                    ""name"": ""Server:"",
                    ""value"": ""{request.Server}""
                }},
                {{
                    ""name"": ""Region:"",
                    ""value"": ""{request.Region}""
                }},
                {{
                    ""name"": ""Location:"",
                    ""value"": ""{request.Location}""
                }},
                {{
                    ""name"": ""Time:"",
                    ""value"": ""{request.Time}""
                }}
            ],
            ""text"": ""Message: {request.UserMessage}""
        }}
    ],
    ""potentialAction"": [
        {{
            ""@type"": ""ActionCard"",
            ""name"": ""Send a Response"",
            ""inputs"": [
                {{
                    ""@type"": ""TextInput"",
                    ""id"": ""comment"",
                    ""title"": ""Enter your response to the player."",
                    ""isMultiline"": true
                }}
            ],
            ""actions"": [
                {{
                    ""@type"": ""HttpPOST"",
                    ""name"": ""OK"",
                    ""target"": ""http://...""
                }}
            ]
        }},
        {{
            ""@type"": ""ActionCard"",
            ""name"": ""Status"",
            ""inputs"": [
                {{
                    ""@type"": ""MultichoiceInput"",
                    ""id"": ""Status"",
                    ""title"": ""Pick an option"",
                    ""value"": ""Open"",
                    ""choices"": [
                        {{
                            ""display"": ""Open"",
                            ""value"": ""Open""
                        }},
                        {{
                            ""display"": ""In-Progress"",
                            ""value"": ""In-Progress""
                        }},
                        {{
                            ""display"": ""Closed"",
                            ""value"": ""Closed""
                        }}
                    ]
                }}
            ],
            ""actions"": [
                {{
                    ""@type"": ""HttpPOST"",
                    ""name"": ""OK"",
                    ""target"": ""http://...""
                }}
            ]
        }}
    ]
}}";
        }
    }
}