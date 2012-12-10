using System.Threading.Tasks;

namespace Codemash.Notification.Context
{
    public interface ISecurityContext
    {
        /// <summary>
        /// Returns the TokenType being used for Push Notification Security
        /// </summary>
        string TokenType { get; }

        /// <summary>
        /// Returns the Access token being used to send Push Notifications
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Provision the Security for the Context instance
        /// </summary>
        /// <returns></returns>
        void Provision(string clientId, string clientSecret);
    }
}
