namespace SlimsteMens.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPlayedToFinaleQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinaleQuestions", "Played", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinaleQuestions", "Played");
        }
    }
}
