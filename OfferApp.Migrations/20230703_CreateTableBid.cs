﻿using FluentMigrator;

namespace OfferApp.Migrations
{
    [Migration(20230703191215)]
    public class CreateTableBid : Migration
    {
        public override void Down()
        {
            // API odpala skrypty w innym miejscu niż Console App
            // Z tego względu potrzebowałem odnieść się do bazowej lokalizacji
            // Oba projekty działają z tą ścieżką
            Execute.Script($"{AppDomain.CurrentDomain.BaseDirectory}delete_bid_table.sql");
        }

        public override void Up()
        {
            // API odpala skrypty w innym miejscu niż Console App
            // Z tego względu potrzebowałem odnieść się do bazowej lokalizacji
            // Oba projekty działają z tą ścieżką
            Execute.Script($"{AppDomain.CurrentDomain.BaseDirectory}create_bid_table.sql");
        }
    }
}
