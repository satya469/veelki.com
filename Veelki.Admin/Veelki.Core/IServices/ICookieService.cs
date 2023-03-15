namespace Veelki.Core.IServices
{
    public interface ICookieService
    {
        /// <summary>
        /// Set Cookie.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        void Set(string key, string value, int? expireTime);

        /// <summary>
        /// Remove Cookie.
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
