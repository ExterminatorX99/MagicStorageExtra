﻿using Terraria.ID;
using Terraria.ModLoader;

namespace MagicStorage.Stations
{
	//Overwrite the base class logic
	[Autoload(true)]
	public class CombinedStations4Item : CombinedStationsItem<CombinedStations4Tile>
	{
		public override string ItemName => "Combined Stations (Final Tier)";

		public override string ItemDescription => "Combines the functionality of all crafting stations and liquids";

		public override int Rarity => ItemRarityID.Purple;

		public override void SafeSetDefaults()
		{
			Item.value = BasePriceFromItems((ModContent.ItemType<CombinedStations3Item>(), 1),
				(ModContent.ItemType<CombinedFurnitureStations2Item>(), 1),
				(ItemID.Autohammer, 1),
				(ItemID.LunarCraftingStation, 1),
				(ItemID.LavaBucket, 30),
				(ItemID.HoneyBucket, 30));
		}

		public override void GetItemDimensions(out int width, out int height)
		{
			width = 30;
			height = 30;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<CombinedStations3Item>())
				.AddIngredient(ModContent.ItemType<CombinedFurnitureStations2Item>())
				.AddIngredient(ItemID.Autohammer)
				.AddIngredient(ItemID.LunarCraftingStation)
				.AddIngredient(ItemID.LavaBucket, 30)
				.AddIngredient(ItemID.HoneyBucket, 30)
				.Register();
		}
	}
}