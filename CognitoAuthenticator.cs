using RestSharp;
using RestSharp.Authenticators;

namespace RestSharpCognitoAuthenticator;

/// <summary>
/// AWS Cognito用のAuthenticator
/// </summary>
public class CognitoAuthenticator : AuthenticatorBase
{
    private readonly string baseUrl;
    private readonly string clientId;
    private readonly string username;
    private readonly string password;
    private readonly string userAgent;
    private readonly int timeout;

    /// <summary>
    /// AWS Cognito用のAuthenticator
    /// </summary>
    public CognitoAuthenticator(
        string baseUrl,
        string clientId,
        string username,
        string password,
        string token = "",
        string userAgent = "test-requets",
        int timeout = -1
    ) : base(token)
    {
        this.baseUrl = baseUrl;
        this.clientId = clientId;
        this.username = username;
        this.password = password;
        this.userAgent = userAgent;
        this.timeout = timeout;
    }

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, token);
    }

    private async Task<string> GetToken()
    {
        // 認証クライアント
        var authClient = new RestClient(
            new RestClientOptions(baseUrl)
            {
                MaxTimeout = timeout
            }
        );

        var body =
$@"{{
	""AuthFlow"": ""USER_PASSWORD_AUTH"",
	""ClientId"": ""{clientId}"",
	""AuthParameters"": {{
		""USERNAME"": ""{username}"",
		""PASSWORD"": ""{password}""
    }}
}}";
        var request = new RestRequest()
            .AddHeader("Content-Type", "application/x-amz-json-1.1")
            .AddHeader("X-Amz-User-Agent", userAgent)
            .AddHeader("X-Amz-Target", "AWSCognitoIdentityProviderService.InitiateAuth")
            .AddJsonBody(body);

        var response = await authClient.PostAsync<CognitoResponse>(request);
        return response?.AuthenticationResult?.IdToken ?? "";
    }
}

record CognitoResponse
{
    public AuthenticationResult? AuthenticationResult { get; init; }
    public ChallengeParameters? ChallengeParameters { get; init; }
}

record AuthenticationResult
{
    public string? AccessToken { get; init; }
    public int ExpiresIn { get; init; }
    public string? IdToken { get; init; }
    public string? RefreshToken { get; init; }
    public string? TokenType { get; init; }
}

record ChallengeParameters
{
}
