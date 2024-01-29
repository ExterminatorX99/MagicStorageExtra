﻿using MagicStorage.Components;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;

namespace MagicStorage {
	public static partial class DecraftingGUI {
		internal static readonly List<int> viewingItems = new();
		internal static readonly List<bool> itemAvailable = new();
		internal static int selectedItem;

		internal static void Unload() {
			ResetRefreshCache();
			viewingItems.Clear();
			itemAvailable.Clear();
			selectedItem = -1;
		}

		internal static TEStorageHeart GetHeart() => StoragePlayer.LocalPlayer.GetStorageHeart();

		internal static TEDecraftingAccess GetDecraftingEntity() => StoragePlayer.LocalPlayer.GetDecraftingAccess();

		internal static Item DoWithdraw(Item toWithdraw, bool toInventory = false) {
			TEStorageHeart heart = GetHeart();
			if (heart is null)
				return new Item();

			if (Main.netMode == NetmodeID.MultiplayerClient) {
				ModPacket packet = heart.PrepareClientRequest(toInventory ? TEStorageHeart.Operation.WithdrawToInventoryThenTryModuleInventory : TEStorageHeart.Operation.WithdrawThenTryModuleInventory);
				ItemIO.Send(toWithdraw, packet, true, true);
				packet.Send();
				return new Item();
			}

			Item withdrawn = heart.Withdraw(toWithdraw, false);

			if (withdrawn.IsAir)
				withdrawn = CraftingGUI.TryToWithdrawFromModuleItems(toWithdraw, false);

			return withdrawn;
		}

		internal static void SetSelectedItem(int item) {
			NetHelper.Report(true, "Reassigning current item...");

			selectedItem = item;
			RefreshStorageItems();
			CraftingGUI.blockStorageItems.Clear();

			NetHelper.Report(true, "Successfully reassigned current item!");
		}
	}
}