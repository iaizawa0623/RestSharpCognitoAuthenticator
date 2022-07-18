using RestSharp;
using RestSharpCognitoAuthenticator;

#region // 外部ファイルで設定できる項目
//  認証サーバーのURL
const string idpUrl = "https://cognito-idp.ap-northeast-1.amazonaws.com/";
const string clientId = "2qiutf2l0qlq5agfrbb0di47kj";
const string username = "developer";
const string password = "IvDF0dZ_";
//  APIサーバーのURL
const string apiUrl = "https://dxy1ztfd01.execute-api.ap-northeast-1.amazonaws.com/Prod/";
#endregion

// APIクライアント
var client = new RestClient(apiUrl)
{
    Authenticator = new CognitoAuthenticator(
        baseUrl: idpUrl,
        clientId: clientId,
        username: username,
        password: password
    )
};

//	チケット数リクエスト
var issuerRequest = new RestRequest($"/issuers/{username}");
var issuerResponse = await client.GetAsync(issuerRequest);
Console.WriteLine(issuerResponse?.Content);

//	チケット発行リクエスト
var ticketNo = 1;
var serialNo = $"{username}{DateTime.Today:yyyyMMdd}{ticketNo}";
var body = $@"{{
	""ticketType"": ""digital"",
	""survey"": {{
		""性別"": ""女性"",
		""年齢"": ""40代""
	}}
}}";
var ticketRequet = new RestRequest($"/tickets/{serialNo}")
    .AddJsonBody(body);
var ticketResponse = await client.PostAsync(ticketRequet);
Console.WriteLine(ticketResponse?.Content);

