namespace Antwiwaa.ArchBit.Application.Common.Configurations
{
    public class AdminConfig
    {
        public AdminUser AdminUser { get; set; }
        public string ApiBaseUrl { get; set; }
        public ApiContact ApiContact { get; set; }
        public string ApiDescription { get; set; }
        public ApiLicense ApiLicense { get; set; }
        public string ApiName { get; set; }
        public string ApiVersion { get; set; }
        public bool CorsAllowAnyOrigin { get; set; }
        public string[] CorsAllowOrigins { get; set; }
        public bool EnableCache { get; set; }
        public IdentityConfig IdentityConfig { get; set; }
        public RedisConfig RedisConfig { get; set; }
        public TwilioConfig TwilioConfig { get; set; }
        public HubtelConfig HubtelConfig { get; set; }
        public string VoucherContact { get; set; }
        public string DocumentRepoUrl { get; set; }
        public string DefaultDoaMonth { get; set; }
        public string DefaultDocMonth { get; set; }
    }

    public class HubtelConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseUrl { get; set; }
    }

    public class TwilioConfig
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string Sender { get; set; }
    }

    public class IdentityConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string OidcApiName { get; set; }
        public string OidcSwaggerUiClientId { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public string Secret { get; set; }
    }

    public class ApiContact
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class ApiLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class AdminUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RedisConfig
    {
        public string Instance { get; set; }
    }
}