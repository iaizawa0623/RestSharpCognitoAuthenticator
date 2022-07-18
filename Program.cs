using RestSharp;
using RestSharpCognitoAuthenticator;

// 外部ファイルで設定できる項目
const string idpUrl = "https://cognito-idp.ap-northeast-1.amazonaws.com/";
const string clientId = "2qiutf2l0qlq5agfrbb0di47kj";
const string username = "developer";
const string password = "IvDF0dZ_";
const string apiUrl = "https://egd0hq2z6h.execute-api.ap-northeast-1.amazonaws.com/Prod/";

// クライアント
var client = new RestClient(
    new RestClientOptions(apiUrl) {}
){
	Authenticator = new CognitoAuthenticator(
		baseUrl : idpUrl,
		clientId: clientId,
	    username: username,
	    password: password
	)
};

//	チケット数の取得
var issuerRequest = new RestRequest("/users");
//var issuerRequest = new RestRequest("/issuer");
var response = await client.GetAsync(issuerRequest);

//	JSON文字列
var content = response.Content;
Console.WriteLine(content);

