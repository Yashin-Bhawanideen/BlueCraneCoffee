namespace BlueCraneCoffee.Models;

public record Product(int Id, string Name, string Origin, string Notes, decimal Price, string Category, string Image, string? Badge = null);

public static class ProductData
{
    public static readonly string[] Categories = { "Beans", "Gear", "Merch" };

    public static readonly IReadOnlyList<Product> Items = new List<Product>
    {
        new(1, "Ethiopia Yirgacheffe", "Light roast, 250g", "Jasmine, bergamot and ripe peach.", 149m, "Beans", "bag-ethiopia.svg", "New harvest"),
        new(2, "Colombia Huila", "Medium roast, 250g", "Caramel, red apple and cocoa.", 135m, "Beans", "bag-colombia.svg"),
        new(3, "Kenya AA", "Light roast, 250g", "Blackcurrant, grapefruit and brown sugar.", 159m, "Beans", "bag-kenya.svg"),
        new(4, "Brazil Cerrado", "Medium-dark roast, 250g", "Hazelnut, milk chocolate, low acidity.", 125m, "Beans", "bag-brazil.svg"),
        new(5, "Blue Crane House Blend", "Medium-dark roast, 250g", "Dark chocolate and toasted almond. Great in milk.", 115m, "Beans", "bag-house.svg", "Best seller"),
        new(6, "Swiss Water Decaf", "Medium roast, 250g", "Toffee, orange peel and a smooth finish.", 155m, "Beans", "bag-decaf.svg"),
        new(7, "Ceramic V60 Dripper", "Size 02, white", "Cone-shaped brewer for clean, bright pour-over.", 265m, "Gear", "v60.svg"),
        new(8, "Glass Server, 600ml", "Heat-resistant glass", "Pairs with the V60 and keeps pours tidy.", 320m, "Gear", "server.svg"),
        new(9, "Hand Grinder", "Steel burrs", "Consistent grinds from espresso to French press.", 749m, "Gear", "grinder.svg", "Staff pick"),
        new(10, "Enamel Camp Mug", "350ml, Crane blue", "Light, tough and ready for the next road trip.", 120m, "Merch", "mug.svg"),
    };

    public static Product? Find(int id) => Items.FirstOrDefault(p => p.Id == id);
}

public record DrinkItem(string Name, string Description, decimal Price, string Image);

public static class MenuData
{
    public static readonly IReadOnlyList<DrinkItem> Drinks = new List<DrinkItem>
    {
        new("Espresso", "Double shot of the house blend. Thick, sweet and short.", 30m, "espresso.svg"),
        new("Flat White", "Two shots with silky microfoam. Our most ordered drink.", 42m, "flatwhite.svg"),
        new("Cappuccino", "Equal parts espresso, steamed milk and foam, dusted with cocoa.", 42m, "cappuccino.svg"),
        new("Latte", "Gentle and milky with a fresh shot of single origin.", 45m, "latte.svg"),
        new("Mocha", "Espresso, steamed milk and 70% dark chocolate.", 50m, "mocha.svg"),
        new("Cold Brew", "Steeped for 18 hours, served over ice. Smooth and low acid.", 48m, "coldbrew.svg"),
    };

    public static readonly IReadOnlyList<(string Name, string Description, decimal Price)> Bakery = new List<(string, string, decimal)>
    {
        ("Butter Croissant", "Baked every morning at 05:30.", 38m),
        ("Almond Croissant", "Twice baked with frangipane.", 48m),
        ("Banana Bread", "Toasted, with salted butter.", 40m),
        ("Cheese & Chive Scone", "Served warm with cultured butter.", 36m),
        ("Avo on Sourdough", "Smashed avocado, feta, chilli flakes, lemon.", 85m),
        ("Ham & Cheese Toastie", "Gruyère, smoked ham and Dijon on sourdough.", 72m),
    };
}
