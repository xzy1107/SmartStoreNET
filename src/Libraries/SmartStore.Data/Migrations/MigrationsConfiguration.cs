﻿namespace SmartStore.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	using Setup;
    using SmartStore.Core.Data;
    using SmartStore.Core.Domain.Catalog;
	using SmartStore.Core.Domain.Common;
	using SmartStore.Utilities;

	public sealed class MigrationsConfiguration : DbMigrationsConfiguration<SmartObjectContext>
	{
		public MigrationsConfiguration()
		{
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "SmartStore.Core";

            if (DataSettings.Current.IsSqlServer)
            {
                var commandTimeout = CommonHelper.GetAppSetting<int?>("sm:EfMigrationsCommandTimeout");
                if (commandTimeout.HasValue)
                {
                    CommandTimeout = commandTimeout.Value;
                }

                CommandTimeout = 9999999;
            }
		}

		public void SeedDatabase(SmartObjectContext context)
		{
			using (var scope = new DbContextScope(context, hooksEnabled: false))
			{
				Seed(context);
				scope.Commit();
			}		
		}

		protected override void Seed(SmartObjectContext context)
		{
			context.MigrateLocaleResources(MigrateLocaleResources);
			MigrateSettings(context);
        }

		public void MigrateSettings(SmartObjectContext context)
		{

		}

		public void MigrateLocaleResources(LocaleResourcesBuilder builder)
		{
            builder.AddOrUpdate("Admin.Configuration.Languages.NoAvailableLanguagesFound",
                "There were no other available languages found for version {0}. On <a href=\"https://translate.smartstore.com/\" target=\"_blank\">translate.smartstore.com</a> you will find more details about available resources.",
                "Es wurden keine weiteren verfügbaren Sprachen für Version {0} gefunden. Auf <a href=\"https://translate.smartstore.com/\" target=\"_blank\">translate.smartstore.com</a> finden Sie weitere Details zu verfügbaren Ressourcen.");

            builder.AddOrUpdate("Checkout.OrderCompletes",
                "Your order will be completed.",
                "Ihre Bestellung wird abgeschlossen.");

            builder.AddOrUpdate("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TextPrompt",
                "Text prompt",
                "Text Eingabeaufforderung",
                "Specifies the prompt text.",
                "Legt den Text zur Eingabeaufforderung fest.");

            builder.AddOrUpdate("Admin.Catalog.Products.ProductVariantAttributes.Attributes.Fields.TextPrompt",
                "Text prompt",
                "Text Eingabeaufforderung");

            builder.AddOrUpdate("Admin.Catalog.Categories.Fields.ExternalLink",
                "External link",
                "Externer Link",
                "Alternative external link for this category in the main menu and in category listings. For example, to a landing page that contains a back link to the category.",
                "Abweichender, externer Verweis für diese Warengruppe im Hauptmenü und in Warengruppen-Listings. Z.B. auf eine Landingpage, die einen Rückverweis auf die Warengruppe enthält.");

            builder.AddOrUpdate("Admin.ContentManagement.Menus.Title",
                "Title",
                "Titel",
                "Specifies the title. Please keep in mind that this title is not displayed in all design templates.",
                "Legt den Titel fest. Bitte beachten Sie, dass dieser Titel nicht bei allen Design-Vorlagen ausgegeben wird.");

            builder.AddOrUpdate("Admin.ContentManagement.Menus.Item.InvalidRouteValues",
                "Please check the link data. No link can be created on the basis of your input.",
                "Bitte überprüfen Sie die Link-Daten. Auf Basis Ihrer Eingabe kann kein Link erzeugt werden.");

            builder.AddOrUpdate("Admin.Packaging.IsIncompatible",
                "The package is not compatible the current app version {0}. Please update Smartstore.NET or install another version of this package.",
                "Das Paket ist nicht kompatibel mit der aktuellen Programmversion {0}. Bitte aktualisieren Sie Smartstore.NET oder nutzen Sie eine andere, kompatible Paket-Version.");
        }
    }
}
