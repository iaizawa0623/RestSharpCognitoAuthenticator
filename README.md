# RestSharpCognitoAuthenticator

Amazon Cognito authorizer for RestSharp.  
This is just a little encapsulation of the Cognito authentication part.  
It is implemented only as I thought necessary, so I recommend that you use it only as a reference.

RestSharp 用の Amazon Cognito のオーソライザー。  
Cognito 認証のとこをちょっとカプセル化しただけです。  
必要だと思った部分しか実装されてないので、飽くまで参考程度にとどめることをお勧めします。

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
```

## MIT License Summary (ja)
1. このソフトウェアを誰でも無償で無制限に扱って良い。ただし、著作権表示および本許諾表示をソフトウェアのすべての複製または重要な部分に記載しなければならない。
2. 作者または著作権者は、ソフトウェアに関してなんら責任を負わない。
