# RestSharpCognitoAuthenticator

Amazon Cognito authorizer for RestSharp.  
This is just a little encapsulation of the Cognito authentication part.  

RestSharp 用の Amazon Cognito のオーソライザー。  
Cognito 認証のとこをちょっとカプセル化しただけです。  

## Usage
```
var client = new RestClient(apiUrl)
{
    Authenticator = new CognitoAuthenticator(
        baseUrl: idpUrl,
        clientId: clientId,
        username: username,
        password: password
    )
};

var helloRequest = new RestRequest($"/hello");
var helloResponse = await client.GetAsync(helloRequest);
Console.WriteLine(helloResponse?.Content);
```

## MIT License Summary (ja)
1. このソフトウェアを誰でも無償で無制限に扱って良い。ただし、著作権表示および本許諾表示をソフトウェアのすべての複製または重要な部分に記載しなければならない。
2. 作者または著作権者は、ソフトウェアに関してなんら責任を負わない。
