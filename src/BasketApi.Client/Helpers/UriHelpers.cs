namespace BasketApi.Client.Helpers
{
    /// <summary>
    /// Class that contains all the supported paths of this client application
    /// </summary>
    public static class UriHelpers
    {
        private const string UriBasketBase = "api/Basket";
        public static string UserBasketUri(string userId) => $"{UriBasketBase}/{userId}";
        public static string AddBasketItemUri(int basketId) => $"{UriBasketBase}/AddItem/{basketId}";
        public static string UpdateBasketItemUri(int basketId) => $"{UriBasketBase}/Update/{basketId}";
        public static string RemoveBasketItemUri(int basketId) => $"{UriBasketBase}/RemoveItem/{basketId}";
        public static string ClearBasketItemsUri(int basketId) => $"{UriBasketBase}/ClearItems/{basketId}";

        private const string UriProductBase = "api/Product";
        public static string ProductListUri => $"{UriProductBase}/list";
    }
}
