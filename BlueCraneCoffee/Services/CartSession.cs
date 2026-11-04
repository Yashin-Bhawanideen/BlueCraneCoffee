using System.Text.Json;
using BlueCraneCoffee.Models;

namespace BlueCraneCoffee.Services;

public static class CartSession
{
    private const string Key = "cart";

    public static List<CartItem> Get(ISession session)
    {
        var json = session.GetString(Key);
        if (string.IsNullOrEmpty(json)) return new List<CartItem>();
        return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    public static void Save(ISession session, List<CartItem> items) =>
        session.SetString(Key, JsonSerializer.Serialize(items));

    public static void Clear(ISession session) => session.Remove(Key);

    public static int Count(ISession session) => Get(session).Sum(i => i.Quantity);

    public static CartSummary Summary(ISession session) => new() { Items = Get(session) };
}
