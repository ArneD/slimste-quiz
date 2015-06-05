using System;
using System.Data.Entity.Migrations;

namespace SlimsteMens.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SlimsteMensContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SlimsteMensContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            base.Seed(context);
        }
    }
}
