namespace LoadIBKData.Common
{
    public interface IConfigurationFactory
    {
        string ClientId { get; }
        string Currency { get; }
        int Days { get; }
        string Exchange { get; }
        string Host { get; }
        string Port { get; }
        string PrimmaryExch { get; }
        string RecipientEmailAddress { get; }
        string SecType { get; }
        string SendingEmailAddress { get; }
        string Symbol { get; }
    }
}