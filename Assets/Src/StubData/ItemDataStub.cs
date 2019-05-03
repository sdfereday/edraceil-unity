using System.Collections.Generic;

public static class ItemDataStub
{
    public class ItemMeta
    {
        public ITEM_TYPE Type { get; set; }
        public int HealthValue { get; set; }
        public int MpValue { get; set; }
        // DeathState, etc, etc
    }

    public static List<ItemMeta> ItemRegistry = new List<ItemMeta>() {
        new ItemMeta()
        {
            Type = ITEM_TYPE.CHICKEN,
            HealthValue = 1
        }
    };
}
