/* 使用例 */
using RestSharp;
using RestSharpCognitoAuthenticator;

const string idpUrl = "https://cognito-idp.ap-northeast-1.amazonaws.com/";
const string clientId = "<clientId>";
const string username = "<username>";
const string password = "<passwerd>";
//  APIサーバーのURL
const string apiUrl = "<apiUrl>";

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

//	Get /hello リクエスト
var helloRequest = new RestRequest($"/hello");
var helloResponse = await client.GetAsync(helloRequest);
Console.WriteLine(helloResponse?.Content);
